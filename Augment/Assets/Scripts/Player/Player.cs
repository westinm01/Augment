using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool playerTeam;
    public List<ChessPiece> player;

    void Start() {
        GameObject[] pieces = GameObject.FindGameObjectsWithTag("ChessPiece");

        foreach (GameObject piece in pieces) {
            ChessPiece cPiece = piece.GetComponent<ChessPiece>();
            if (cPiece.team == playerTeam) {
                player.Add(cPiece);
            }
        }
    }

    public void mattIsSuperCheckedSuperDuperMattFunction()
    {
        foreach (ChessPiece piece in player)
        {
            KingPiece king;
            if ( piece.TryGetComponent<KingPiece>(out king) )
            {
                //Debug.Log("Garrick is cool");
                GameManager.Instance.board.isCheckThreatened(piece.coord, this.gameObject.GetComponent<Player>());
            }
        }
    }

    public void mattWinsTheGame()
    {
        foreach (ChessPiece piece in player)
        {
            KingPiece king;
            if ( piece.TryGetComponent<KingPiece>(out king) )
            {
                //Debug.Log("Matt wins the Game");
                foreach(Vector2Int vec in piece.possibleSpaces)
                {
                    GameManager.Instance.board.isCheckThreatened(vec, this.gameObject.GetComponent<Player>());
                }
            }
        }
    }

}
