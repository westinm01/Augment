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

    public ChessPiece GetChessPiece(int row, int col) {
        if (!InBounds(row, col)) {
            return null;
        }
        return board.GetPiece(row, col);
    }

    // Checks to see if a space is within bounds and not occupied
    public bool isValidMoveSpace(int row, int col)
    {
        return InBounds(row, col) && !isSpaceOccupied(row, col);
    }

    // Checks to see if space is occupied by another piece
    public bool isSpaceOccupied(int row, int col)
    {
        return board.GetPiece(row, col) != null;
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
            EatPiece(tempPiece);
            
            pieceEaten = true;
        }

        // Move the backend values in the board array
        board.MovePiece(piece.coord.x, piece.coord.y, newX, -newY);

        // Update new coordinates
        piece.coord.x = newX;
        piece.coord.y = -newY;

        // Update physical location in scene
        piece.transform.position = new Vector3(newX, newY, 0);

        //!!!!!!!!CHECK TRIGGERS: 3!!!!!!!!!!!!!!!!!!!!!!!
        if(pieceEaten)
        {
            tm.CheckTrigger(3, piece);
        }
        

        // Update moves for all pieces
        GameManager.Instance.UpdateAllPossibleMoves();

        // Check if opposing player is now in check
        Player enemyPlayer = GameManager.Instance.GetPlayer(!GameManager.Instance.GetCurrentPlayer().playerTeam);
        if (enemyPlayer.isInCheck()) {
            
            // Update the player moves again to only allow moves that escape check
            enemyPlayer.UpdatePossibleMoves();
            if (enemyPlayer.isInCheckmate()) {
                GameManager.Instance.EndGame();
            }
        }
         //!!!!!!!!CHECK TRIGGERS: 1!!!!!!!!!!!!!!!!!!!!!!!!
         tm.CheckTrigger(1, piece);
         
    }

    /// <summary>
    /// Eats piece in parameter
    /// </summary>
    public void EatPiece(ChessPiece piece) {
        Player piecePlayer = GameManager.Instance.GetPlayer(piece.team);
        piecePlayer.playerPieces.Remove(piece);
        Destroy(piece.gameObject);
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
        while (xPos != pos2.x && yPos != pos2.y && InBounds(yPos, xPos)) {
            Vector2Int newSpace = new Vector2Int(xPos, yPos);
            spaces.Add(newSpace);

            xPos += xDir;
            yPos += yDir;
        }

        return spaces;
    }
}
