using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance {get; private set; }
    public BoardManager board;

    public Player whitePlayer;
    public Player blackPlayer;
    private Player currPlayer;

    private TriggerManager tm;
    private EventsManager events;
    
    public StatusManager statusManager;

    int numFullMoves = 1;

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
        statusManager = GetComponent<StatusManager>();
        events = GetComponent<EventsManager>();
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

    public EventsManager GetEventsManager() {
        return events;
    }

    public int GetNumFullMoves()
    {
        return numFullMoves;
    }

    public void UpdateAllPossibleMoves() {
        whitePlayer.UpdatePossibleMoves();
        blackPlayer.UpdatePossibleMoves();
    }

    public void SwitchTeams() {
         //!!!!!!!!CHECK CURRPLAYER TRIGGERS: 7!!!!!!!!!!!!!!!!!!!!!!!!
        tm.CheckTrigger(7, currPlayer.playerTeam);

        if (whitePlayer == currPlayer) {
            currPlayer = blackPlayer;
        }
        else {
            currPlayer = whitePlayer;

            numFullMoves++;
        }
        statusManager.TurnUpdate();

         //!!!!!!!!CHECK CURRPLAYER TRIGGERS: 0!!!!!!!!!!!!!!!!!!!!!!!!
         tm.CheckTrigger(0, currPlayer.playerTeam);
         events.CallOnTurnEnd();
    }

    public void EndGame() {
    }
}
