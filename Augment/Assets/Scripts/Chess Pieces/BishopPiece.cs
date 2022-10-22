using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class BishopPiece : ChessPiece
{
    void Start()
    {
        GameManager.Instance.board.AddPiece(this, coord.y, coord.x);
        GameManager.Instance.board.PrintBoard();
    }

    void Update()
    {
        
    }
    public override void GetPossibleSpaces()
    {
        int right = 1;
        int left = -1;
        int up = 1;
        int down = -1;

        // Get all spaces to top right
        for (int i = 1; i <= (GameManager.Instance.board.getWidth() - coord.x); i++)
        {
            MoveAndCheckPos(i, right, up);
        }
        // Get all spaces to top left
        for (int i = 1; i <= coord.x; i++)
        {
            MoveAndCheckPos(i, left, up);
        }
        // Get all spaces bottom right
        for (int i = 1; i <= (GameManager.Instance.board.getHeight() - coord.y); i++)
        {
            MoveAndCheckPos(i, right, down);
        }
        // Get all spaces bottom left
        for (int i = 1; i <= coord.y; i++)
        {
            MoveAndCheckPos(i, left, down);
        }
    }

    private void MoveAndCheckPos(int distance, int xDir, int yDir)
    {
        int xCheck = xDir * coord.x + distance;
        int yCheck = yDir * coord.y + distance;

        bool validX = (xCheck >= 0) && (xCheck < GameManager.Instance.board.getWidth());
        bool validY = (yCheck >= 0) && (yCheck < GameManager.Instance.board.getHeight());

        if (validX && validY)
        {
            // Spot is open, add it to possible spaces
            possibleSpaces.Add(new Vector2Int(xCheck, yCheck));
        }
    }
}
