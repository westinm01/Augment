using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Duke : Augmentor
{
    public int turnsToEat = 4;
    GameObject managers;
    BoardManager bm;
    Player enemyPlayer;
    GameManager gm;
    public KingPiece enemyKing;
    GameObject noteEffect;

    protected override void Awake()
    {
        managers = GameObject.FindGameObjectsWithTag("GameManager")[0];
        HoldingManager hm = managers.GetComponent<HoldingManager>();
        this.augmentor = hm.augmentorObjects[3];
        base.Awake();

    }
    // Start is called before the first frame update
    void Start()
    {
        //managers = GameObject.FindGameObjectsWithTag("GameManager")[0];
        gm = managers.GetComponent<GameManager>();
        noteEffect = managers.GetComponent<HoldingManager>().augmentorEffectObjects[4]; //get note effect
        bm = managers.GetComponent<BoardManager>();
        enemyPlayer = GameManager.Instance.GetPlayer(!this.gameObject.GetComponent<ChessPiece>().team);
        enemyKing = enemyPlayer.GetKingPiece();
    }
    public override void UseAugment()
    {
        
        if(canActivate)
        {
            bm.lastEater = null;
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
            bool ready = bm.lastEater !=null;
            if(ready && bm.lastEater == targetPiece)
            {
                //Debug.Log(bm.lastEater != null);
                //take control of opponent's king
                Debug.Log("Move King!");
                bm.FreezeBoard(true); //freeze everything; wait for player to move enemy king.
                if(gm.currPlayer == gm.whitePlayer)
                {
                    gm.currPlayer =gm.blackPlayer;
                }
                else{
                    gm.currPlayer =gm.whitePlayer;
                }
                enemyPlayer.GetKingPiece().team = !enemyPlayer.GetKingPiece().team;
                enemyPlayer.GetKingPiece().canMove = true;
                //next 5 lines are a little shakey
                //enemyKing.canMove = true;
                //enemyKing.team = !enemyKing.team;
                Destroy(targetPiece.gameObject.transform.GetChild(1).gameObject);
                GameManager.Instance.GetEventsManager().OnTurnEnd -= WaitForEat;
                GameManager.Instance.GetEventsManager().OnTurnEnd += ContinueGame;
                
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

        GameManager.Instance.GetInputManager().AugmentActivation(this, false);
        while(targetPiece == null){
            yield return new WaitForSeconds(0.1f);
        }
        GameObject note = Instantiate(noteEffect, targetPiece.transform.position, Quaternion.identity);
        note.transform.parent = targetPiece.transform;
        StartCoroutine(CanvasManager.Instance.AugmentorFlash(this));
        GameManager.Instance.GetEventsManager().OnTurnEnd += WaitForEat;
        //GameManager.Instance.board.SwapPiece(augmentPiece, targetPiece);
        GameManager.Instance.SwitchTeams(); //Switch turns to end turn
    }

    public void setSelection(GameObject piece){
        targetPiece = piece.GetComponent<ChessPiece>();
    }
    public void ContinueGame()
    {
        bm.FreezeBoard(false);
        //pieces unfrozen
        enemyPlayer.GetKingPiece().team = !enemyPlayer.GetKingPiece().team;
        
        StartCoroutine(CanvasManager.Instance.AugmentorFlash(this));
        GameManager.Instance.GetEventsManager().OnTurnEnd -= ContinueGame;
        
    }
}
