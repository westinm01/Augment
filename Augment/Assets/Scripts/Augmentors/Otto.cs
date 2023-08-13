using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Otto : Augmentor
{
    
    void Start()
    {

    }
    public override void UseAugment()
    {
        StartCoroutine(CanvasManager.Instance.AugmentorFlash(this));
    }
}
