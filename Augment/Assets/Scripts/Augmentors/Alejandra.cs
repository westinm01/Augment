using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Alejandra : Augmentor
{
    private bool hasExtraLife = true;
    public GameObject lifesaver;

    protected override void Awake()
    {
        base.Awake();
        canActivate = true;
    }

    public override void UseAugment()
    {
        base.UseAugment();

        if (canActivate)
        {
            UseExtraLife();
            StartCoroutine(CanvasManager.Instance.AugmentorFlash(this));
            Instantiate(lifesaver, this.gameObject.transform.position, Quaternion.identity);
        }
        

        // might want to do VFX and add a effect to the augmented piece to represent it has an extra life
    }

    public void UseExtraLife()
    {
        hasExtraLife = false;
        canActivate = false;
        GameManager.Instance.board.CancelMovement();
    }

    private void GainExtraLife()
    {
        hasExtraLife = true;

        // now we only want to activate while eaten
        GameManager.Instance.GetTriggerManager().RemoveFromBin(gameObject.GetComponent<ChessPiece>(), triggerVal);
        triggerVal = 4;         
        GameManager.Instance.GetTriggerManager().AddToBin(gameObject.GetComponent<ChessPiece>(), triggerVal);
        
        GameManager.Instance.SwitchTeams();
    }

    public bool GetHasExtraLife()
    {
        return hasExtraLife;
    }

}
