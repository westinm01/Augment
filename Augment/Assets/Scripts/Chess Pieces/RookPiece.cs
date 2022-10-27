using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RookPiece : ChessPiece
{

    void Update()
    {
        
    }

    public override void GetPossibleSpaces()
    {
        base.GetPossibleSpaces();
        /* TO DO:
         * restrict movement if piece blocking (cant tp behind piece)
         * implement a break statement if a piece is blocking to 
         * not add the rest to possible spaces
         */

        // Get all spaces to right
        for (int i = coord.x + 1; i < GameManager.Instance.board.getWidth(); i++)
        {
            possibleSpaces.Add(new Vector2Int(i, coord.y));
        }
        // Get all spaces to left
        for (int i = coord.x - 1; i >= 0; i--)
        {
            possibleSpaces.Add(new Vector2Int(i, coord.y));
        }
        // Get all spaces up
        for (int i = coord.y + 1; i < GameManager.Instance.board.getHeight(); i++)
        {
            possibleSpaces.Add(new Vector2Int(coord.x, i));
        }
        // Get all spaces down
        for (int i = coord.y - 1; i >= 0; i--)
        {
            possibleSpaces.Add(new Vector2Int(coord.x, i));
        }
    }
}
