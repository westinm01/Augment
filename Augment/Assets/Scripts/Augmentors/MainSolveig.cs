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
    
        protected override void Awake()
    {
        managers = GameObject.FindGameObjectsWithTag("GameManager")[0];
        HoldingManager hm = managers.GetComponent<HoldingManager>();
        this.augmentor = hm.augmentorObjects[12];
        base.Awake();
    }

    void Start()
    {
        //get chess board
        gm = managers.GetComponent<GameManager>();
        bm = managers.GetComponent<BoardManager>();
        board = bm.GetBoard();
        cp = gameObject.GetComponent<ChessPiece>();
        movedTwice = false;
    }
    public override void UseAugment()
    {
        //TODO: Prompt for second move
        
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
        //TODO: Fix bug that allows you to move more than twice if you eat an enemy piece


    }

    public void ContinueGame()
    {
        Debug.Log("Game Continued...");
        movedTwice = false;
        bm.FreezeBoard(false);
        //pieces unfrozen

        
        //StartCoroutine(CanvasManager.Instance.AugmentorFlash(this));
        
    }
}
