using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sali : Augmentor
{
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
