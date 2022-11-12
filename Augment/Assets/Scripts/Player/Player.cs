using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool playerTeam;
    public bool inCheck;
    public List<ChessPiece> playerPieces;
    public List<ChessPiece> threateningPieces;
    public List<Vector2Int> checkPath;

    void Start() {
        checkPath = new List<Vector2Int>();

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

    public bool isInCheck()
    {
        // Find the king piece
        KingPiece king = GetKingPiece();
        Player enemyPlayer = GameManager.Instance.GetPlayer(!this.playerTeam);

        // King piece found, check if its threatened
        threateningPieces = GameManager.Instance.board.GetThreateningPieces(king.coord, enemyPlayer);

        // Add path from threatening pieces to king to checkPath
        // Needs to add a path for each threatening piece
        checkPath.Clear();
        foreach (ChessPiece piece in threateningPieces) {
            List<Vector2Int> spacesInbetween = GameManager.Instance.board.GetSpacesInbetween(king.coord, piece.coord);
            foreach (Vector2Int space in spacesInbetween) {
                checkPath.Add(space);
            }
        }

        inCheck = threateningPieces.Count != 0;
        return inCheck;
    }

    public bool isInCheckmate()
    {
        // Find the king piece
        KingPiece king = GetKingPiece();
        Player enemyPlayer = GameManager.Instance.GetPlayer(!this.playerTeam);

        // Check if all spaces the king can move to are threatened
        // foreach(Vector2Int vec in king.possibleSpaces)
        // {
        //     if (GameManager.Instance.board.GetThreateningPieces(vec, enemyPlayer).Count == 0){
        //         return false;
        //     }
        // }

        // Check if any pieces can move
        foreach (ChessPiece piece in playerPieces) {
            if (piece.possibleEats.Count != 0 || piece.possibleSpaces.Count != 0) {
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
