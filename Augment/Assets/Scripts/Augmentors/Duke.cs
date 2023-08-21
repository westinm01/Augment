using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Duke : Augmentor
{
    public int turnsToEat = 4;
    GameObject managers;
    BoardManager bm;
    Player enemyPlayer;
    public KingPiece enemyKing;
    // Start is called before the first frame update
    void Start()
    {
        managers = GameObject.FindGameObjectsWithTag("GameManager")[0];
        bm = managers.GetComponent<BoardManager>();
        enemyPlayer = GameManager.Instance.GetPlayer(!this.gameObject.GetComponent<ChessPiece>().team);
        enemyKing = enemyPlayer.GetKingPiece();
    }
    public override void UseAugment()
    {
        
        if(canActivate)
        {
            canActivate = false;
            Debug.Log("Can Activate Duke");
            targetPiece = null;
            StartCoroutine(SelectTargetPiece());

        }
    }

    private void WaitForEat()
    {
        if(turnsToEat > 0)
        {
            //check if the targetpiece ate anything
            if(bm.lastEater == targetPiece);
            {
                //take control of opponent's king
                Debug.Log("Move King!");
                bm.FreezeBoard(true); //freeze everything; wait for player to move enemy king.

                //next 5 lines are a little shakey
                //enemyKing.canMove = true;
                //enemyKing.team = !enemyKing.team;
                GameManager.Instance.GetEventsManager().OnTurnEnd -= WaitForEat;
                bm.FreezeBoard(false);
                //enemyKing.team = !enemyKing.team;
                
            }
            turnsToEat--;
        }
        else
        {
            GameManager.Instance.GetEventsManager().OnTurnEnd -= WaitForEat;
            
        }
    }

    private IEnumerator SelectTargetPiece()
    {
        //how do i select a targetpiece


        GameManager.Instance.GetInputManager().AugmentActivation(this, false);
        while(targetPiece == null){
            yield return new WaitForSeconds(0.1f);
        }        
        StartCoroutine(CanvasManager.Instance.AugmentorFlash(this));
        GameManager.Instance.GetEventsManager().OnTurnEnd += WaitForEat;
        //GameManager.Instance.board.SwapPiece(augmentPiece, targetPiece);
        GameManager.Instance.SwitchTeams(); //Switch turns to end turn
    }

    public void setSelection(GameObject piece){
        targetPiece = piece.GetComponent<ChessPiece>();
    }
}
