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

    private bool hasActivated;
    private int turnsLeft;
    private InputManager inputManager;
    protected override void Start()
    {
        base.Start();
        hasActivated = false;
        turnsLeft = 0;   
        inputManager = GameManager.Instance.GetInputManager();
    }

    public override void UseAugment()
    {
        if (canActivate) {
            if (!hasActivated) {
                hasActivated = true;
                hasPrompt = false;
                // inputManager.currentAugmentor = this;
                GameObject piece = GameManager.Instance.GetChessPiecePrefab(augmentPiece.pieceValue);
                List<Vector2Int> freeSpaces = GameManager.Instance.board.GetFreeAdjacentSpaces(this.augmentPiece.coord);
                if (freeSpaces.Count > 0) {
                    int randIndex = Random.Range(0, freeSpaces.Count-1);
                    Vector2Int randSpace = freeSpaces[randIndex];
                    GameObject newPiece = Instantiate(piece, new Vector3(randSpace.x, -randSpace.y, 0), Quaternion.identity);
                    ChessPiece newChessPiece = newPiece.GetComponent<ChessPiece>();
                    newChessPiece.team = augmentPiece.team;
                    augmentPiece.GetPlayer().playerPieces.Add(newChessPiece);
                    // GameManager.Instance.board.AddPiece(newChessPiece, randSpace.y, randSpace.x);
                }
            }
            else {
                hasActivated = false;
                hasPrompt = true;
            }
        }
    }

    public override void UseAugment(Vector2Int space)
    {
        base.UseAugment(space);
        
    }
}
