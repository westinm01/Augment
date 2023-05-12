using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingPiece : ChessPiece
{   
    private bool moved;
    public RookPiece kingSide = null;
    public RookPiece queenSide = null;

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
        Player enemyPlayer = GameManager.Instance.GetPlayer(!this.team);
        foreach (Vector2Int vec in allPossibleSpaces)
        {
            int newX = coord.x + vec.x;
            int newY = coord.y + vec.y;
            Vector2Int nextMove = new Vector2Int(newX, newY);
            // Player enemyPlayer = GameManager.Instance.GetPlayer(!this.team);

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

        if(canCastle(0)){
            if(!GameManager.Instance.board.isSpaceOccupied(coord.x + 1, coord.y) && !GameManager.Instance.board.isSpaceOccupied(coord.x + 2, coord.y)){
                List<ChessPiece> threatening = GameManager.Instance.board.GetThreateningPieces(new Vector2Int(coord.x+2, coord.y), enemyPlayer);

                if (threatening.Count == 0 ) 
                {
                    possibleSpaces.Add(new Vector2Int(coord.x + 3, coord.y));
                }
                
            }
        }

        if(canCastle(1)){
            if(!GameManager.Instance.board.isSpaceOccupied(coord.x - 1, coord.y) && !GameManager.Instance.board.isSpaceOccupied(coord.x - 2, coord.y) && !GameManager.Instance.board.isSpaceOccupied(coord.x - 2, coord.y)){
                
                List<ChessPiece> threatening = GameManager.Instance.board.GetThreateningPieces(new Vector2Int(coord.x- 2, coord.y), enemyPlayer);

                if (threatening.Count == 0 ) 
                {
                    possibleSpaces.Add(new Vector2Int(coord.x - 4, coord.y));

                }

                
            }
        }

    }

    private bool CanMoveBackwardsInCheck(Vector2Int nextMove) {
        if (thisPlayer.inCheck) {
            foreach (ChessPiece piece in thisPlayer.threateningPieces) {
                if (piece.InPath(nextMove) && piece.coord != nextMove) {
                    // Debug.Log("Preventing king from moving to " + nextMove);
                    return false;
                }
            }
        }

        return true;
    }

    public bool canCastle(int i){//Neither king nor specific rook can have been moved, and you can't castle out or into check. 0 is kingside, 1 is queenside
        
        if(thisPlayer.inCheck) return false;

        if(kingSide == null && queenSide == null){
            kingSide = (RookPiece) GameManager.Instance.board.GetChessPiece(coord.x + 3, coord.y);
            queenSide = (RookPiece) GameManager.Instance.board.GetChessPiece(coord.x - 4, coord.y);
        }
        
        

        if(!moved){
            if(i == 0 && kingSide.canCastle || i==1 && queenSide.canCastle){//
                return true;
            }else{
                return false;
            }
        }else{
            return false;
        }
    }

    public void firstMove(){
        Debug.Log("King Moved");
        moved = true;
    }
}
