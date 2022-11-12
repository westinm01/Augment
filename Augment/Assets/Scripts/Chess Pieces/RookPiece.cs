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
            // if (GameManager.Instance.board.isValidMoveSpace(i, coord.y)) {
            //     possibleSpaces.Add(new Vector2Int(i, coord.y));
            // }
            // else if (CheckIfCanEat(i, coord.y)) {
            //     possibleEats.Add(new Vector2Int(i, coord.y));
            //     break;
            // }
            // else {
            //     break;
            // }
            if (!CheckHorizontalAndVertical(i, coord.y)) {
                break;
            }
        }
        // Get all spaces to left
        for (int i = coord.x - 1; i >= 0; i--)
        {
            // if (GameManager.Instance.board.isValidMoveSpace(i, coord.y)) {
            //     possibleSpaces.Add(new Vector2Int(i, coord.y));
            // }
            // else if (CheckIfCanEat(i, coord.y)) {
            //     possibleEats.Add(new Vector2Int(i, coord.y));
            //     break;
            // }
            // else {
            //     break;
            // }
            if (!CheckHorizontalAndVertical(i, coord.y)) {
                break;
            }
        }
        // Get all spaces up
        for (int i = coord.y + 1; i < GameManager.Instance.board.getHeight(); i++)
        {
            // if (GameManager.Instance.board.isValidMoveSpace(coord.x, i)) {
            //     possibleSpaces.Add(new Vector2Int(coord.x, i));
            // }
            // else if (CheckIfCanEat(coord.x, i)) {
            //     possibleEats.Add(new Vector2Int(coord.x, i));
            //     break;
            // }
            // else {
            //     break;
            // }
            if (!CheckHorizontalAndVertical(coord.x, i)) {
                break;
            }
        }
        // Get all spaces down
        for (int i = coord.y - 1; i >= 0; i--)
        {
            // if (GameManager.Instance.board.isValidMoveSpace(coord.x, i)) {
            //     possibleSpaces.Add(new Vector2Int(coord.x, i));
            // }
            // else if (CheckIfCanEat(coord.x, i)) {
            //     possibleEats.Add(new Vector2Int(coord.x, i));
            //     break;
            // }
            // else {
            //     break;
            // }
            if (!CheckHorizontalAndVertical(coord.x, i)) {
                break;
            }
        }
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
        else {
            return false;
        }
    }
}
