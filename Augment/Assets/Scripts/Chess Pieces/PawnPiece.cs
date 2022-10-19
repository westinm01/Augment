using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnPiece : ChessPiece
{
    public int direction = 1;   // 1 for moving up, -1 for moving down

    public override void GetPossibleSpaces()
    {
        possibleSpaces.Clear();
        int nextY = coord.y + direction;

        // If the pawn is in the last square, can't move further
        // Shouldnt be possible(pawn should evolve) but just in case
        if (GameManager.Instance.board.InBounds(nextY, coord.x))
        {
            possibleSpaces.Add(new Vector2Int(coord.x, nextY));
        }

        // If pawn is in starting position, also add another space
        if ((coord.y == 1 && direction > 0) || (coord.y == GameManager.Instance.board.getHeight() - 2 && direction < 0))
        {
            possibleSpaces.Add(new Vector2Int(coord.x, nextY + direction));
        }
    }
}
