using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnityEngine;

public class StockfishAI : AIPlayer
{
    [SerializeField]
    [Range(0, 20)]
    int difficultyLevel;

    string boardState = "";

    public const char PAWN_CHAR = 'P';
    public const char KNIGHT_CHAR = 'N';
    public const char BISHOP_CHAR = 'B';
    public const char ROOK_CHAR = 'R';
    public const char QUEEN_CHAR = 'Q';
    public const char KING_CHAR = 'K';

    public override IEnumerator MakeMove()
    {
        // Make a move based off of algebraic notation
        boardState = GetForsythBoardState();
        UnityEngine.Debug.Log(GetBestMove(boardState));

        throw new System.NotImplementedException();
    }

    private void Update()
    {
        GetForsythBoardState();
    }

    string GetForsythBoardState()
    {
        string currBoardState = "";
        int emptyCount = 0;

        // Create Forsyth Board State string
        BoardManager currBoard = GameManager.Instance.board;

        // Gather rows of pieces
        for (int row = 0; row < currBoard.getHeight(); row++)
        {
            for (int column = 0; column < currBoard.getWidth(); column++)
            {
                ChessPiece c = currBoard.GetChessPiece(column, row);

                if (c != null)
                {
                    if (emptyCount > 0)     // if empty spaces between pieces, print the number
                    {
                        currBoardState += emptyCount.ToString();
                        emptyCount = 0;
                    }
                    if (c.team == true)     // Piece is white
                    {
                        currBoardState += char.ToUpper(c.pieceChar);
                    }
                    else                    // Piece is black
                    {
                        currBoardState += char.ToLower(c.pieceChar);
                    }
                }
                else
                {
                    emptyCount++;
                }
            }

            if (emptyCount > 0)           // if empty spaces between piece and end, print the number
            {
                currBoardState += emptyCount.ToString();
                emptyCount = 0;
            }

            if (row != currBoard.getHeight() - 1)
            {
                currBoardState += '/';
            }
        }
        currBoardState += ' ';

        // Say whose turn it is
        if (playerTeam == true)
        {
            currBoardState += 'w'; 
        }
        else
        {
            currBoardState += 'b';
        }

        currBoardState += ' ';

        // TODO: Say if castling is possible for each side each team
        currBoardState += "KQkq"; // TEMPORARY until we have castling logic

        currBoardState += ' ';

        // TODO: Say if there's a possible en pissant move
        currBoardState += '-';  // TEMPORARY until we have en pissant logic

        currBoardState += ' ';

        // TODO: Say the halfmove clock
        currBoardState += '0';  // TEMPORARY until/ if we have halfmove logic for the fifty-move rule

        currBoardState += ' ';

        // TODO: Say the fullmove clock
        currBoardState += GameManager.Instance.GetNumFullMoves().ToString();


        UnityEngine.Debug.Log(currBoardState);
        return currBoardState;
    }

    string GetBestMove(string forsythEdwardsNotationString)
    {
        var p = new System.Diagnostics.Process();
        p.StartInfo.FileName = "stockfishExecutable.exe";
        p.StartInfo.UseShellExecute = false;
        p.StartInfo.RedirectStandardInput = true;
        p.StartInfo.RedirectStandardOutput = true;
        p.Start();
        string setupString = "position fen " + forsythEdwardsNotationString;
        p.StandardInput.WriteLine(setupString);

        // Process for 5 seconds
        string processString = "go movetime 5000";

        // Process 20 deep
        // string processString = "go depth 20";

        p.StandardInput.WriteLine(processString);

        string bestMoveInAlgebraicNotation = p.StandardOutput.ReadLine();

        p.Close();

        return bestMoveInAlgebraicNotation;
    }
}
