using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueenPiece : ChessPiece
{

    void Update()
    {

    }
    public override void GetPossibleSpaces()
    {
        base.GetPossibleSpaces();
        // Get all spaces to the right
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


        int right = 1;
        int left = -1;
        int up = 1;
        int down = -1;

        // Get all spaces to bottom right
        for (int i = 1; i <= (GameManager.Instance.board.getWidth() - coord.y); i++)
        {
            if(!CheckAndMovePos(i, right, up))
            {
                break;
            }
        }
        // Get all spaces to top left
        for (int i = 1; i <= coord.x; i++)
        {
            if(!CheckAndMovePos(i, left, up))
            {
                break;
            }
        }
        // Get all spaces top right
        for (int i = 1; i <= (GameManager.Instance.board.getHeight() - coord.x); i++)
        {
            if(!CheckAndMovePos(i, right, down))
            {
                break;
            }
        }
        // Get all spaces bottom left
        for (int i = 1; i <= coord.y; i++)
        {
            if(!CheckAndMovePos(i, left, down))
            {
                break;
            }
        }
    }

    private bool CheckAndMovePos(int distance, int xDir, int yDir)
    {
        int xCheck = coord.x + xDir * distance;
        int yCheck = coord.y + yDir * distance;
        Vector2Int nextMove = new Vector2Int(xCheck, yCheck);
        //bool validX = (xCheck >= 0) && (xCheck < GameManager.Instance.board.getWidth());
        //bool validY = (yCheck >= 0) && (yCheck < GameManager.Instance.board.getHeight());

        //if (validX && validY)
        if (GameManager.Instance.board.isValidMoveSpace(xCheck, yCheck))
        {
            // Spot is open, add it to possible spaces
            if (ValidMoveInCheck(nextMove)) {
                possibleSpaces.Add(new Vector2Int(xCheck, yCheck));
            }
            return true;
        }
        else if (CheckIfCanEat(xCheck, yCheck))
        {
            // Spot is on an enemy piece, return false to prevent from moving further
            if (ValidMoveInCheck(nextMove)) {
                possibleEats.Add(new Vector2Int(xCheck, yCheck));
            }
            return false;
        }
        else
        {
            return false;
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
