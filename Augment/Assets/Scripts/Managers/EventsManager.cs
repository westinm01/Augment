using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsManager : MonoBehaviour
{
    public delegate void TurnEnded();
    public event TurnEnded OnTurnEnd;

    public void CallOnTurnEnd() {
        if (OnTurnEnd != null) {
            OnTurnEnd();
        }
    }
}
