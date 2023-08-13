using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tahira : Augmentor
{
    //need to make statemachine
    //
    enum SkateState {init, onePawn, waitKnight, twoPawn, eatenKnight, waitPawn, pawnKnightpawn};
    SkateState currState;
    void Start()
    {
        
    }
    void Update()
    {
        //state transition may happen here...
    }
    public override void UseAugment()
    {

        StartCoroutine(CanvasManager.Instance.AugmentorFlash(this));
    }
}
