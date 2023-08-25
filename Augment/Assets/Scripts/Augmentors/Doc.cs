using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doc : Augmentor
{
    public float enemyNextTurnTime = 5f;
    public GameObject managers;
    protected override void Awake()
    {
        //canActivate = true;
        managers = GameObject.FindGameObjectsWithTag("GameManager")[0];
        HoldingManager hm = managers.GetComponent<HoldingManager>();
        this.augmentor = hm.augmentorObjects[2];
        base.Awake();
    }
    public override void UseAugment()
    {
        StartCoroutine(CanvasManager.Instance.AugmentorFlash(this));
        GameManager.Instance.SetNextTurnTimer(enemyNextTurnTime);
    }
}
