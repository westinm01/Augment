using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Alejandra : Augmentor
{
    private bool hasExtraLife = false;

    protected override void Awake()
    {
        base.Awake();
        canActivate = true;
    }

    public override void UseAugment()
    {
        if (canActivate)
        {
            base.UseAugment();

            if (hasExtraLife)
            {
                UseExtraLife();
            }
            else
            {
                GainExtraLife();
            }
        }

        // might want to do VFX and add a effect to the augmented piece to represent it has an extra life
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

    public void UseExtraLife()
    {
        if (hasExtraLife)
        {
            hasExtraLife = false;
            
            // back to activate as turn --We might not want to do this for balance?
            GameManager.Instance.GetTriggerManager().RemoveFromBin(gameObject.GetComponent<ChessPiece>(), triggerVal);
            triggerVal = 2;
            GameManager.Instance.GetTriggerManager().AddToBin(gameObject.GetComponent<ChessPiece>(), triggerVal);
            GameManager.Instance.SwitchTeams();
        }
        else
        {
            Debug.LogError("Tried to use an extra life when Alejandra did not have one.");
        }
    }
}
