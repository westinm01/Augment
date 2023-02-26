using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : Augmentor
{
    private int counter = 7;
    public override void UseAugment()
    {
        counter--;
        if (counter == 0) {
            Destroy(this.gameObject);
        }
    }
}
