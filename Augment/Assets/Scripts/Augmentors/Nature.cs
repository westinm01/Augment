using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nature : Augmentor
{
    // Start is called before the first frame update
    void Start()
    {
        name = "Fleur";
        backgroundColor = Color.green;
        augmentName = "Evergreen";
        augmentDesc = "When this piece eats an enemy piece, all enemy and ally statuses last an extra turn.";
        triggerVal = 3;
    }

    // Update is called once per frame
    public override void UseAugment()
    {
        GameObject managers = GameObject.FindGameObjectsWithTag("GameManager")[0];
        StatusManager sm = managers.GetComponent<StatusManager>();
        for (int i = 0; i < sm.bins.Length; i++)
        {
            for(int j = 0; j < sm.bins[i].Count; j++)
            {
                sm.bins[i][j].statusTimer += 2;
            }
        }
    }
}
