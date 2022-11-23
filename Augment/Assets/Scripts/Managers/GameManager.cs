using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get; private set; }
    public BoardManager board;

    public Player whitePlayer;
    public Player blackPlayer;
    private Player currPlayer;

    private TriggerManager tm;
    
    public List<ChessPiece> statusedPieces = new List<ChessPiece>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        currPlayer = whitePlayer;
        whitePlayer.UpdatePossibleMoves();
        blackPlayer.UpdatePossibleMoves();
        tm = GetComponent<TriggerManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Player GetCurrentPlayer() {
        return currPlayer;
    }

    public Player GetPlayer(bool team) {
        if (team) {
            return whitePlayer;
        }
        else {
            return blackPlayer;
        }
    }

    public void UpdateAllPossibleMoves() {
        whitePlayer.UpdatePossibleMoves();
        blackPlayer.UpdatePossibleMoves();
    }

    public void SwitchTeams() {
         //!!!!!!!!CHECK CURRPLAYER TRIGGERS: 7!!!!!!!!!!!!!!!!!!!!!!!!
        tm.CheckTrigger(7, currPlayer.playerTeam);

         //also need to update all statusTurns for chesspieces with statuses.
        for (int i = 0; i <statusedPieces.Count; i++)
        {
            statusedPieces[i].statusTimer--;
            if(statusedPieces[i].statusTimer == 0)
            {
                statusedPieces.RemoveAt(i);
            }
        }

        if (whitePlayer == currPlayer) {
            currPlayer = blackPlayer;
        }
        else {
            currPlayer = whitePlayer;
        }
         //!!!!!!!!CHECK CURRPLAYER TRIGGERS: 0!!!!!!!!!!!!!!!!!!!!!!!!
         tm.CheckTrigger(0, currPlayer.playerTeam);
    }

    public void EndGame() {
    }
}
