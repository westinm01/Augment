using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnPiece : ChessPiece
{
    public int direction = 1;   // 1 for moving up, -1 for moving down

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.board.AddPiece(this, coord.y, coord.x);
        GameManager.Instance.board.PrintBoard();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void GetPossibleSpaces()
    {
        possibleSpaces.Clear();
        int nextY = coord.y + direction;
        // If the pawn is in the last square, can't move further
        // Shouldnt be possible(pawn should evolve) but just in case
        if (nextY < GameManager.Instance.board.getHeight() && nextY >= 0)
        {
            possibleSpaces.Add(new Vector2Int(coord.x, nextY));
        }

        // If pawn is in starting position
        if ((coord.y == 1 && direction > 0) || (coord.y == GameManager.Instance.board.getHeight() - 2 && direction < 0))
        {
            possibleSpaces.Add(new Vector2Int(coord.x, nextY + direction));
        }
    }
}
