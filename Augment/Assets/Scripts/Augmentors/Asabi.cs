using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asabi : Augmentor
{
    [SerializeField]
    int webTurns = 5;
    protected override void Start() {
        base.Start();
        canActivate = true;
        
    }

    public override void UseAugment()
    {
        StartCoroutine(CanvasManager.Instance.AugmentorFlash(this));

    }
}
