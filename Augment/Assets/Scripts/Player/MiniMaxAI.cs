using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Unity.VisualScripting;
using UnityEngine;

public class MiniMaxAI : AIPlayer
{
    public const int PAWN_VAL = 10;
    public const int KNIGHT_VAL = 30;
    public const int BISHOP_VAL = 50;
    public const int ROOK_VAL = 50;
    public const int QUEEN_VAL = 90;
    public const int KING_VAL = 900;

    private int preferredSide; // -1 for black, 1 for white

    BoardManager gameBoardManager;
    ChessBoard simulatedBoard;


    private void Start()
    {
        if (playerTeam == false)
        {
            preferredSide = -1;
        }
        else
        {
            preferredSide = 1;
        }

        gameBoardManager = GameManager.Instance.board;
        
    }

    public override IEnumerator MakeMove()
    {
        ChessPiece movePiece = null;

        // Set simulated board to current board

        throw new System.NotImplementedException();
    }

    private void MiniMax(int depth, int maximizingPlayer, int maximizing_color)
    {

    }

    /*
    int MinimumMove(int depth, ref ChessPiece movePiece)
    {
        throw new System.NotImplementedException();
    }
    int MaximumMove(int depth, ref ChessPiece movePiece)
    {
        throw new System.NotImplementedException();
    } 
    */

    int EvaluateBoard(int maximizingColor)
    {
        int whiteScore = 0;
        int blackScore = 0;

        Player otherPlayer = GameManager.Instance.GetPlayer(!playerTeam);

        // Calculate Scores of Each Team
        foreach (ChessPiece c in playerPieces)
        {
            whiteScore += c.pieceValue;
        }
        foreach (ChessPiece c in otherPlayer.playerPieces)
        {
            blackScore += c.pieceValue;
        }

        if (maximizingColor == -1)  // maxing for black
        {
            return (blackScore - whiteScore);
        }
        else                        // maxing for white
        {
            return (whiteScore - blackScore);
        }
    }
}
