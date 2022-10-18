using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
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

    public void AddPiece(ChessPiece piece, int row, int col)
    {
        board.AddPiece(piece, row, col);
    }

    public void PrintBoard(){
        board.PrintBoard();
    }
}
