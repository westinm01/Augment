using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    int heuristicValue;
    int costValue;
    int teamValue;
    int opponentMoves;
    int teamMoves;
    public int depth;
    //ChessBoard b;
    public List<Node> children = new List<Node>();

    // Constructors///////////////////////////
    public Node()
    {
        heuristicValue = 1;
        costValue = 1;
    }


    public Node(int d)
    {
        depth = d;
    }
    /*public Node(ChessBoard b)
    {
        this.b = b;
    }*/

///////////////////////////////////////////
    public int getHeuristicValue()
    {
        return heuristicValue;
    }

    public int getCostValue()
    {
        return costValue;
    }

    public int calculateHeuristicValue()
    {
        heuristicValue = teamValue - opponentMoves + teamMoves; //placeholder heuristic
        return  heuristicValue;
    }

    void AddNode(Node n)
    {
        children.Add(n);
    }
}
