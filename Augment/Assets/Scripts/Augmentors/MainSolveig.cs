using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSolveig : Augmentor
{
    private ChessBoard board;
    private ChessPiece cp;
    private BoardManager bm;
    private GameManager gm;
    GameObject managers;

    public bool movedTwice = false;
    
    void Start()
    {
        //get chess board
        managers = GameObject.FindGameObjectsWithTag("GameManager")[0];
        gm = managers.GetComponent<GameManager>();
        bm = managers.GetComponent<BoardManager>();
        board = bm.GetBoard();
        cp = gameObject.GetComponent<ChessPiece>();
    }
    public override void UseAugment()
    {
        //TODO: Prompt for second move
        //TODO: Only allow second move if there are available spaces
        
        if(!movedTwice && (cp.possibleSpaces.Count > 1 || cp.possibleEats.Count > 0 || cp.possibleProtects.Count > 1))
        {
            Debug.Log(cp.possibleSpaces.Count);
            MoveTwice();
        }
        else{
            ContinueGame();
        }
        
        
    }

    public void MoveTwice()
    {
        movedTwice = true;
        
        bm.FreezeBoard(true);

        cp.canMove = true;

        //don't flash until second move
        gm = managers.GetComponent<GameManager>();
        if(gm.currPlayer == gm.whitePlayer)
        {
            gm.currPlayer =gm.blackPlayer;
        }
        else{
            gm.currPlayer =gm.whitePlayer;
        }
        StartCoroutine(CanvasManager.Instance.AugmentorFlash(this));


    }

    public void ContinueGame()
    {
        movedTwice = false;
        bm.FreezeBoard(false);
        //pieces unfrozen

        
        //StartCoroutine(CanvasManager.Instance.AugmentorFlash(this));
        
    }
}
