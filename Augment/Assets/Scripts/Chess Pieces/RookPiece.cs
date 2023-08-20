using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RookPiece : ChessPiece
{
    public bool canCastle;
    private bool jumped = false;
    private bool wrapAround = false;
    private void Awake()
    {
        if(TryGetComponent<Augmentor>(out Augmentor aug))
        {
            pieceAugmentor = aug;
            if (this.gameObject.transform.GetChild(0).TryGetComponent<SpriteRenderer>(out SpriteRenderer s))
            {
                s.color = pieceAugmentor.augmentor.backgroundColor;
            }
        }
        else
        {
            this.gameObject.transform.GetChild(0).gameObject.SetActive(false);
        }
        canCastle = true;
        SetPieceValue(MiniMaxAI.ROOK_VAL);
        SetPieceChar(StockfishAI.ROOK_CHAR);
    }
    //how do i make it so that if this piece has the Sali component, it can jump over ally pieces?
    public override void GetPossibleSpaces()
    {
        base.GetPossibleSpaces();
        /* TO DO:
         * restrict movement if piece blocking (cant tp behind piece)
         * implement a break statement if a piece is blocking to 
         * not add the rest to possible spaces
         */

        // Get all spaces to right
        int right = 1;
        int left = -1;
        int up = 1;
        int down = -1;

        if (TryGetComponent<Otto>(out Otto ottoInstance))
        {
            wrapAround = false;
            for (int i = coord.x + 1; i <= GameManager.Instance.board.getWidth(); i = (i + 1)%8)
            {
                if (!CheckHorizontalAndVertical(i%8, coord.y)) {
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
                if(wrapAround)
                {
                    augmentedSpaces.Add(new Vector2Int(i, coord.y));
                }
                
            }
        }

        else
        {
            jumped = false;
            for (int i = coord.x + 1; i < GameManager.Instance.board.getWidth(); i++)
            {
                if (!CheckHorizontalAndVertical(i, coord.y)) {
                    break;
                }
                if(jumped)
                {
                    augmentedSpaces.Add(new Vector2Int(i, coord.y));
                }
            }
            // Get all spaces to left
            jumped = false;
            for (int i = coord.x - 1; i >= 0; i--)
            {
                if (!CheckHorizontalAndVertical(i, coord.y)) {
                    break;
                }
                if(jumped)
                {
                    augmentedSpaces.Add(new Vector2Int(i, coord.y));
                }
            }
        }
        // Get all spaces up
        jumped = false;
        for (int i = coord.y + 1; i < GameManager.Instance.board.getHeight(); i++)
        {
            if (!CheckHorizontalAndVertical(coord.x, i)) {
                break;
            }
            if(jumped)
            {
                augmentedSpaces.Add(new Vector2Int(coord.x, i));
            }
        }
        // Get all spaces down
        jumped = false;
        for (int i = coord.y - 1; i >= 0; i--)
        {
            if (!CheckHorizontalAndVertical(coord.x, i)) {
                break;
            }
            if(jumped)
                {
                    augmentedSpaces.Add(new Vector2Int(coord.x, i));
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
        else if(TryGetComponent<Sali>(out Sali sali))
        {
            Debug.Log("Has Sali");
            //check if x and y are occupied by an ally
            ChessPiece temp = GameManager.Instance.board.GetChessPiece(x, y);
            if (GameManager.Instance.board.InBounds(x, y) && temp != null && temp.team == this.team)
            {
                jumped = true;
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
            if(wrapAround)
            {
                augmentedSpaces.Add(nextMove);
            }
            return false;
        }
        else if (CheckIfCanProtect(x, y))
        {
            // Spot is on an enemy piece, return false to prevent from moving further
            if (ValidMoveInCheck(nextMove)) {
                possibleProtects.Add(new Vector2Int(x, y));
                if(wrapAround)
                {
                    augmentedSpaces.Add(nextMove);
                }
            }
            
            return false;
        }
        
        else {
            return false;
        }
    }

    public void UpdateCastle(){
        canCastle = false;
    }
}
