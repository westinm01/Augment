using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gabe : Augmentor
{

    // Start is called before the first frame update
    void Start()
    {
        name = "Gabe";
        backgroundColor = Color.yellow;
        augmentName = "Zappetizer";
        augmentDesc = "Upon eating an enemy piece, all adjacent enemy pieces are stunned for 2 turns.";
        triggerVal = 3;
    }

    // Update is called once per frame
    public override void UseAugment()
    {
        ChessPiece cp = gameObject.GetComponent<ChessPiece>();
        Vector2Int spaceVal = cp.coord;
        ChessBoard board = FindObjectOfType<ChessBoard>();
        for(int i = -1; i < 1; i++)
        {

            for(int j = -1; j <1; j++)
            {
                if(i!= j)
                {
                    Vector2Int spaceCheck = new Vector2Int(i,j);
                    spaceCheck +=spaceVal;
                    if(spaceCheck.x > 0 && spaceCheck.x < 7 && spaceCheck.y < 0 && spaceCheck.y < 7)
                    {
                        ChessPiece possibleEnemy = board.GetPiece(spaceCheck.x, spaceCheck.y);
                        if(possibleEnemy.team != cp.team)
                        {
                            possibleEnemy.status = 1;
                            possibleEnemy.statusTimer = 2; //restored at the second chance the opponent has to move.
                            Debug.Log("Stunned enemy at (" + spaceCheck.x + ", " +spaceCheck.y + ")");

                        }
                        
                    }
                }    
            }
        }
    }
}