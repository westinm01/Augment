using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnPiece : ChessPiece
{

    public int direction = 1;   // 1 for moving up, -1 for moving down
    private bool wrapAround = false;

    public override void GetPossibleSpaces()
    {
        base.GetPossibleSpaces();
        int nextY = coord.y - direction;
        
        Vector2Int nextMove = new Vector2Int(coord.x, nextY);

        // Check move up/down
        if (GameManager.Instance.board.isValidMoveSpace(coord.x, nextY) && ValidMoveInCheck(nextMove))
        {
            possibleSpaces.Add(new Vector2Int(coord.x, nextY));
            // If pawn is in starting position, also add another space
            nextMove = new Vector2Int(coord.x, nextY - direction);
            if ((coord.y == 1 && direction < 0) || (coord.y == GameManager.Instance.board.getHeight() - 2 && direction > 0))
            {
                if (GameManager.Instance.board.isValidMoveSpace(coord.x, nextY - direction) && ValidMoveInCheck(nextMove)){
                    possibleSpaces.Add(new Vector2Int(coord.x, nextY - direction));
                }
            }
        }
        
        int rightPos = coord.x + 1;
        int leftPos = coord.x - 1;
        if (TryGetComponent<Otto>(out Otto ottoInstance) && this.gameObject.activeSelf == true)
        {
            wrapAround = false;
            rightPos = rightPos % 8;
            if(leftPos < 0)
            {
                leftPos = 7;
            }
        }
        // Check diagonal eats
        ChessPiece leftDiagonal = GameManager.Instance.board.GetChessPiece(leftPos, nextY);
        ChessPiece rightDiagonal = GameManager.Instance.board.GetChessPiece(rightPos, nextY);
        
        if (leftDiagonal != null && ValidMoveInCheck(leftDiagonal.coord)) {
            if (leftDiagonal.team != this.team) {
                possibleEats.Add(leftDiagonal.coord);
            }
            else {
                possibleProtects.Add(leftDiagonal.coord);
            }
        }
        if (rightDiagonal != null && ValidMoveInCheck(rightDiagonal.coord)) {
            if (rightDiagonal.team != this.team) {
                possibleEats.Add(rightDiagonal.coord);
            }
            else {
                possibleProtects.Add(rightDiagonal.coord);
            }
        }
    }

    public override bool CanAttack(Vector2Int space) {
        int nextY = coord.y - direction;

        int rightPos = coord.x + 1;
        int leftPos = coord.x - 1;
        if (TryGetComponent<Otto>(out Otto ottoInstance) && this.gameObject.activeSelf == true)
        {
            wrapAround = false;
            rightPos = rightPos % 8;
            if(leftPos < 0)
            {
                leftPos = 7;
            }
        }

        Vector2Int leftDiagonal = new Vector2Int(leftPos, nextY);
        Vector2Int rightDiagonal = new Vector2Int(rightPos, nextY);

        return (space == leftDiagonal || space == rightDiagonal);
    }

    public override void SwitchTeams()
    {
        base.SwitchTeams();
        direction = -direction;
    }
}
