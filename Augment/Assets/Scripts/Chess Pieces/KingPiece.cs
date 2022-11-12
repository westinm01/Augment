using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KingPiece : ChessPiece
{
    private List<Vector2Int> allPossibleSpaces = new List<Vector2Int> { new Vector2Int(-1, 1), 
                                                                        new Vector2Int(-1, -1), 
                                                                        new Vector2Int(-1, 0),
                                                                        new Vector2Int(0, 1), 
                                                                        new Vector2Int(0, -1), 
                                                                        new Vector2Int(1, 1), 
                                                                        new Vector2Int(1, -1),
                                                                        new Vector2Int(1, 0) };

    // Update is called once per frame
    public override void GetPossibleSpaces()
    {
        base.GetPossibleSpaces();
        foreach (Vector2Int vec in allPossibleSpaces)
        {
            int newX = coord.x + vec.x;
            int newY = coord.y + vec.y;
            Vector2Int nextMove = new Vector2Int(newX, newY);
            Player enemyPlayer = GameManager.Instance.GetPlayer(!this.team);

            // Prevents king from moving to threatened tile
            if (GameManager.Instance.board.GetThreateningPieces(nextMove, enemyPlayer).Count < 0) {
                if (GameManager.Instance.board.isValidMoveSpace(newX, newY))
                {
                    possibleSpaces.Add(nextMove);
                }
                else if (CheckIfCanEat(newX, newY))
                {
                    possibleEats.Add(nextMove);
                }
            }
        }
    }
}
