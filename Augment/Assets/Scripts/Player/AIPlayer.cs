using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIPlayer : Player
{
    public float moveDelay;
    public bool isMakingMove;
    public abstract IEnumerator MakeMove();

    public void StartMove() {
        isMakingMove = true;
        StartCoroutine(MakeMove());
    }
}
