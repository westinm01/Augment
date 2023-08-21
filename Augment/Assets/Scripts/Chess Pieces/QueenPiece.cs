using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueenPiece : ChessPiece
{
    private bool wrapAround = false;
    private bool jumped = false;
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
            for (int i = coord.x + 1; i <= GameManager.Instance.board.getWidth(); i = (i + 1)%8)
            {
                if (!CheckHorizontalAndVertical(i, coord.y)) {
                    break;
                }
                if(i == 0)
                {
                    wrapAround = true;
                }
                if(wrapAround){
                    augmentedSpaces.Add(new Vector2Int(i%8, coord.y));
                }
            }
            // Get all spaces to left
            wrapAround = false;
            for (int i = coord.x - 1; i >= -1; i--)
            {
                if(i < 0)
                {
                    i = 7;
                    wrapAround = true;
                }
                if (!CheckHorizontalAndVertical(i, coord.y)) {
                    break;
                }
                
            }

                // Get all spaces up
            wrapAround = false;
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

            wrapAround = false;
            //top right
            for (int i = 1; i <= 7; i++)
            {
                if(!CheckAndMovePos(i, right, up, true))
                {
                    break;
                }
            }
            wrapAround = false;
            // Get all spaces to top left
            for (int i = 1; i <= 7; i++)
            {
                if(!CheckAndMovePos(i, left, up, true))
                {
                    break;
                }
            }
            wrapAround = false;
            // Get all spaces top right
            for (int i = 1; i <= (GameManager.Instance.board.getHeight()); i++)
            {
                if(!CheckAndMovePos(i, right, down, true))
                {
                    break;
                }
            }
            wrapAround = false;
            // Get all spaces bottom left
            for (int i = 1; i <= 7; i++)
            {
                if(!CheckAndMovePos(i, left, down, true))
                {
                    break;
                }
            }
            return;
        }

        // Get all spaces to the right
        jumped = false;
        for (int i = coord.x + 1; i < GameManager.Instance.board.getWidth(); i++)
        {
            if (!CheckHorizontalAndVertical(i, coord.y)) {
                break;
            }
        }
        // Get all spaces to left
        jumped = false;
        for (int i = coord.x - 1; i >= 0; i--)
        {
            if (!CheckHorizontalAndVertical(i, coord.y)) {
                break;
            }
        }
        // Get all spaces up
        jumped = false;
        for (int i = coord.y + 1; i < GameManager.Instance.board.getHeight(); i++)
        {
            if (!CheckHorizontalAndVertical(coord.x, i)) {
                break;
            }
        }
        // Get all spaces down
        jumped = false;
        for (int i = coord.y - 1; i >= 0; i--)
        {
            if (!CheckHorizontalAndVertical(coord.x, i)) {
                break;
            }
        }


        

        // Get all spaces to bottom right
        jumped = false;
        for (int i = 1; i <= (GameManager.Instance.board.getWidth() - coord.y); i++)
        {
            if(!CheckAndMovePos(i, right, up))
            {
                break;
            }
        }
        // Get all spaces to top left
        jumped = false;
        for (int i = 1; i <= coord.x; i++)
        {
            if(!CheckAndMovePos(i, left, up))
            {
                break;
            }
        }
        // Get all spaces top right
        jumped = false;
        for (int i = 1; i <= (GameManager.Instance.board.getHeight() - coord.x); i++)
        {
            if(!CheckAndMovePos(i, right, down))
            {
                break;
            }
        }
        // Get all spaces bottom left
        jumped = false;
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

        if (possibleMove.x == currX || possibleMove.y == currY) {
            return true;
        }

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
                possibleSpaces.Add(new Vector2Int(xCheck, yCheck));
                if(jumped)
                {
                    augmentedSpaces.Add(nextMove);
                }
            }
            return true;
        }
        else if(TryGetComponent<Sali>(out Sali sali))
        {
            if(!GameManager.Instance.board.InBounds(xCheck, yCheck))
            {
                return false;
            }
            //check if x and y are occupied by an ally
            ChessPiece temp = GameManager.Instance.board.GetChessPiece(xCheck, yCheck);
            if (GameManager.Instance.board.InBounds(xCheck, yCheck) && temp != null && temp.team == this.team)
            {
                jumped = true;
                return true;
            }
            else if (GameManager.Instance.board.InBounds(xCheck, yCheck) && temp != null && temp.team != this.team)
            {
                if(jumped)
                {
                    augmentedSpaces.Add(nextMove);
                }
                possibleEats.Add(nextMove);
                return false;
            }
            else
            {
                if (ValidMoveInCheck(nextMove))
                {
                    if(jumped)
                    {
                        augmentedSpaces.Add(nextMove);
                    }
                    possibleSpaces.Add(nextMove);
                }
                return true;
            }
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


    private bool CheckAndMovePos(int distance, int xDir, int yDir, bool hasWrapAround)
    {
        int xCheck = coord.x + xDir * distance;
        if(xCheck < 0)
        {
            xCheck = 8 + xCheck;
            wrapAround = true;
        }
        if(xCheck >= 8)
        {
            xCheck = xCheck % 8;
            wrapAround = true;
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
                if(wrapAround)
                {
                    augmentedSpaces.Add(nextMove);
                }
            }
            return true;
        }
        else if (CheckIfCanEat(xCheck, yCheck))
        {
            // Spot is on an enemy piece, return false to prevent from moving further
            if (ValidMoveInCheck(nextMove)) {
                possibleEats.Add(new Vector2Int(xCheck, yCheck));
                if(wrapAround)
                {
                    augmentedSpaces.Add(nextMove);
                }
            }
            return false;
        }
        else if (CheckIfCanProtect(xCheck, yCheck))
        {
            // Spot is on an enemy piece, return false to prevent from moving further
            if (ValidMoveInCheck(nextMove)) {
                if(wrapAround)
                {
                    augmentedSpaces.Add(nextMove);
                }
                possibleProtects.Add(new Vector2Int(xCheck, yCheck));
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
                if(jumped)
                {
                    augmentedSpaces.Add(nextMove);
                }
            }
            return true;
        }
        
        else if(TryGetComponent<Sali>(out Sali sali))
        {
            //Debug.Log("Has Sali");
            //check if x and y are occupied by an ally
            ChessPiece temp = GameManager.Instance.board.GetChessPiece(x, y);
            if (GameManager.Instance.board.InBounds(x, y) && temp != null && temp.team == this.team)
            {
                jumped = true;
                augmentedSpaces.Add(nextMove);
                return true;
            }
            else if (GameManager.Instance.board.InBounds(x, y) && temp != null && temp.team != this.team)
            {
                possibleEats.Add(nextMove);
                if(jumped)
                {
                    augmentedSpaces.Add(nextMove);
                }
                return false;
            }
            else
            {
                if (ValidMoveInCheck(nextMove))
                {
                    possibleSpaces.Add(nextMove);
                    if(jumped)
                    {
                        augmentedSpaces.Add(nextMove);
                    }
                }
                return true;
            }
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
