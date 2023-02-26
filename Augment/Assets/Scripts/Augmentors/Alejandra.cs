using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Alejandra : Augmentor
{
    bool extraLife = false;

    public override void UseAugment()
    {
        if (canActivate)
        {
            extraLife = true;
            canActivate = false;
        }
    }
}
