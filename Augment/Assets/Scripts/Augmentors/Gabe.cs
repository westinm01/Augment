using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gabe : Augmentor
{

    // // Start is called before the first frame update
    // void Start()
    // {
    //     name = "Gabe";
    //     backgroundColor = Color.yellow;
    //     augmentName = "Zappetizer";
    //     augmentDesc = "Upon eating an enemy piece, all adjacent enemy pieces are stunned for 2 turns.";
    //     triggerVal = 3;
    // }
    GameObject managers;
    public GameObject electricGameObject;

    protected override void Awake()
    {
        managers = GameObject.FindGameObjectsWithTag("GameManager")[0];
        HoldingManager hm = managers.GetComponent<HoldingManager>();
        this.augmentor = hm.augmentorObjects[5];
        base.Awake();

    }
    void Start()
    {
        managers = GameObject.FindGameObjectsWithTag("GameManager")[0];
        electricGameObject = managers.GetComponent<HoldingManager>().augmentorEffectObjects[1]; // get electric effect
    }
    
    public override void UseAugment()
    {
        bool stunned = false;
        ChessPiece cp = gameObject.GetComponent<ChessPiece>();
        Vector2Int spaceVal = cp.coord;
        
        
         
        BoardManager g = managers.GetComponent<BoardManager>();
        for (int i = -1; i <= 1; i++)
        {
            
            for (int j = -1; j <= 1; j++)
            {
                
                if (true)
                {
                    
                    Vector2Int spaceCheck = new Vector2Int(i,j);
                    spaceCheck += spaceVal;
                    
                    Debug.Log("Checking... (" + spaceCheck.y + ", " + spaceCheck.x + ")");
                    if (spaceCheck.x >= 0 && spaceCheck.x < 8 && spaceCheck.y >= 0 && spaceCheck.y < 8)
                    {
                        ChessPiece possibleEnemy = g.GetChessPiece(spaceCheck.x, spaceCheck.y);
                        
                        if (possibleEnemy != null)
                        {
                            if (possibleEnemy.team != cp.team)
                            {
                                possibleEnemy.status = 2; //this turn and the next.
                                possibleEnemy.statusTimer = 2; //restored at the second chance the opponent has to move.
                                Debug.Log("ZAPPETIZER: Stunned enemy at (" + spaceCheck.x + ", " +spaceCheck.y + ")");
                                StatusManager sm = managers.GetComponent<StatusManager>();
                                sm.ApplyStatus(possibleEnemy, 0);
                                stunned = true;
                                GameObject effect = Instantiate(electricGameObject, possibleEnemy.transform.localPosition, Quaternion.identity);
                                sm.otherObjects.Add(effect);
                                effect.SetActive(true);
                            }
                        }
                        
                    }
                }    
            }
        }
        if(stunned)
        {
            StartCoroutine(CanvasManager.Instance.AugmentorFlash(this));
        }
        
    }
}
