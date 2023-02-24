using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doc : Augmentor
{
    public float enemyNextTurnTime = 5f;
    public override void UseAugment()
    {
        StartCoroutine(CanvasManager.Instance.AugmentorFlash(this));
        GameManager.Instance.SetNextTurnTimer(enemyNextTurnTime);
    }
}
