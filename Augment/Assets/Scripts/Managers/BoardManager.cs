using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public GameObject selectedPieceHighlighter;

    private ChessBoard board;

    private void Awake()
    {
        // Create new chess board with 8 rows and 8 cols
        board = new ChessBoard(8, 8);
    }

    public int getWidth()
    {
        return board.getWidth();
    }

    public int getHeight()
    {
        return board.getHeight();
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

    public void PrintBoard(){
        board.PrintBoard();
    }

    public void HighlightPossibleMoves(ChessPiece piece)
    {

    }

    public void HighlightPiece(GameObject piece)
    {
        selectedPieceHighlighter.SetActive(true);
        selectedPieceHighlighter.transform.position = piece.transform.position;
    }

    public void UnHighlightPiece()
    {
        selectedPieceHighlighter.SetActive(false);
    }
}
