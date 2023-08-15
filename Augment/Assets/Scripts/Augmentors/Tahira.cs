using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tahira : Augmentor
{
    //need to make statemachine
    //
    enum SkateState {init, onePawn, pawnKnight, pawnKnightpawn};
    SkateState currState = SkateState.init;

    Player piecePlayer;
    Player enemyPlayer;
    void Start()
    {
        piecePlayer = GameManager.Instance.GetPlayer(this.gameObject.GetComponent<ChessPiece>().team);
        enemyPlayer = GameManager.Instance.GetPlayer(!this.gameObject.GetComponent<ChessPiece>().team);
        
    }

    public override void UseAugment()
    {
        //transition states
        switch(currState)
        {
            case SkateState.init:
            

                if(piecePlayer.capturedPieces[piecePlayer.capturedPieces.Count - 1].GetType() == typeof(PawnPiece))
                {
                    //assign every Tahira piece's state at once!
                    currState = SkateState.onePawn;
                    Debug.Log("TAHIRA: 1 pawn down");
                }
            
            break;

            case SkateState.onePawn:
            
                if(piecePlayer.capturedPieces[piecePlayer.capturedPieces.Count - 1].GetType() == typeof(PawnPiece))
                {
                    
                    //instantiate a new pawn and stay in this state
                    Debug.Log("Making Pawn!");
                    StartCoroutine(CanvasManager.Instance.AugmentorFlash(this));
                }

                else if(piecePlayer.capturedPieces[piecePlayer.capturedPieces.Count - 1].GetType() == typeof(KnightPiece))
                {
                    currState = SkateState.pawnKnight;
                    Debug.Log("TAHIRA: 1 pawn down, 1 knight down");

                }
                else
                {
                    currState = SkateState.init;
                }
            
            break;

            case SkateState.pawnKnight:

                if(piecePlayer.capturedPieces[piecePlayer.capturedPieces.Count - 1].GetType() == typeof(PawnPiece))
                {
                    Debug.Log("Making Knight!");
                    //instantiate a new knight, and go to one Pawn
                    currState = SkateState.onePawn;
                    StartCoroutine(CanvasManager.Instance.AugmentorFlash(this));
                }
                else{
                    currState = SkateState.init;
                }

            break;

        }

            

        
        
    }
}
