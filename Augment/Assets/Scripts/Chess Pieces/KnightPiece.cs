using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightPiece : ChessPiece
{
    // More efficient to hard code lol
    private List<Vector2Int> allPossibleSpaces = new List<Vector2Int>{  new Vector2Int(-2, 1),
                                                                        new Vector2Int(-2, -1),
                                                                        new Vector2Int(-1, 2),
                                                                        new Vector2Int(-1, -2),
                                                                        new Vector2Int(1, 2),
                                                                        new Vector2Int(1, -2),
                                                                        new Vector2Int(2, 1),
                                                                        new Vector2Int(2, -1)};


    public override void GetPossibleSpaces()
    {
        base.GetPossibleSpaces();
        foreach (Vector2Int vec in allPossibleSpaces)
        {
            int newX = coord.x + vec.x;
            int newY = coord.y + vec.y;
            if (GameManager.Instance.board.InBounds(newX, newY))
            {
                possibleSpaces.Add(new Vector2Int(newX, newY));
            }
        }
        // int x, y;
        // int currX = coord.x;
        // int currY = coord.y;
        // for (int i=1; i < 3; i++){
        //     x = i;
        //     y = (x % 2) + 1;
        //     possibleSpaces.Add(new Vector2Int(currX + x, currY + y));
        //     possibleSpaces.Add(new Vector2Int(currX + x, currY - y));
        //     possibleSpaces.Add(new Vector2Int(currX - x, currY + y));
        //     possibleSpaces.Add(new Vector2Int(currX - x, currY - y));
        // }
    }
}
