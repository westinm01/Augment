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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Player GetCurrentPlayer() {
        return currPlayer;
    }

    public Player GetEnemyPlayer(Player friendlyPlayer) {
        if (whitePlayer == friendlyPlayer) {
            return blackPlayer;
        }
        else {
            return whitePlayer;
        }
    }

    public void SwitchTeams() {
        if (whitePlayer == currPlayer) {
            currPlayer = blackPlayer;
        }
        else {
            currPlayer = whitePlayer;
        }

        whitePlayer.UpdatePossibleMoves();
        blackPlayer.UpdatePossibleMoves();
    }
}
