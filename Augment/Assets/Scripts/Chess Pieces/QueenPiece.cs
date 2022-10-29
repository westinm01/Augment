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
            if (GameManager.Instance.board.isValidMoveSpace(i, coord.y)) {
                possibleSpaces.Add(new Vector2Int(i, coord.y));
            }
            else {
                break;
            }
        }
        // Get all spaces to left
        for (int i = coord.x - 1; i >= 0; i--)
        {
            if (GameManager.Instance.board.isValidMoveSpace(i, coord.y)) {
                possibleSpaces.Add(new Vector2Int(i, coord.y));
            }
            else {
                break;
            }
        }
        // Get all spaces up
        for (int i = coord.y + 1; i < GameManager.Instance.board.getHeight(); i++)
        {
            if (GameManager.Instance.board.isValidMoveSpace(coord.x, i)) {
                possibleSpaces.Add(new Vector2Int(coord.x, i));
            }
            else {
                break;
            }
        }
        // Get all spaces down
        for (int i = coord.y - 1; i >= 0; i--)
        {
            if (GameManager.Instance.board.isValidMoveSpace(coord.x, i)) {
                possibleSpaces.Add(new Vector2Int(coord.x, i));
            }
            else {
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

        //bool validX = (xCheck >= 0) && (xCheck < GameManager.Instance.board.getWidth());
        //bool validY = (yCheck >= 0) && (yCheck < GameManager.Instance.board.getHeight());

        //if (validX && validY)
        if (GameManager.Instance.board.isValidMoveSpace(xCheck, yCheck))
        {
            // Spot is open, add it to possible spaces
            possibleSpaces.Add(new Vector2Int(xCheck, yCheck));
            return true;
        }
        else
        {
            return false;
        }
    }
}
