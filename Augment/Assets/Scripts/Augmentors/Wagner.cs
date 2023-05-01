using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wagner : Augmentor
{
    [SerializeField] int turnsLeft;
    [SerializeField] int cooldownTime;

    // Start is called before the first frame update
    protected override void Start() {
        base.Start();
        canActivate = true;
        turnsLeft = cooldownTime*2;
        targetPiece = null;
    }

    public void Cooldown(){
        if(turnsLeft <= 0){
            turnsLeft = cooldownTime;
            canActivate = true;
            GameManager.Instance.GetEventsManager().OnTurnEnd -= Cooldown;
        }else{
            turnsLeft--;
        }
    }
    
    public override void UseAugment()
    {   
        targetPiece = null;
        if(canActivate){
            Debug.Log("Can Activate");
            canActivate = false;
            StartCoroutine(UseRR());
        }else{
            Debug.Log("Can't activate :'3");
            
            //Play an animation??
        }
    }

    private IEnumerator UseRR(){
        //Piece Selection
        GameManager.Instance.GetInputManager().AugmentActivation(this, true);
        while(targetPiece == null){
            yield return new WaitForSeconds(0.1f);
        }        

        //RiskReward
        GameManager.Instance.GetEventsManager().OnTurnEnd += Cooldown;
        // GameManager.Instance.GetEventsManager().OnPieceEaten += targetCheck;
       

    }

    public void setSelection(GameObject piece){
        targetPiece = piece.GetComponent<ChessPiece>();
    }

    public void targetCheck(){
        if(!targetPiece.gameObject.activeSelf){
            turnsLeft = -1;

            //Place it back and move your piece back
        }
    }


}