using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InputState{
    Wait, TouchHold, TouchRelease
}

public class InputManager : MonoBehaviour
{
    public GameObject currSelected;

    [SerializeField]
    private InputState state = InputState.Wait;
    void Update()
    {
        switch (state)
        {
            case InputState.Wait:
                if (Input.touchCount > 0)               // If touched screen
                {
                    Touch t = Input.GetTouch(0);        // Get first touch
                    if (t.phase == TouchPhase.Began)    // Check if first touch is start of touch
                    {
                        currSelected = GetTouchedPiece(t.position);      // Check if touch was on a piece
                        if (currSelected != null && currSelected.tag == "ChessPiece")
                        {
                            GameManager.Instance.board.HighlightPiece(currSelected);
                            state = InputState.TouchHold;
                        }
                    }
                }
                break;
            case InputState.TouchHold:
                if (Input.touchCount > 0)
                {
                    Touch t = Input.GetTouch(0);
                    if (t.phase == TouchPhase.Ended)
                    {
                        Debug.Log("Released");
                        state = InputState.TouchRelease;
                    }
                }
                break;

            case InputState.TouchRelease:
                if (Input.touchCount > 0)               // If touched screen
                {
                    Touch t = Input.GetTouch(0);        // Get first touch
                    if (t.phase == TouchPhase.Ended)    // Check if first touch is end of touch
                    {
                        GameObject tempSelected = GetTouchedPiece(t.position);      // Check if touch was on a piece
                        if (tempSelected != null)
                        {
                            if (tempSelected.tag == "ChessPiece")
                            {
                                currSelected = tempSelected;
                                GameManager.Instance.board.HighlightPiece(currSelected);
                            }
                        }
                        else
                        {
                            GameManager.Instance.board.UnHighlightPiece();
                            state = InputState.Wait;
                        }
                    }
                }
                break;
        }
    }

    private GameObject GetTouchedPiece(Vector3 touchPos)
    {
        Vector3 tPos = Camera.main.ScreenToWorldPoint(touchPos);
        RaycastHit2D raycast = Physics2D.Raycast(tPos, Vector2.zero);
        if (raycast)                    // Check if touch hit any colliders
        {
            Debug.Log("Touched " + raycast.transform.gameObject);
            return raycast.transform.gameObject;                // Set currSelected to touched piece
        }
        return null;
    }
}
