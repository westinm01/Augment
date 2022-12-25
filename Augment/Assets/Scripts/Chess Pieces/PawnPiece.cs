using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnPiece : ChessPiece
{
    private void Awake()
    {
        SetPieceValue(MiniMaxAI.PAWN_VAL);
    }

    public int direction = 1;   // 1 for moving up, -1 for moving down

    public override void GetPossibleSpaces()
    {
        base.GetPossibleSpaces();
        int nextY = coord.y - direction;
        Vector2Int nextMove = new Vector2Int(coord.x, nextY);

        // Check move up/down
        if (GameManager.Instance.board.isValidMoveSpace(coord.x, nextY) && ValidMoveInCheck(nextMove))
        {
            possibleSpaces.Add(new Vector2Int(coord.x, nextY));
        }

        // If pawn is in starting position, also add another space
        nextMove = new Vector2Int(coord.x, nextY - direction);
        if ((coord.y == 1 && direction < 0) || (coord.y == GameManager.Instance.board.getHeight() - 2 && direction > 0))
        {
            if (GameManager.Instance.board.isValidMoveSpace(coord.x, nextY - direction) && ValidMoveInCheck(nextMove)){
                possibleSpaces.Add(new Vector2Int(coord.x, nextY - direction));
            }
        }
        
        // Check diagonal eats
        ChessPiece leftDiagonal = GameManager.Instance.board.GetChessPiece(coord.x - 1, nextY);
        ChessPiece rightDiagonal = GameManager.Instance.board.GetChessPiece(coord.x + 1, nextY);
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

        Vector2Int leftDiagonal = new Vector2Int(coord.x - 1, nextY);
        Vector2Int rightDiagonal = new Vector2Int(coord.x + 1, nextY);

        return (space == leftDiagonal || space == rightDiagonal);
    }
}
