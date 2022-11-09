using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAI : AIPlayer
{
    public override IEnumerator MakeMove() {
        yield return new WaitForSeconds(moveDelay);

        ChessPiece randPiece = null;
        int randIndex;

        while (randPiece == null) {
            randIndex = Random.Range(0, playerPieces.Count);
            randPiece = playerPieces[randIndex];
            
            if (randPiece.possibleSpaces.Count > 0) {
                break;
            }
            else {
                randPiece = null;
            }
        }


        List<Vector2Int> possibleEats = randPiece.possibleEats;
        List<Vector2Int> possibleMoves = randPiece.possibleSpaces;
        Vector2Int move = new Vector2Int(0, 0);
        if (possibleEats.Count > 0) {
            move = possibleEats[Random.Range(0, possibleEats.Count)];
        }
        else if (possibleMoves.Count > 0) {
            move = possibleMoves[Random.Range(0, possibleMoves.Count)];
        }

        Debug.Log("Moving piece " + randPiece + " to " + move);
        GameManager.Instance.board.MovePiece(randPiece, move.x, -move.y);
        GameManager.Instance.SwitchTeams();

        isMakingMove = false;
    }
}
