using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class ChessBoard : MonoBehaviour
{
    public GameObject something;
    private int width;
    private int height;
    private ChessPiece[,] grid;

    public ChessBoard(int width, int height)
    {
        this.width = width;
        this.height = height;
        grid = new ChessPiece[width, height];

        //Backend Stringbuilder to show chessboard in console 
        StringBuilder sb = new StringBuilder();  

        //Print 8x8 chess board with width and height arguments. 
        for ( int i = 0; i < width; i++ )
        {
            for ( int j = 0; j < height; j++ )
            {
                grid[i, j] = null; 
                sb.Append(grid[i, j]);
                sb.Append(" | ");
                //Debug.DrawLine(new Vector3(i, j), new Vector3(i, j + 1), Color.white, 100f);
                //Debug.DrawLine(new Vector3(i, j), new Vector3(i + 1, j), Color.white, 100f);
            }
            //sb.AppendLine(); 
        }
        //Debug.DrawLine(new Vector3(0, height), new Vector3(width, height), Color.white, 100f);
        //Debug.DrawLine(new Vector3(width, 0), new Vector3(width, height), Color.white, 100f);
        Debug.Log(sb);
    }

    public int getWidth()
    {
        return width;
    }

    public int getHeight()
    {
        return height;
    }

    public ChessPiece GetPiece(int x, int y){
        return grid[y, x];
    }

    public void SetGrid(ChessPiece[,] newGrid)
    {
        grid = newGrid;
    }

    public void PrintBoard(){
        //Backend Stringbuilder to show chessboard in console 
        StringBuilder sb = new StringBuilder();  

        //Print 8x8 chess board with width and height arguments. 
        for ( int i = 0; i < width; i++ )
        {
            for ( int j = 0; j < height; j++ )
            {
                sb.Append(grid[i, j]);
                sb.Append(" | ");
                //Debug.DrawLine(new Vector3(i, j), new Vector3(i, j + 1), Color.white, 100f);
                //Debug.DrawLine(new Vector3(i, j), new Vector3(i + 1, j), Color.white, 100f);
            }
            sb.AppendLine(); 
        }
        //Debug.DrawLine(new Vector3(0, height), new Vector3(width, height), Color.white, 100f);
        //Debug.DrawLine(new Vector3(width, 0), new Vector3(width, height), Color.white, 100f);
        //Debug.Log(sb);
    }

    public void AddPiece(ChessPiece piece, int row, int col)
    {
        if (row < height && row >= 0 && col < width && col >= 0){
            grid[row, col] = piece;
        }
        else{
            Debug.Log("Added piece " + piece + " out of bounds at (" + row + ", " + col + ")");
        }
    }

    public void RemovePiece(int row, int col) {
        grid[row, col] = null;
    }

    public void MovePiece(int x1, int y1, int x2, int y2) {
        grid[y2, x2] = grid[y1, x1];
        grid[y1, x1] = null;
    }

    public void SwapPieces(int x1, int y1, int x2, int y2) {
        ChessPiece temp = grid[y1, x1];
        grid[y1, x1] = grid[y2, x2];
        grid[y2, x2] = temp;
    }
}
