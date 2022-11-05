using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public GameObject selectedPieceHighlighter;
    public GameObject possibleSpaceHighlighter;
    public GameObject possibleEatHighlighter;
    public GameObject player1;
    public GameObject player2; 

    private ChessBoard board;
    private List<GameObject> possibleSpaceHighlights;
    private List<GameObject> possibleEatHighlights;


    private void Awake()
    {
        // Create new chess board with 8 rows and 8 cols
        board = new ChessBoard(8, 8);
        possibleSpaceHighlights = new List<GameObject>();
        possibleEatHighlights = new List<GameObject>();
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

    public void MovePiece(ChessPiece piece, int newX, int newY)
    {
        piece.transform.position = new Vector3(newX, newY, 0);

        ChessPiece tempPiece = GetChessPiece(newX, -newY);
        Debug.Log(tempPiece);
        if (tempPiece != null && tempPiece.team != piece.team) {
            // Eat piece
            Debug.Log("Eating piece " + tempPiece);
            Destroy(tempPiece.gameObject);
        }
        board.MovePiece(piece.coord.x, piece.coord.y, newX, -newY);
        piece.coord.x = newX;
        piece.coord.y = -newY;
        board.PrintBoard();
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

    public void isCheckmate()
    {
        
    }
}
