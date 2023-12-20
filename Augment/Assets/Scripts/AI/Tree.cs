using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree
{
    Node rootNode;
    Node bestNode;

    GameManager gameManager;
    ChessBoard board;
    
    int width = 8;
    int height = 8;

    

    public Tree()
    {
        
        //get the game manager instance
        gameManager = GameManager.Instance;
        Player thisPlayer = gameManager.blackPlayer;
        Player opponentPlayer = gameManager.whitePlayer;
        InitializeBoard();

        rootNode = new Node(0);
        
        List<ChessPiece> teamPieces = thisPlayer.playerPieces;
        foreach(ChessPiece piece in teamPieces){
            //get every possible move for that piece
            piece.GetPossibleSpaces();
            List<Vector2Int> possibleMoves = piece.possibleSpaces;
            List<Vector2Int> possibleEats = piece.possibleEats;
            
            Vector2Int piecePosition = piece.coord;

            //tree generation logic
            foreach(Vector2Int move in possibleMoves){
                //create a new node for that move
                /*Node newNode = new Node(board);
                //add the move to the node
                newNode.move = move;
                //add the piece to the node
                newNode.piece = piece;
                //add the node to the root node's children
                rootNode.children.Add(newNode);*/
            }
        }

    }

    void InitializeBoard(){
        BoardManager boardManager = gameManager.board;

        board = new ChessBoard(width, height);

        for(int i = 0; i < width; i++){
            for(int j = 0; j < height; j++){
                ChessPiece piece = boardManager.GetChessPiece(i, j);
                if(piece != null){
                    board.AddPiece(piece, i, j);
                }
            }
        }
    }

    

    Node CreateNode(Node parent, ChessPiece piece, Vector2Int move){
        ChessPiece possibleEat;
        Node child = new Node(parent.depth + 1);
        if(board.GetPiece(move.x, move.y) != null && board.GetPiece(move.x, move.y).team != piece.team){
            possibleEat = board.GetPiece(move.x, move.y);
            //remove possible eat from the boardf
            board.RemovePiece(move.x, move.y);
        }
        child.opponentMoves = GetAdvantage(!piece.team);
        return child;

        //keep in mind possible eat

        //board.MovePiece(piece, move);


    }

    int GetAdvantage(bool team){
        int sum = 0;
        for (int i = 0; i < 8; i++){
            for(int j = 0; j < 8; j++){
                ChessPiece piece = board.GetPiece(i, j);
                if(piece != null){
                    if(piece.team == team){
                        //add the piece's value to the opponent's advantage
                        sum += piece.pieceValue;
                        //also calculate the multiplier for the augmentor Value
                    }
                }
            }
        }

        return sum;
    }
    

}
