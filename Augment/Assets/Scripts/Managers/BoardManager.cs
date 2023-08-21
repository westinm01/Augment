using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public GameObject selectedPieceHighlighter;
    public GameObject possibleSpaceHighlighter;
    public GameObject possibleEatHighlighter;

    private ChessBoard board;
    private List<GameObject> possibleSpaceHighlights;
    private List<GameObject> possibleEatHighlights;

    private TriggerManager tm;
    public ChessPiece lastEater = null;
    // Internal
    private bool cancelMovement = false;

    private void Awake()
    {
        // Create new chess board with 8 rows and 8 cols
        board = new ChessBoard(8, 8);
        possibleSpaceHighlights = new List<GameObject>();
        possibleEatHighlights = new List<GameObject>();
    }

    void Start()
    {
        tm = GetComponent<TriggerManager>();
    }

    public int getWidth()
    {
        return board.getWidth();
    }

    public int getHeight()
    {
        return board.getHeight();
    }

    public void PrintBoard(){
        board.PrintBoard();
    }

    public ChessBoard GetBoard()
    {
        return board;
    }

    public ChessPiece GetChessPiece(int x, int y) {
        if (!InBounds(x, y)) {
            return null;
        }
        return board.GetPiece(x, y);
    }

    // Checks to see if a space is within bounds and not occupied
    public bool isValidMoveSpace(int x, int y)
    {
        return InBounds(x, y) && !isSpaceOccupied(x, y);
    }

    // Checks to see if space is occupied by another piece
    public bool isSpaceOccupied(int x, int y)
    {
        return board.GetPiece(x, y) != null;
    }

    // Checks to see if space is within bounds of board
    public bool InBounds(int row, int col){
        if (row >= board.getHeight() || row < 0){
            // Debug.Log("Row " + row + " out of bounds");
            return false;
        }
        else if (col >= board.getWidth() || col < 0){
            // Debug.Log("Col " + col + " out of bounds");
            return false;
        }
        else{
            return true;
        }
    }
    
    public void AddPiece(ChessPiece piece, int row, int col)
    {
        board.AddPiece(piece, row, col);
    }

    public void RemovePiece(int row, int col) {
        board.RemovePiece(row, col);
    }

    /// <summary>
    /// Moves piece to global coordinates newX and newY
    /// NOTE: newY is negative, function converts it to positive
    /// </summary>
    public void MovePiece(ChessPiece piece, int newX, int newY)
    {
        // Check if moving piece eats another piece
        
        
        bool pieceEaten = false;
        ChessPiece tempPiece = GetChessPiece(newX, -newY);
        if (tempPiece != null && tempPiece.team != piece.team) {
            Debug.Log("Eating piece " + tempPiece);
            //!!!!!!!!CHECK TRIGGERS: 4!!!!!!!!!!!!!!!!!!!!!!!!
            tm.CheckTrigger(4, tempPiece);
            
            // Check if movement got cancelled
            if (cancelMovement)
            {
                cancelMovement = false;
                newX = piece.coord.x; 
                newY = -piece.coord.y;
                pieceEaten = false;
                return;
                cancelMovement = true;//?
            }
            else
            {
                lastEater = piece;//for duke
                EatPiece(tempPiece);
                pieceEaten = true;
            }
        }
        piece.lastCoord = piece.coord; //save last position!
        // Move the backend values in the board array
        board.MovePiece(piece.coord.x, piece.coord.y, newX, -newY);
                 //!!!!!!!!CHECK TRIGGERS: 1!!!!!!!!!!!!!!!!!!!!!!!!
        
        // Update new coordinates
        piece.coord.x = newX;
        piece.coord.y = -newY;

        // Update physical location in scene
        piece.transform.position = new Vector3(newX, newY, 0);
        tm.CheckTrigger(1, piece);

        //!!!!!!!!CHECK TRIGGERS: 3!!!!!!!!!!!!!!!!!!!!!!!
        if(pieceEaten)
        {
            tm.CheckTrigger(3, piece);
            
        }
        

        // Update moves for all pieces
        GameManager.Instance.UpdateAllPossibleMoves();

        // Check if opposing player is now in CHECK
        Player enemyPlayer = GameManager.Instance.GetPlayer(!GameManager.Instance.GetCurrentPlayer().playerTeam);
        if (enemyPlayer.isInCheck()) {
            Debug.Log("CHECK!");
            //Maybe add visual queues!

            // Update the player moves again to only allow moves that escape check
            enemyPlayer.UpdatePossibleMoves();
            if (enemyPlayer.isInCheckmate()) {
                Debug.Log("CHECKMATE!");
                StartCoroutine(CanvasManager.Instance.CheckmateCoroutine());
                GameManager.Instance.EndGame();
            }
            else {
                StartCoroutine(CanvasManager.Instance.CheckCoroutine());
            }
        }

         
    }

    /// <summary>
    /// Eats piece in parameter
    /// </summary>
    public void EatPiece(ChessPiece piece) {
        // EventsManager em = GameManager.Instance.GetEventsManager();
        // // em.CallOnPieceEaten(piece);
        // // if (em.cancelPieceEaten) {
        // //     em.cancelPieceEaten = false;
        // //     return;
        // // }

        // // List<bool> b = em.CallFunc(piece);
        // // foreach (bool temp in b) {
        // //     Debug.Log(temp);
        // // } 
        // Debug.Log(em.CallFunc(piece));

        Player piecePlayer = GameManager.Instance.GetPlayer(piece.team);
        Player enemyPlayer = GameManager.Instance.GetPlayer(!piece.team);
        enemyPlayer.capturedPieces.Add(piece);
        piecePlayer.playerPieces.Remove(piece);
        // Destroy(piece.gameObject);
        piece.gameObject.SetActive(false);
        board.RemovePiece(piece.coord.y, piece.coord.x);
    }

    public void HighlightPossibleMoves(ChessPiece piece)
    {
        piece.GetPossibleSpaces();
        foreach (Vector2Int space in piece.possibleSpaces)
        {
            Vector3 pos = new Vector3(space.x, -space.y, -5);   // Set z to -5 to prioritize raycast to hit highlighter rather than chess piece
            GameObject newHighlight = Instantiate(possibleSpaceHighlighter, pos, Quaternion.Euler(0, 0, 0));
            possibleSpaceHighlights.Add(newHighlight);
        }

        foreach (Vector2Int space in piece.possibleEats)
        {
            Vector3 pos = new Vector3(space.x, -space.y, -5);   // Set z to -5 to prioritize raycast to hit highlighter rather than chess piece
            GameObject newHighlight = Instantiate(possibleEatHighlighter, pos, Quaternion.Euler(0, 0, 0));
            possibleEatHighlights.Add(newHighlight);
        }
    }

    public void HighlightSquares(List<Vector2Int> squares)
    {
        foreach (Vector2Int space in squares)
        {
            Vector3 pos = new Vector3(space.x, -space.y, -5);   // Set z to -5 to prioritize raycast to hit highlighter rather than chess piece
            GameObject newHighlight = Instantiate(possibleSpaceHighlighter, pos, Quaternion.Euler(0, 0, 0));
            possibleSpaceHighlights.Add(newHighlight);
        }
    }

    public void HighlightPiece(GameObject piece)
    {
        selectedPieceHighlighter.SetActive(true);
        selectedPieceHighlighter.transform.position = piece.transform.position;
    }

    public void UnHighlightPieces()
    {
        selectedPieceHighlighter.SetActive(false);
        foreach (GameObject highlight in possibleSpaceHighlights)
        {
            Destroy(highlight);
        }

        foreach (GameObject highlight in possibleEatHighlights)
        {
            Destroy(highlight);
        }
        possibleSpaceHighlights.Clear();
        possibleEatHighlights.Clear();
    }

    /// <summary>
    /// Gets a list of all pieces on the enemy team that can attack a square
    /// </summary>
    /// <param name="vec"> Square to be checked </param>
    /// <param name="enemyPlayer"> Team of the enemy pieces that can attack </param>
    /// <returns> List of pieces that are able to attack the square </returns>
    public List<ChessPiece> GetThreateningPieces(Vector2Int vec, Player enemyPlayer)
    {
        List<ChessPiece> attackingPieces = new List<ChessPiece>();

        // Debug.Log("Checking if tile " + vec + " is threatened");
        foreach (ChessPiece piece in enemyPlayer.playerPieces)
        {
            if (piece.CanAttack(vec)) {
                attackingPieces.Add(piece);
            }
        }
        
        return attackingPieces;
    }

    public List<Vector2Int> GetSpacesInbetween(Vector2Int pos1, Vector2Int pos2) {
        List<Vector2Int> spaces = new List<Vector2Int>();
        int xPos = pos1.x;
        int yPos = pos1.y;
        int xDir = 0;
        int yDir = 0;

        // Moving from pos1 to pos2
        if (pos2.x > xPos) {
            xDir = 1;
        }
        else if (pos2.x < xPos) {
            xDir = -1;
        }

        if (pos2.y > yPos) {
            yDir = 1;
        }
        else if (pos2.y < yPos) {
            yDir = -1;
        }

        xPos += xDir;
        yPos += yDir;
        while ((xPos != pos2.x || yPos != pos2.y) && InBounds(yPos, xPos)) {
            Vector2Int newSpace = new Vector2Int(xPos, yPos);
            spaces.Add(newSpace);
            xPos += xDir;
            yPos += yDir;
        }

        return spaces;
    }

    /// <summary>
    /// Returns a list of all spaces adjacent to input that aren't taken
    /// </summary>
    /// <param name="pos"></param>
    /// <returns></returns>
    public List<Vector2Int> GetFreeAdjacentSpaces(Vector2Int pos)
    {
        List<Vector2Int> spaces = new List<Vector2Int>();
        if (isValidMoveSpace(pos.x-1, pos.y)) {
            spaces.Add(new Vector2Int(pos.x-1, pos.y));
        }
        if (isValidMoveSpace(pos.x+1, pos.y)) {
            spaces.Add(new Vector2Int(pos.x+1, pos.y));
        }
        if (isValidMoveSpace(pos.x, pos.y-1)) {
            spaces.Add(new Vector2Int(pos.x, pos.y-1));
        }
        if (isValidMoveSpace(pos.x, pos.y+1)) {
            spaces.Add(new Vector2Int(pos.x, pos.y+1));
        }
        return spaces;
    }
    
    public void SwapPiece(ChessPiece piece1, ChessPiece piece2){
        Vector2Int temp = piece1.coord;
        Vector3 tempPos = piece1.transform.position;
        piece1.coord.x = piece2.coord.x;
        piece1.coord.y = piece2.coord.y;
        piece1.transform.position = new Vector3(piece2.coord.x, -piece2.coord.y, 0);


        piece2.coord.x = temp.x;
        piece2.coord.y = temp.y;
        piece2.transform.position = new Vector3(tempPos.x, tempPos.y, 0);

        board.SwapPieces(piece1.coord.x, piece1.coord.y, piece2.coord.x, piece2.coord.y);
    }

    public void CancelMovement()
    {
        cancelMovement = true;
    }

    public void FreezeBoard(bool isFrozen)
    {
        for(int i = 0; i < board.getWidth(); i++)
        {
            for(int j = 0; j < board.getHeight(); j++)
            {
                ChessPiece piece = board.GetPiece(i,j);
                if(piece != null)
                {
                    piece.canMove = !isFrozen;
                }

            }
        }
    }
}
