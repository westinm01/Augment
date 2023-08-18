using Unity.Networking.Transport;
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
    public Player currPlayer;

    private TriggerManager tm;
    private EventsManager events;
    private AudioManager audioManager;
    private InputManager inputManager;
    public StatusManager statusManager;
    public List<ChessPiece> piecePrefabs;   // List of possible pieces sorted by value

    public float turnTimer {get; private set; }
    private float nextTurnTime;


    int numFullMoves = 1;


    //For Multiplayer logic////////////////
    private int playerCount = -1;
    private int currentTeam = -1;
    ////////////////////////////////////

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else {
            Instance = this;
        }
        RegisterEvents();//maybe here or at end of start.
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
        audioManager = GetComponentInChildren<AudioManager>();
        inputManager = GetComponent<InputManager>();

        turnTimer = currPlayer.maxTurnTime;
        nextTurnTime = currPlayer.maxTurnTime;
    }

    // Update is called once per frame
    private void Update()
    {
        turnTimer -= Time.deltaTime;
        currPlayer.DecrementTime(Time.deltaTime);
        if (turnTimer <= 0) {
            board.UnHighlightPieces();
            SwitchTeams();
        }
    }

    public Player GetCurrentPlayer() {
        return currPlayer;
    }

    /// <summary>
    /// Returns the opponent of the current player
    /// </summary>
    /// <returns></returns>
    public Player GetEnemyPlayer() {
        return GetPlayer(!currPlayer.playerTeam);
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

    public TriggerManager GetTriggerManager() {
        return tm;
    }

    public AudioManager GetAudioManager() {
        return audioManager;
    }

    public InputManager GetInputManager() {
        return inputManager;
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

        turnTimer = nextTurnTime;
        if (nextTurnTime != currPlayer.maxTurnTime)
        {
            nextTurnTime = currPlayer.maxTurnTime;
        }
        currPlayer.DecrementTime(Time.deltaTime);
    }

    public void SetNextTurnTimer(float time)
    {
        nextTurnTime = time;
    }

    public void EndGame() {
        Debug.Log("Game over");
    }

    /// <summary>
    /// Returns a chess piece with equal or lesser value than the input value
    /// Used to get a piece for Felipe augment
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public GameObject GetChessPiecePrefab(int value) {
        int maxIndex = 0;
        if (value >= 30) {   // bishop/knght
            maxIndex += 2; 
        }
        if (value >= 50) {   // rook
            maxIndex++;
        }
        if (value >= 90) {   // queen
            maxIndex++;
        }
        if (value >= 900) {  // king
            maxIndex++;
        }
        int randIndex = UnityEngine.Random.Range(0, maxIndex);
        return piecePrefabs[randIndex].gameObject;
    }

    public GameObject GetChessPiecePrefabByIndex(int index)
    {
        return piecePrefabs[index].gameObject;
    }

    #region Events
    private void RegisterEvents()
    {
        NetUtility.S_WELCOME += OnWelcomeServer;

        NetUtility.C_WELCOME += OnWelcomeClient;
        NetUtility.C_START_GAME += OnStartGameClient;
    }
    
    private void UnRegisterEvents()
    {

    }
    //Server
    private void OnWelcomeServer(NetMessage msg, NetworkConnection cnn)
    {
        //Client has connected, assign a team and return the message back to them
        NetWelcome nw = msg as NetWelcome;
        
        //Assign a team
        nw.AssignedTeam = ++playerCount;

        //Return back to the client.
        Server.instance.SendToClient(cnn, nw);
        if(playerCount == 1)
        {
            Server.instance.Broadcast(new NetStartGame());
        }
    }

    //Client
    private void OnWelcomeClient(NetMessage msg)
    {
        //Received the connection message
        NetWelcome nw = msg as NetWelcome;

        //Assign the team
        currentTeam = nw.AssignedTeam;

        Debug.Log($"My assigned team is {nw.AssignedTeam}");
    }

    private void OnStartGameClient(NetMessage msg)
    {
        //get everything ready to start game
    }

    #endregion
}
