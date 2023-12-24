using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree
{
    Node rootNode;
    Node bestNode;

    GameManager gameManager;
    ChessBoard board;
    //TODO: NEED TO MAKE A BOARD MANAGER

    Player thisPlayer;
    Player opponentPlayer;
    
    int width = 8;
    int height = 8;

    

    public Tree()
    {
        
        //get the game manager instance and players
        gameManager = GameManager.Instance;
        thisPlayer = gameManager.blackPlayer;
        opponentPlayer = gameManager.whitePlayer;
        InitializeBoard();

        rootNode = new Node(0);
        
        List<ChessPiece> teamPieces = thisPlayer.playerPieces;


        //TREE GENERATION:
            //For every piece, get a list of all its moves, eats, and protects (not implemented yet)
                //for every eat, create a node for it and add it to the tree
                //for every move, create a node for it and add it to the tree
        foreach(ChessPiece piece in teamPieces){
            
            piece.GetPossibleSpaces(); //get every possible space for that piece
            List<Vector2Int> possibleMoves = piece.possibleSpaces;
            List<Vector2Int> possibleEats = piece.possibleEats;
            
            

            
            foreach(Vector2Int eatMove in possibleEats){
                rootNode.AddChild(CreateNode(rootNode, piece, eatMove));

            }

            if(possibleEats.Count <= 0){
                foreach(Vector2Int move in possibleMoves){
                    rootNode.AddChild(CreateNode(rootNode, piece, move));

                }
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
        Vector2Int piecePosition = piece.coord;
        int eatValue = 0;
        Node child = new Node(parent.depth + 1);

        //1. Calculate teamValue (ratio of piece values) after a possible eat
        if(board.GetPiece(move.x, move.y) != null && board.GetPiece(move.x, move.y).team != piece.team){
            ChessPiece possibleEat = board.GetPiece(move.x, move.y);
            eatValue = possibleEat.pieceValue;
        }

        int opponentAdvantage = GetAdvantage(!piece.team) - eatValue;
        int teamAdvantage = GetAdvantage(piece.team);
        child.teamValue = (float)teamAdvantage / (float)opponentAdvantage; //Ratio between Ai pieces and player piece values

        //2. need to move the piece, and calculate how many moves the opponent can make
        //board.MovePiece(piece, move.x, move.y);//TODO: NEED TO MAKE A BOARD MANAGER
        List<ChessPiece> opponentPieces = opponentPlayer.playerPieces;
        int opponentMoves = 0;
        foreach(ChessPiece opponentPiece in opponentPieces){
            opponentPiece.GetPossibleSpaces();
            opponentMoves += opponentPiece.possibleSpaces.Count;
        }
        child.opponentMoves = opponentMoves;
        //board.MovePiece(piece, piecePosition.x, piecePosition.y);//TODO: NEED TO MAKE A BOARD MANAGER
        return child;
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
