using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public GameObject selectedPieceHighlighter;
    public GameObject possibleSpaceHighlighter;

    private ChessBoard board;
    private List<GameObject> possibleSpaceHighlights;

    private void Awake()
    {
        // Create new chess board with 8 rows and 8 cols
        board = new ChessBoard(8, 8);
        possibleSpaceHighlights = new List<GameObject>();
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

    public bool InBounds(int row, int col){
        if (row >= board.getHeight() || row < 0){
            //Debug.Log("Row " + row + " out of bounds");
            return false;
        }
        else if (col >= board.getWidth() || col < 0){
            //Debug.Log("Col " + col + " out of bounds");
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
        piece.coord.x = newX;
        piece.coord.y = -newY;
    }

    public void HighlightPossibleMoves(ChessPiece piece)
    {
        piece.GetPossibleSpaces();
        foreach (Vector2Int space in piece.possibleSpaces)
        {
            Vector3 pos = new Vector3(space.x, -space.y, 0);
            GameObject newHighlight = Instantiate(possibleSpaceHighlighter, pos, Quaternion.Euler(0, 0, 0));
            possibleSpaceHighlights.Add(newHighlight);
        }
    }

    public void HighlightPiece(GameObject piece)
    {
        selectedPieceHighlighter.SetActive(true);
        selectedPieceHighlighter.transform.position = piece.transform.position;
    }

    public void UnHighlightPiece()
    {
        selectedPieceHighlighter.SetActive(false);
        foreach (GameObject highlight in possibleSpaceHighlights)
        {
            Destroy(highlight);
        }
        possibleSpaceHighlights.Clear();
    }
}
