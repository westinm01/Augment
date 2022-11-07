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

    public void UpdatePossibleMoves() {
        foreach (ChessPiece piece in playerPieces) {
            piece.GetPossibleSpaces();
        }
    }

    public void mattIsSuperCheckedSuperDuperMattFunction()
    {
        // Find the king piece
        KingPiece king = GetKingPiece();
        //Debug.Log("Garrick is cool");

        // King piece found, check if its threatened
        GameManager.Instance.board.isCheckThreatened(king.coord, GameManager.Instance.GetEnemyPlayer(this));
        return;
    }

    public bool mattWinsTheGame()
    {
        KingPiece king = GetKingPiece();
        //Debug.Log("Matt wins the Game");
        foreach(Vector2Int vec in king.possibleSpaces)
        {
            if (GameManager.Instance.board.isCheckThreatened(vec, GameManager.Instance.GetEnemyPlayer(this)) == null){
                return false;
            }
        }

        Debug.Log("CHECKMATE");
        return true;
    }

    private KingPiece GetKingPiece() {
        foreach (ChessPiece piece in playerPieces) {
            KingPiece king;
            if ( piece.TryGetComponent<KingPiece>(out king) ) {
                return king;
            }
        }
        
        return null;
    }

}
