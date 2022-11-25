using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hiroshi : Augmentor
{
    // Start is called before the first frame update
    void Start()
    {
        hasPrompt = true;
        name = "Hiroshi";
        backgroundColor = Color.grey;
        augmentName = "Vanish";
        augmentDesc = "This piece can vanish from the board for 3 turns. After, it eats any piece on its space, but if an ally is on its space, this piece is discarded.";
        triggerVal = 2;
    }

    public override void UseAugment()
    {
        //
    }
}
