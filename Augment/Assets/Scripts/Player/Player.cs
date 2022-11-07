using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool playerTeam;
    public List<ChessPiece> playerPieces;

    void Start() {
        GameObject[] pieces = GameObject.FindGameObjectsWithTag("ChessPiece");

        foreach (GameObject piece in pieces) {
            ChessPiece cPiece = piece.GetComponent<ChessPiece>();
            if (cPiece.team == playerTeam) {
                playerPieces.Add(cPiece);
            }
        }
    }

    public void mattIsSuperCheckedSuperDuperMattFunction()
    {
        // Find the king piece
        foreach (ChessPiece piece in playerPieces)
        {
            KingPiece king;
            if ( piece.TryGetComponent<KingPiece>(out king) )
            {
                //Debug.Log("Garrick is cool");
                piece.GetPossibleSpaces();
                // King piece found, check if its threatened
                GameManager.Instance.board.isCheckThreatened(piece.coord, GameManager.Instance.GetEnemyPlayer(this));
                return;
            }
        }
    }

    public void mattWinsTheGame()
    {
        foreach (ChessPiece piece in playerPieces)
        {
            KingPiece king;
            if ( piece.TryGetComponent<KingPiece>(out king) )
            {
                //Debug.Log("Matt wins the Game");
                piece.GetPossibleSpaces();
                foreach(Vector2Int vec in piece.possibleSpaces)
                {
                    GameManager.Instance.board.isCheckThreatened(vec, GameManager.Instance.GetEnemyPlayer(this));
                    return;
                }
            }
        }
    }

}
