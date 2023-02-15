using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventsManager : MonoBehaviour
{
    public delegate void TurnEnded();
    public delegate void PieceEaten(ChessPiece pieceEaten, ChessPiece pieceEating);
    public static event Func<ChessPiece, bool> myFunc;
    public event TurnEnded OnTurnEnd;
    public event PieceEaten OnPieceEaten;

    public void CallOnTurnEnd() {
        if (OnTurnEnd != null) {
            OnTurnEnd();
        }
    }

    /// <summary>
    /// Called every time a piece gets eaten
    /// </summary>
    /// <returns></returns>
    public void CallOnPieceEaten(ChessPiece pieceEaten, ChessPiece pieceEating) {
        if (OnPieceEaten != null) {
            OnPieceEaten(pieceEaten, pieceEating);
        }
    }

    public bool CallFunc(ChessPiece p) {
        // List<bool> l = new List<bool>();
        var cancelPieceEaten = true;
        foreach (Delegate func in myFunc.GetInvocationList()) {
            if (!(bool)(func.DynamicInvoke(p))) {
                cancelPieceEaten = false;
            }
            // func(p);
        }
        return cancelPieceEaten;
    }

    // private void Start() {
    //     // ChessPiece p = new ChessPiece();
    //     // myFunc += (ChessPiece p ) => {
    //     //     List<bool> l = new List<bool>();
    //     //     return l;
    //     // };
    //     myFunc += (ChessPiece p) => {return f(p);};
    //     myFunc += (ChessPiece p) => {return t(p);};
    //     // Debug.Log(myFunc)
    //     // myFunc += t(p);
    //     // myFunc += f(p);
    // }

    // private bool t(ChessPiece p) {
    //     Debug.Log("t");
    //     return true;
    // }

    // private bool f(ChessPiece p) {
    //     Debug.Log("f");
    //     return true;
    // }
}
