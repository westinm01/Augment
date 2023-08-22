using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lucy : Augmentor
{
    public GameObject fireBlock;
    public int turnCount = 2;
    int lastCol = 0;
    int lastRow = 0;
    private GameObject managers;

    void Start()
    {
        managers = GameObject.FindGameObjectsWithTag("GameManager")[0];
        fireBlock = managers.GetComponent<HoldingManager>().augmentorEffectObjects[0]; // get fire effect
    }
    public override void UseAugment()
    {
        StartCoroutine(CanvasManager.Instance.AugmentorFlash(this));

        ChessPiece cp = gameObject.GetComponent<ChessPiece>();
        
        List<Vector2Int> spaces = new List<Vector2Int>();
        BoardManager bm = managers.GetComponent<BoardManager>();
        //check if cp is of type KnightPiece

        if (cp.GetType() != typeof(KnightPiece))
        {
            spaces = bm.GetSpacesInbetween(cp.lastCoord, cp.coord);
        }

        spaces.Add(cp.lastCoord);
        StatusManager sm = FindObjectOfType<StatusManager>();
        foreach(Vector2Int coords in spaces)
        {
            GameObject g = Instantiate(fireBlock, new Vector3(coords.x, -1 * coords.y, -10), Quaternion.identity);//how do i instantiate this such that it is not in front of UI?
            g.transform.parent = this.gameObject.transform.parent;
            
            FireBlock fire = g.GetComponent<FireBlock>();
            
            sm.boardObjects.Add(fire);
            fire.coord = coords;
            fire.team = !(cp.team);
            fire.turnCount = turnCount;
            BoardManager cb = FindObjectOfType<BoardManager>();
            Debug.Log("Fire coord: " + fire.coord.x + ", " + fire.coord.y);
            cb.AddPiece(fire, fire.coord.x, fire.coord.y);

        }
    }
}
