using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingPiece : ChessPiece
{
    public int direction = 1; 
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.board.AddPiece(this, coord.y, coord.x);
        GameManager.Instance.board.PrintBoard();
    }

    // Update is called once per frame
    void Update()
    {
        possibleSpaces.Clear(); 
        int nextY = coord.y + direction;
        int nextX = coord.x + direction; 

        if (nextY < GameManager.Instance.board.getHeight() && nextY >= 0)
        {
            possibleSpaces.Add(new Vector2Int(nextX, nextY));
            possibleSpaces.Add(new Vector2Int(coord.x, nextY));
            possibleSpaces.Add(new Vector2Int(nextX, coord.y));
            possibleSpaces.Add(new Vector2Int(coord.x, -coord.y));
        }
    }
}
