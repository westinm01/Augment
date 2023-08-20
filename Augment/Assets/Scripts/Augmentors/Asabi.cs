using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asabi : Augmentor
{
    public GameObject webTile;
    [SerializeField]
    int turnCount = 5;
    int lastCol = 0;
    int lastRow = 0;
    public GameObject managers;

    protected override void Start() {
        base.Start();
        canActivate = true;
        managers = GameObject.FindGameObjectsWithTag("GameManager")[0];
        webTile = managers.GetComponent<HoldingManager>().augmentorEffectObjects[3]; // get web effect
        
    }

    public override void UseAugment()
    {
        StartCoroutine(CanvasManager.Instance.AugmentorFlash(this));
        ChessPiece cp = gameObject.GetComponent<ChessPiece>();
        GameObject g = Instantiate(webTile, new Vector3(cp.lastCoord.x, -1 * cp.lastCoord.y, 0), Quaternion.identity);
       
        g.transform.parent = this.gameObject.transform.parent;
        BoardObject web = g.GetComponent<BoardObject>();
        g.GetComponent<SpriteRenderer>().sprite = webTile.GetComponent<SpriteRenderer>().sprite;
        StatusManager sm = FindObjectOfType<StatusManager>();
        //add web such that it doesn't block movement
        web.coord = cp.lastCoord;
        web.team = (cp.team);
        web.turnCount = turnCount;
        sm.boardObjects.Add(web);
        //BoardManager cb = FindObjectOfType<BoardManager>();
        //Debug.Log("Web coord: " + web.coord.x + ", " + web.coord.y);
        //cb.AddPiece(web, web.coord.x, web.coord.y);
    }
}
