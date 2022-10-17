using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class ChessBoard : MonoBehaviour
{
    public static ChessBoard Instance { get; private set; }
    private void Awake()
    {
        if ( Instance != null && Instance != this )
        {
            Destroy(this);
        }
        else 
        {
            Instance = this;
        }
    }

    public GameObject something;
    private int width;
    private int height;
    private int[,] grid;

    public ChessBoard(int width, int height)
    {
        this.width = width;
        this.height = height;
        grid = new int[width, height];

        //Backend Stringbuilder to show chessboard in console 
        StringBuilder sb = new StringBuilder();  

        //Print 8x8 chess board with width and height arguments. 
        for ( int i = 0; i < width; i++ )
        {
            for ( int j = 0; j < height; j++ )
            {
                grid[i, j] = 1; 
                sb.Append(grid[i, j]);
                sb.Append(" | ");
                //Debug.DrawLine(new Vector3(i, j), new Vector3(i, j + 1), Color.white, 100f);
                //Debug.DrawLine(new Vector3(i, j), new Vector3(i + 1, j), Color.white, 100f);
            }
            sb.AppendLine(); 
        }
        //Debug.DrawLine(new Vector3(0, height), new Vector3(width, height), Color.white, 100f);
        //Debug.DrawLine(new Vector3(width, 0), new Vector3(width, height), Color.white, 100f);
        Debug.Log(sb);
    }

    void Start()
    {

    }
}
