using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Felipe : Augmentor
{
    // void Start()
    // {
    //     hasPrompt = true;
    //     name = "Felipe";
    //     augmentName = "Piece Creation";
    //     augmentDesc = "This Augment can create a piece of equal or lower value in a free space next to it. The more powerful the piece, the more number of turns it takes to construct it. This piece cannot move while constructing.";
    //     triggerVal = 7;
    // }

    private int turnsLeft;
    private InputManager inputManager;
    public GameObject managers;

    protected override void Awake()
    {
        managers = GameObject.FindGameObjectsWithTag("GameManager")[0];
        HoldingManager hm = managers.GetComponent<HoldingManager>();
        this.augmentor = hm.augmentorObjects[4];
        base.Awake();

    }
    protected override void Start()
    {
        base.Start();
        turnsLeft = 0;   
        inputManager = GameManager.Instance.GetInputManager();
    }

    public override void UseAugment()
    {
        if (canActivate) {
            if (turnsLeft == 0) {
                canActivate = false;
                this.augmentPiece.canMove = false;
                targetPiece = GameManager.Instance.GetChessPiecePrefab(augmentPiece.pieceValue).GetComponent<ChessPiece>();
                turnsLeft = (targetPiece.pieceValue / 5) + 1;
                GameManager.Instance.GetEventsManager().OnTurnEnd += Wait;
            }
        }
    }
    public override void UseAugment(Vector2Int space)
    {
        base.UseAugment(space);   
    }

    public void Wait() {
        if (turnsLeft == 0) {
            Debug.Log("Finished waiting");
            CreatePiece();
            canActivate = true;
        }
        else {
            turnsLeft--;
        }
    }

    private void CreatePiece() {
        List<Vector2Int> freeSpaces = GameManager.Instance.board.GetFreeAdjacentSpaces(this.augmentPiece.coord);
        if (freeSpaces.Count > 0) {
            int randIndex = Random.Range(0, freeSpaces.Count-1);
            Vector2Int randSpace = freeSpaces[randIndex];
            GameObject newPiece = Instantiate(targetPiece.gameObject, new Vector3(randSpace.x, -randSpace.y, 0), Quaternion.identity);
            ChessPiece newChessPiece = newPiece.GetComponent<ChessPiece>();
            newChessPiece.team = augmentPiece.team;
            augmentPiece.GetPlayer().playerPieces.Add(newChessPiece);
            GameManager.Instance.board.AddPiece(newChessPiece, randSpace.y, randSpace.x);
        }

        GameManager.Instance.GetEventsManager().OnTurnEnd -= Wait;
        this.augmentPiece.canMove = true;
    }
}
