using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ChessPiece : MonoBehaviour
{
    public Vector2Int coord;
    public List<Vector2Int> possibleSpaces = new List<Vector2Int>(); //calculated at the start of your turn.
    public List<Vector2Int> possibleEats = new List<Vector2Int>(); //calculated at the start of your turn.

    public bool team = true; //true if white, false if black. True by default.
    public Sprite blackSprite;
    public Sprite whiteSprite;
    public bool canJump = false; //setToTrue for Knight.
    public int statusTimer; //number of turns the current status lasts.
    Augmentation pieceAugmentation;
    public int status = 0; //to determine what status it has.
    

    // Start is called before the first frame update
    public void Start()
    {
        SpriteRenderer sr = gameObject.GetComponent<SpriteRenderer>();
        if (team) {     // piece is white team
            sr.sprite = whiteSprite;
        }
        else {
            sr.sprite = blackSprite;
        }

        // automatically finds coord on board using gameObject position
        coord.x = (int)gameObject.transform.position.x;
        coord.y = -(int)gameObject.transform.position.y;

        GameManager.Instance.board.AddPiece(this, coord.y, coord.x);
        GameManager.Instance.board.PrintBoard();    // for testing
        GetPossibleSpaces();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    virtual public void Move()
    {

    }

    virtual public void GetPossibleSpaces()
    {
        possibleSpaces.Clear();
        possibleEats.Clear();
    }

    protected bool CheckIfCanEat(int x, int y) {
        ChessPiece temp = GameManager.Instance.board.GetChessPiece(x, y);
        if (temp != null && temp.team != this.team) {
            return true;
        }
        else {
            return false;
        }
        
    }

    //Eating & CheckMate will be done by a game manager
}
