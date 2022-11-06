using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnPiece : ChessPiece
{
    public int direction = 1;   // 1 for moving up, -1 for moving down

    public override void GetPossibleSpaces()
    {
        base.GetPossibleSpaces();
        int nextY = coord.y - direction;

        // Check move up/down
        if (GameManager.Instance.board.isValidMoveSpace(coord.x, nextY))
        {
            possibleSpaces.Add(new Vector2Int(coord.x, nextY));
            // If pawn is in starting position, also add another space
            if ((coord.y == 1 && direction < 0) || (coord.y == GameManager.Instance.board.getHeight() - 2 && direction > 0))
            {
                if (GameManager.Instance.board.isValidMoveSpace(coord.x, nextY - direction)){
                    possibleSpaces.Add(new Vector2Int(coord.x, nextY - direction));
                }
            }
        }
        
        // Check diagonal eats
        ChessPiece leftDiagonal = GameManager.Instance.board.GetChessPiece(coord.x - 1, nextY);
        ChessPiece rightDiagonal = GameManager.Instance.board.GetChessPiece(coord.x + 1, nextY);
        if (leftDiagonal != null && leftDiagonal.team != this.team) {
            possibleEats.Add(leftDiagonal.coord);
        }
        if (rightDiagonal != null && rightDiagonal.team != this.team) {
            possibleEats.Add(rightDiagonal.coord);
        }
    }
}
