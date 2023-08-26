using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tahira : Augmentor
{
    //need to make statemachine
    //
    enum SkateState {init, onePawn, pawnKnight, pawnKnightpawn};
    SkateState currState = SkateState.init;
    
    private GameObject managers;
    private BoardManager bm;

    [SerializeField]
    private GameObject pawnObject;
    [SerializeField]
    private GameObject knightObject;

    Player piecePlayer;
    Player enemyPlayer;
        protected override void Awake()
    {
        managers = GameObject.FindGameObjectsWithTag("GameManager")[0];
        HoldingManager hm = managers.GetComponent<HoldingManager>();
        pawnObject = hm.augmentorEffectObjects[5];
        knightObject = hm.augmentorEffectObjects[6];
        this.augmentor = hm.augmentorObjects[15];
        base.Awake();

    }
    void Start()
    {
        piecePlayer = GameManager.Instance.GetPlayer(this.gameObject.GetComponent<ChessPiece>().team);
        enemyPlayer = GameManager.Instance.GetPlayer(!this.gameObject.GetComponent<ChessPiece>().team);
        //managers = GameObject.FindGameObjectsWithTag("GameManager")[0];
        bm = managers.gameObject.GetComponent<BoardManager>();
        
    }

    public override void UseAugment()
    {
        int numEaten = piecePlayer.capturedPieces.Count;
        if(numEaten >= 2)
        {
            if(piecePlayer.capturedPieces[numEaten - 1].GetType() == typeof(PawnPiece))
                if(piecePlayer.capturedPieces[numEaten - 2].GetType() == typeof(PawnPiece))
                {
                    CreateTahiraPiece(true, this.gameObject.GetComponent<ChessPiece>().team);
                    StartCoroutine(CanvasManager.Instance.AugmentorFlash(this));
                }
                else if(piecePlayer.capturedPieces[numEaten - 2].GetType() == typeof(KnightPiece))
                {
                    if(numEaten >= 3)
                    {
                        if(piecePlayer.capturedPieces[numEaten - 3].GetType() == typeof(PawnPiece))
                        {
                            //create knight
                            CreateTahiraPiece(false, this.gameObject.GetComponent<ChessPiece>().team);
                            StartCoroutine(CanvasManager.Instance.AugmentorFlash(this));
                        }
                    }
                }
        }
    /*
        //transition states
        switch(currState)
        {
            case SkateState.init:
            

                if(piecePlayer.capturedPieces[piecePlayer.capturedPieces.Count - 1].GetType() == typeof(PawnPiece))
                {
                    //assign every Tahira piece's state at once!
                    currState = SkateState.onePawn;
                    Debug.Log("TAHIRA: 1 pawn down");
                }
            
            break;

            case SkateState.onePawn:
            
                if(piecePlayer.capturedPieces[piecePlayer.capturedPieces.Count - 1].GetType() == typeof(PawnPiece))
                {
                    
                    //instantiate a new pawn and stay in this state
                    Debug.Log("Making Pawn!");
                    StartCoroutine(CanvasManager.Instance.AugmentorFlash(this));
                }

                else if(piecePlayer.capturedPieces[piecePlayer.capturedPieces.Count - 1].GetType() == typeof(KnightPiece))
                {
                    currState = SkateState.pawnKnight;
                    Debug.Log("TAHIRA: 1 pawn down, 1 knight down");

                }
                else
                {
                    currState = SkateState.init;
                }
            
            break;

            case SkateState.pawnKnight:

                if(piecePlayer.capturedPieces[piecePlayer.capturedPieces.Count - 1].GetType() == typeof(PawnPiece))
                {
                    Debug.Log("Making Knight!");
                    //instantiate a new knight, and go to one Pawn
                    currState = SkateState.onePawn;
                    StartCoroutine(CanvasManager.Instance.AugmentorFlash(this));
                }
                else{
                    currState = SkateState.init;
                }

            break;

        }

            */
    }

    void AddTahiraPieceToBoard(bool piece, int i, int j)
    {
        GameObject g;
        
        if (piece)
        {
            targetPiece = GameManager.Instance.GetChessPiecePrefabByIndex(0).GetComponent<ChessPiece>(); //piece with value 0
        }
        else
        {
            targetPiece = GameManager.Instance.GetChessPiecePrefabByIndex(1).GetComponent<ChessPiece>(); //piece with value 1
        }
        GameObject newPiece = Instantiate(targetPiece.gameObject, new Vector3(i, -j, 0), Quaternion.identity);
        ChessPiece newChessPiece = newPiece.GetComponent<ChessPiece>();
        
        newChessPiece.SetTeam(this.gameObject.GetComponent<ChessPiece>().team);
        piecePlayer.playerPieces.Add(newChessPiece);
        newPiece.transform.parent = this.gameObject.transform.parent;
        bm.AddPiece(newChessPiece, j, i);
        
    }

    private bool CreateTahiraPiece(bool piece, bool team)
    {
        if (team)
        {
            for(int j = bm.getHeight() - 1; j >= 0; j--)
            {
                for(int i = 0; i < bm.getWidth() - 1; i++)
                {
                    if(bm.isValidMoveSpace(i, j))
                    {
                        AddTahiraPieceToBoard(piece, i, j);
                        return true; //added piece successfully!
                    }
                }
            }
        }
        else
        {
            for(int j = 0; j < bm.getHeight() - 1; j++)
            {
                for(int i = 0; j < bm.getWidth() - 1; i++)
                {
                    if(bm.isValidMoveSpace(i, j))
                    {
                        AddTahiraPieceToBoard(piece,i,j);
                        return true; // added piece successfully!
                    }
                }
            }
        }
        return false; //could not add piece
    }
}
