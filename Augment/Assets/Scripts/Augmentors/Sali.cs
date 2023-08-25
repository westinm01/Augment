using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sali : Augmentor
{
    protected override void Awake()
    {
        GameObject managers = GameObject.FindGameObjectsWithTag("GameManager")[0];
        HoldingManager hm = managers.GetComponent<HoldingManager>();
        this.augmentor = hm.augmentorObjects[14];
        base.Awake();

    }
    public override void UseAugment()
    {
        ChessPiece cp = this.gameObject.GetComponent<ChessPiece>();
        //check if current coordinates are in the augmentedCoords
        if (cp.augmentedSpaces.Contains(cp.coord))
        {
            StartCoroutine(CanvasManager.Instance.AugmentorFlash(this));
        }
        
    }
}
