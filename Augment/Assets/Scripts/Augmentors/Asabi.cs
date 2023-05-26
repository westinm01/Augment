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

    protected override void Start() {
        base.Start();
        canActivate = true;
        
    }

    public override void UseAugment()
    {
        StartCoroutine(CanvasManager.Instance.AugmentorFlash(this));
        ChessPiece cp = gameObject.GetComponent<ChessPiece>();
        GameObject g = Instantiate(webTile, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, -1), Quaternion.identity);
        //GameObject g = Instantiate(fireBlock, new Vector3(0,0,-1), Quaternion.identity);
        g.transform.parent = this.gameObject.transform.parent;
        BoardObject web = g.GetComponent<BoardObject>();
        //fire.coord = new Vector2(GetComponent<ChessPiece>().coord.x, GetComponent<ChessPiece>().coord.y);
        StatusManager sm = FindObjectOfType<StatusManager>();
        sm.boardObjects.Add(web);
        web.coord = cp.coord;
        web.team = !(cp.team);
        web.turnCount = turnCount;
        BoardManager cb = FindObjectOfType<BoardManager>();
        Debug.Log("Web coord: " + web.coord.x + ", " + web.coord.y);
        cb.AddPiece(web, web.coord.x, web.coord.y);
    }
}
