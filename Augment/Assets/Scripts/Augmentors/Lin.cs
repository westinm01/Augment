using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lin : Augmentor
{

    [SerializeField] int turnsLeft;
    [SerializeField] int cooldownTime;
    [SerializeField] ChessPiece selectedPiece;
    

    // Start is called before the first frame update
    protected override void Start() {
        base.Start();
        canActivate = true;
        turnsLeft = cooldownTime*2;
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
        Debug.Log("Used");
        if(canActivate){
            Debug.Log("Can Activate");
            canActivate = false;
            GameManager.Instance.GetEventsManager().OnTurnEnd += Cooldown;
            GameManager.Instance.board.SwapPiece(augmentPiece, selectedPiece);
            GameManager.Instance.SwitchTeams(); //For now, just switch turns. I 'll find a way to get it to do it after the warp in a bit
        }else{
            Debug.Log("Can't activate :'3");
            //Play an animation??
        }
       //GameManager.Instance.GetEventsManager().OnTurnEnd += Wait;
    }
}
