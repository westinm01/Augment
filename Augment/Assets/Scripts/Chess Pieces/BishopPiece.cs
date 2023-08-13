using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class BishopPiece : ChessPiece
{
    void Update()
    {
        
    }
    public override void GetPossibleSpaces()
    {
        base.GetPossibleSpaces();
        int right = 1;
        int left = -1;
        int up = 1;
        int down = -1;


        if (TryGetComponent<Otto>(out Otto ottoInstance))
        {
            //top right
            for (int i = 1; i <= 7; i++)
            {
                if(!CheckAndMovePos(i, right, up, true))
                {
                    break;
                }
            }
            // Get all spaces to top left
            for (int i = 1; i <= 7; i++)
            {
                if(!CheckAndMovePos(i, left, up, true))
                {
                    break;
                }
            }
            // Get all spaces top right
            for (int i = 1; i <= (GameManager.Instance.board.getHeight()); i++)
            {
                if(!CheckAndMovePos(i, right, down, true))
                {
                    break;
                }
            }
            // Get all spaces bottom left
            for (int i = 1; i <= 7; i++)
            {
                if(!CheckAndMovePos(i, left, down, true))
                {
                    break;
                }
            }
        }


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

    public override bool InPath(Vector2Int possibleMove) {
        int currX = this.coord.x;
        int currY = this.coord.y;

        Vector2Int topLeft = new Vector2Int(-1, 1);
        Vector2Int topRight = new Vector2Int(1, 1);
        Vector2Int botLeft = new Vector2Int(-1, -1);
        Vector2Int botRight = new Vector2Int(1, -1);

        Vector2Int[] directions = {topLeft, topRight, botLeft, botRight};
        foreach (Vector2Int direction in directions) {
            int dist = 1;
            int nextX = currX + direction.x * dist;
            int nextY = currY + direction.y * dist;

            while (GameManager.Instance.board.InBounds(nextY, nextX)) {
                if (possibleMove.x == nextX && possibleMove.y == nextY) {
                    return true;
                }

                dist++;
                nextX = currX + direction.x * dist;
                nextY = currY + direction.y * dist;
            }
        }

        return false;
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
                possibleSpaces.Add(nextMove);
            }
            return true;
        }
        else if (CheckIfCanEat(xCheck, yCheck) && ValidMoveInCheck(nextMove))
        {
            possibleEats.Add(nextMove);
            return false;
        }
        else if (CheckIfCanProtect(xCheck, yCheck))
        {
            // Spot is on an enemy piece, return false to prevent from moving further
            if (ValidMoveInCheck(nextMove)) {
                possibleProtects.Add(new Vector2Int(xCheck, yCheck));
            }
            return false;
        }
        else
        {
            return false;
        }
    }
    
    private bool CheckAndMovePos(int distance, int xDir, int yDir, bool hasWrapAround)
    {
        int xCheck = coord.x + xDir * distance;
        if(xCheck < 0)
        {
            xCheck = 8 + xCheck;
        }
        if(xCheck >= 8)
        {
            xCheck = xCheck % 8;
        }
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
        else if (CheckIfCanProtect(xCheck, yCheck))
        {
            // Spot is on an enemy piece, return false to prevent from moving further
            if (ValidMoveInCheck(nextMove)) {
                possibleProtects.Add(new Vector2Int(xCheck, yCheck));
            }
            return false;
        }
        else
        {
            return false;
        }
    }
}
