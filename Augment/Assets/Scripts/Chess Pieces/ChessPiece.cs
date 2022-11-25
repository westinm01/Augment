using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ChessPiece : MonoBehaviour
{
    public Vector2Int coord;
    public List<Vector2Int> possibleSpaces = new List<Vector2Int>(); //calculated at the start of your turn.
    public List<Vector2Int> possibleEats = new List<Vector2Int>(); //calculated at the start of your turn.
    public List<Vector2Int> possibleProtects = new List<Vector2Int>();

    public bool team = true; //true if white, false if black. True by default.
    public bool isVisible = true;
    public Sprite blackSprite;
    public Sprite whiteSprite;
    public bool canJump = false; //setToTrue for Knight.
    public int statusTimer; //number of turns the current status lasts.
    public Augmentor pieceAugmentor;
    public int status = 0; //to determine what status it has.
    public bool canMove = true;
    protected Player thisPlayer;
    

    // Start is called before the first frame update
    public void Start()
    {
        thisPlayer = GameManager.Instance.GetPlayer(team);
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
        possibleProtects.Clear();
    }

    /// <summary>
    /// Calculates if a piece can attack a square or not
    /// </summary>
    /// <param name="space"> square to check </param>
    /// <returns></returns>
    public virtual bool CanAttack(Vector2Int space) {
        foreach (Vector2Int possibleMove in possibleSpaces) {
            if (space == possibleMove) {
                return true;
            }
        }

        foreach (Vector2Int possibleEat in possibleEats) {
            if (space == possibleEat) {
                return true;
            }
        }

        return false;
    }

    public virtual bool IsProtected() {
        foreach (ChessPiece piece in thisPlayer.playerPieces) {
            foreach (Vector2Int possibleProtect in piece.possibleProtects) {
                if (possibleProtect == this.coord) {
                    return true;
                }
            }
        }

        return false;
    }

    // Used by king piece to prevent from moving in check
    public virtual bool InPath(Vector2Int possibleMove) {
        return false;
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

    protected bool CheckIfCanProtect(int x, int y) {
        ChessPiece temp = GameManager.Instance.board.GetChessPiece(x, y);
        if (temp != null && temp.team == this.team) {
            return true;
        }
        else {
            return false;
        }
    }

    protected bool CanBlock(Vector2Int possibleMove, List<Vector2Int> vector2Ints) {
        foreach (Vector2Int v in vector2Ints) {
            if (possibleMove == v) {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    ///  If player is in check, piece should only be able to move in a space that blocks the check
    /// </summary>
    /// <param name="possibleMove"> Coordinate to move to</param>
    /// <returns></returns>
    protected bool ValidMoveInCheck(Vector2Int possibleMove) {
        bool canEat = false;
        foreach (ChessPiece threateningPiece in thisPlayer.threateningPieces) {
            if (possibleMove == threateningPiece.coord) {
                canEat = true;
                break;
            }
        }
        return !thisPlayer.inCheck || CanBlock(possibleMove, thisPlayer.checkPath) || canEat;
    }

    //Eating & CheckMate will be done by a game manager
}
