using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Alejandra : Augmentor
{
    private bool hasExtraLife = false;

    private void Awake()
    {
        canActivate = true;
    }

    public override void UseAugment()
    {
        if (canActivate)
        {
            Debug.Log("Use alejandra's augment");

            base.UseAugment();

            hasExtraLife = true;
            canActivate = false;
            GameManager.Instance.SwitchTeams();
        }
        
        // might want to do VFX and add a effect to the augmented piece to represent it has an extra life
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
            canActivate = true; // We might not want to do this for balance?
        }
        else
        {
            Debug.LogError("Tried to use an extra life when Alejandra did not have one.");
        }
    }
}
