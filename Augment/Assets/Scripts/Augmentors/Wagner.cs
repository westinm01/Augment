using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wagner : Augmentor
{
    [SerializeField] int turnsLeft = 3;
    [SerializeField] int cooldownTime = 4;
    public bool augmentActivated;
    private bool targetEliminated;
    public GameObject managers;


    protected override void Awake()
    {
        managers = GameObject.FindGameObjectsWithTag("GameManager")[0];
        HoldingManager hm = managers.GetComponent<HoldingManager>();
        this.augmentor = hm.augmentorObjects[16];
        base.Awake();
    }
    // Start is called before the first frame update
    protected override void Start() {
        base.Start();
        canActivate = true;
        turnsLeft = cooldownTime*2;
        targetPiece = null;
    }

    public void Cooldown(){
        if(turnsLeft <= 0){
            turnsLeft = cooldownTime;
            canActivate = true;
            GameManager.Instance.GetEventsManager().OnTurnEnd -= Cooldown;
            augmentActivated = false;
            if(!targetEliminated){
                augmentPiece.gameObject.SetActive(false);
            }
      
        }else{
            turnsLeft--;
            
        }
    }

    public void TargetCheck(ChessPiece pieceEaten, ChessPiece eater){
        //Debug.Log("WAGNER: " + !pieceEaten.gameObject.activeSelf + augmentActivated);
        if(!pieceEaten.gameObject.activeSelf && augmentActivated){
            turnsLeft = -1;
            GameManager.Instance.board.MovePiece(eater, eater.lastCoord.x, -1 * eater.lastCoord.y);
            //upodate the board
            //GameManager.Instance.GetBoardManager().GetBoard().UpdateBoard(eater.coord, eater);
            //Place it back and move your piece back
            pieceEaten.SwitchTeams();
            Player piecePlayer = GameManager.Instance.GetPlayer(augmentPiece.team);
            piecePlayer.playerPieces.Add(pieceEaten);
            pieceEaten.gameObject.SetActive(true);
            targetEliminated =true;
            Debug.Log("WAGNER: Target Replaced");
            GameManager.Instance.board.AddPiece(pieceEaten, pieceEaten.coord.x, pieceEaten.coord.y);
            Debug.Log("WAGNER " + GameManager.Instance.board.GetChessPiece(pieceEaten.coord.x, pieceEaten.coord.y).name);
            StartCoroutine(CanvasManager.Instance.AugmentorFlash(this));
        }
    }
    
    public override void UseAugment()
    {   
        targetPiece = null;
        if(canActivate){
            Debug.Log("Can Activate");
            canActivate = false;
            augmentActivated = true;
            StartCoroutine(UseRR());
        }else{
            Debug.Log("Can't activate :'3");
            
            //Play an animation??
        }
    }

    private IEnumerator UseRR(){
        //Piece Selection
        GameManager.Instance.GetInputManager().AugmentActivation(this, true);
        while(targetPiece == null){
            yield return new WaitForSeconds(0.1f);
        }        
        Debug.Log("WAGNER: Target Piece set");
        //RiskReward
        GameManager.Instance.GetEventsManager().OnTurnEnd += Cooldown;
        GameManager.Instance.GetEventsManager().OnPieceEaten += TargetCheck;//Currently these parameters don't mean very much
       

    }

    public void setSelection(GameObject piece){
        targetPiece = piece.GetComponent<ChessPiece>();
    }






}