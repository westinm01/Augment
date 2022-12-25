using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RookPiece : ChessPiece
{
    private void Awake()
    {
        SetPieceValue(MiniMaxAI.ROOK_VAL);
    }

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
            if (!CheckHorizontalAndVertical(i, coord.y)) {
                break;
            }
        }
        // Get all spaces to left
        for (int i = coord.x - 1; i >= 0; i--)
        {
            if (!CheckHorizontalAndVertical(i, coord.y)) {
                break;
            }
        }
        // Get all spaces up
        for (int i = coord.y + 1; i < GameManager.Instance.board.getHeight(); i++)
        {
            if (!CheckHorizontalAndVertical(coord.x, i)) {
                break;
            }
        }
        // Get all spaces down
        for (int i = coord.y - 1; i >= 0; i--)
        {
            if (!CheckHorizontalAndVertical(coord.x, i)) {
                break;
            }
        }
    }

    public override bool InPath(Vector2Int possibleMove) {
        return possibleMove.x == this.coord.x || possibleMove.y == this.coord.y;
    }

    private bool CheckHorizontalAndVertical(int x, int y) {
        Vector2Int nextMove = new Vector2Int(x, y);

        if (GameManager.Instance.board.isValidMoveSpace(x, y)) {
            if (ValidMoveInCheck(nextMove)) {
                possibleSpaces.Add(nextMove);
            }
            return true;
        }
        else if (CheckIfCanEat(x, y)) {
            possibleEats.Add(nextMove);
            return false;
        }
        else if (CheckIfCanProtect(x, y))
        {
            // Spot is on an enemy piece, return false to prevent from moving further
            if (ValidMoveInCheck(nextMove)) {
                possibleProtects.Add(new Vector2Int(x, y));
            }
            return false;
        }
        else {
            return false;
        }
    }
}
