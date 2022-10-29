using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnPiece : ChessPiece
{
    public int direction = 1;   // 1 for moving down, -1 for moving up

    public override void GetPossibleSpaces()
    {
        base.GetPossibleSpaces();
        int nextY = coord.y + direction;

        if (GameManager.Instance.board.isValidMoveSpace(coord.x, nextY))
        {
            possibleSpaces.Add(new Vector2Int(coord.x, nextY));
            // If pawn is in starting position, also add another space
            if ((coord.y == 1 && direction > 0) || (coord.y == GameManager.Instance.board.getHeight() - 2 && direction < 0))
            {
                if (GameManager.Instance.board.isValidMoveSpace(coord.x, nextY + direction)){
                    possibleSpaces.Add(new Vector2Int(coord.x, nextY + direction));
                }
            }
        }
    }
}
