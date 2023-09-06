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
        AugmentPieces();
        currPlayer = whitePlayer;
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
        SceneLoader s = GetComponent<SceneLoader>();
        
        s.LoadSceneName("EndGame");
    }

    public void EndGame(bool winner)
    {
        Debug.Log("Game over");
        SceneLoader s = GetComponent<SceneLoader>();
        
        s.LoadSceneName("EndGame");
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

    public HoldingManager GetHoldingManager()
    {
        return GetComponent<HoldingManager>();
    }

    private void AugmentPieces()
    {
        GameObject field = GameObject.FindGameObjectsWithTag("Field")[0];

        GameObject king2 = field.transform.GetChild(1).gameObject;
        AddAugmentorToPiece(king2, PlayerPrefs.GetInt("King2"));
        GameObject queen2 = field.transform.GetChild(2).gameObject;
        AddAugmentorToPiece(queen2, PlayerPrefs.GetInt("Queen2"));
        GameObject bishop2 = field.transform.GetChild(3).gameObject;
        AddAugmentorToPiece(bishop2, PlayerPrefs.GetInt("Bishop2"));
        GameObject bishop2b = field.transform.GetChild(4).gameObject;
        AddAugmentorToPiece(bishop2b, PlayerPrefs.GetInt("Bishop2"));
        GameObject knight2 = field.transform.GetChild(5).gameObject;
        AddAugmentorToPiece(knight2, PlayerPrefs.GetInt("Knight2"));
        GameObject knight2b = field.transform.GetChild(6).gameObject;
        AddAugmentorToPiece(knight2b, PlayerPrefs.GetInt("Knight2"));
        GameObject rook2 = field.transform.GetChild(7).gameObject;
        AddAugmentorToPiece(rook2, PlayerPrefs.GetInt("Rook2"));
        GameObject rook2b = field.transform.GetChild(8).gameObject;
        AddAugmentorToPiece(rook2b, PlayerPrefs.GetInt("Rook2"));
        for(int i = 0; i < 8; i++)
        {
            GameObject pawn2 = field.transform.GetChild(9 + i).gameObject;
            AddAugmentorToPiece(pawn2, PlayerPrefs.GetInt("Pawn2"));
        }
        for(int i = 0; i < 8; i++)
        {
            GameObject pawn1 = field.transform.GetChild(17 + i).gameObject;
            AddAugmentorToPiece(pawn1, PlayerPrefs.GetInt("Pawn1"));
        }
        GameObject king1 = field.transform.GetChild(25).gameObject;
        AddAugmentorToPiece(king1, PlayerPrefs.GetInt("King1"));
        GameObject queen1 = field.transform.GetChild(26).gameObject;
        AddAugmentorToPiece(queen1, PlayerPrefs.GetInt("Queen1"));
        GameObject bishop1 = field.transform.GetChild(27).gameObject;
        AddAugmentorToPiece(bishop1, PlayerPrefs.GetInt("Bishop1"));
        GameObject bishop1b = field.transform.GetChild(28).gameObject;
        AddAugmentorToPiece(bishop1b, PlayerPrefs.GetInt("Bishop1"));
        GameObject knight1 = field.transform.GetChild(29).gameObject;
        AddAugmentorToPiece(knight1, PlayerPrefs.GetInt("Knight1"));
        GameObject knight1b = field.transform.GetChild(30).gameObject;
        AddAugmentorToPiece(knight1b, PlayerPrefs.GetInt("Knight1"));
        GameObject rook1 = field.transform.GetChild(31).gameObject;
        AddAugmentorToPiece(rook1, PlayerPrefs.GetInt("Rook1"));
        GameObject rook1b = field.transform.GetChild(32).gameObject;
        AddAugmentorToPiece(rook1b, PlayerPrefs.GetInt("Rook1"));

    }

    private void AddAugmentorToPiece(GameObject g, int aug)
    {
        ChessPiece cp = g.GetComponent<ChessPiece>();
        Debug.Log(aug);
        
        AugmentorObject ao = GetHoldingManager().augmentorObjects[aug];
        Type t;
        if(aug == 12)
        {
            t = Type.GetType("MainSolveig");
        }
        else if(aug == 8)
        {
            t = Type.GetType("LadyLeMure");
        }
        else
        {
            t = Type.GetType(ao.characterName);
        }
        Component c = g.AddComponent(t);
        Augmentor a = c as Augmentor;
        
        a.augmentor = ao;
        cp.pieceAugmentor = a;
        a.UpdateInformation();
        TriggerManager tm = GetComponent<TriggerManager>();
        tm.AddToBin(cp, a.augmentor.triggerVal);
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
