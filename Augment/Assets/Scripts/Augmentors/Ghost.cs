using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : Augmentor
{
    private int counter = 7;
    public GameObject managers;
    protected override void Awake()
    {
        managers = GameObject.FindGameObjectsWithTag("GameManager")[0];
        HoldingManager hm = managers.GetComponent<HoldingManager>();
        this.augmentor = hm.augmentorObjects[9];
        base.Awake();

    }
    public override void UseAugment()
    {
        counter--;
        if (counter == 0) {
            this.gameObject.SetActive(false);
        }
    }
}
