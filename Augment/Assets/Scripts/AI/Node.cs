using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public float heuristicValue;
    public int costValue;
    public float teamValue;
    public int opponentMoves;
    public int teamMoves;
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
    public float getHeuristicValue()
    {
        return heuristicValue;
    }

    public int getCostValue()
    {
        return costValue;
    }

    public float calculateHeuristicValue()
    {
        heuristicValue = teamValue - opponentMoves + teamMoves; //placeholder heuristic
        return  heuristicValue;
    }

    public void AddChild(Node n)
    {
        children.Add(n);
    }
}
