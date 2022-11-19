using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIOpponent : MonoBehaviour
{
    [SerializeField]
    [Range(1, 10)]
    int difficultyLevel = 5;

    [SerializeField]
    bool aiTeam;              // false is black, true is white

    List<ChessPiece> aiPieces;


    void Start()
    {
        GatherTeamPieces();
    }

    private void GatherTeamPieces()
    {
        GameObject[] pieces = GameObject.FindGameObjectsWithTag("ChessPiece");

        foreach (GameObject piece in pieces)
        {
            ChessPiece cPiece = piece.GetComponent<ChessPiece>();
            if (cPiece.team == aiTeam)
            {
                aiPieces.Add(cPiece);
            }
        }
    }

    private void MakeMove()
    {
        
    }

    public void UpdateDifficulty(int newDifficulty)
    {
        difficultyLevel = newDifficulty;
    }
}
