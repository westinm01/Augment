using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingPiece : ChessPiece
{   
    private bool moved;
    private RookPiece kingSide = null;
    private RookPiece queenSide = null;

    private void Awake()
    {
        SetPieceValue(MiniMaxAI.KING_VAL);
        SetPieceChar(StockfishAI.KING_CHAR);
        moved = false;
    }

    private List<Vector2Int> allPossibleSpaces = new List<Vector2Int> { new Vector2Int(-1, 1), 
                                                                        new Vector2Int(-1, -1), 
                                                                        new Vector2Int(-1, 0),
                                                                        new Vector2Int(0, 1), 
                                                                        new Vector2Int(0, -1), 
                                                                        new Vector2Int(1, 1), 
                                                                        new Vector2Int(1, -1),
                                                                        new Vector2Int(1, 0) };

    public override void GetPossibleSpaces()
    {
        base.GetPossibleSpaces();
        foreach (Vector2Int vec in allPossibleSpaces)
        {
            int newX = coord.x + vec.x;
            int newY = coord.y + vec.y;
            Vector2Int nextMove = new Vector2Int(newX, newY);
            Player enemyPlayer = GameManager.Instance.GetPlayer(!this.team);

            if (GameManager.Instance.board.InBounds(newY, newX)) {
                List<ChessPiece> threatening = GameManager.Instance.board.GetThreateningPieces(nextMove, enemyPlayer);

                // Prevents king from moving to threatened tile
                // If the player is in check, prevent the king from moving along the path of the threatening piece
                if (threatening.Count == 0 && CanMoveBackwardsInCheck(nextMove)) {
                    if (GameManager.Instance.board.isValidMoveSpace(newX, newY))
                    {
                        possibleSpaces.Add(nextMove);
                    }
                    else if (CheckIfCanEat(newX, newY))
                    {
                        ChessPiece eatPiece = GameManager.Instance.board.GetChessPiece(newX, newY);
                        if (!eatPiece.IsProtected()) {
                            possibleEats.Add(nextMove);
                        }
                    }
                }
            }
        }

        if(canCastle()){
            
        }

    }

    private bool CanMoveBackwardsInCheck(Vector2Int nextMove) {
        if (thisPlayer.inCheck) {
            foreach (ChessPiece piece in thisPlayer.threateningPieces) {
                if (piece.InPath(nextMove)) {
                    // Debug.Log("Preventing king from moving to " + nextMove);
                    return false;
                }
            }
        }

        return true;
    }

    public bool canCastle(){//Neither king nor specific rook can have been moved, and you can't castle out or into check.
        
        if(kingSide == null && queenSide == null){
            kingSide = (RookPiece) GameManager.Instance.board.GetChessPiece(coord.x + 3, coord.y);
            queenSide = (RookPiece) GameManager.Instance.board.GetChessPiece(coord.x - 4, coord.y);
        }
        
        

        if(!moved){
            // if(typeof(GameManager.Instance.board.GetChessPiece(coord.x + 3, coord.y)) == typeof(RookPiece)){//Kingside
            //     Debug.Log("kingside");
            // }else if(false){//Queenside
            //     Debug.Log("kingside");
            // }
            return true;
        }else{
            return false;
        }
    }

    public void firstMove(){
        Debug.Log("King Moved");
        moved = true;
    }
}
