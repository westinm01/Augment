using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lin : Augmentor
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
            StartCoroutine(UseWarp());
        }else{
            Debug.Log("Can't activate :'3");
            
            //Play an animation??
        }
    }

    private IEnumerator UseWarp(){
        //Piece Selection
        GameManager.Instance.GetInputManager().AugmentActivation(this, false);
        while(targetPiece == null){
            yield return new WaitForSeconds(0.1f);
        }        

        //Swap + cooldown
        GameManager.Instance.GetEventsManager().OnTurnEnd += Cooldown;
        GameManager.Instance.board.SwapPiece(augmentPiece, targetPiece);
        GameManager.Instance.SwitchTeams(); //Switch turns to end turn

    }

    public void setSelection(GameObject piece){
        targetPiece = piece.GetComponent<ChessPiece>();
    }
}
