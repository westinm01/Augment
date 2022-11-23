using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InputState{
    Wait, TouchHold, TouchRelease, Move
}

public class InputManager : MonoBehaviour
{
    public GameObject currSelected;

    [SerializeField]
    private InputState state = InputState.Wait;
    private Vector3 initialPos;

    void Update()
    {
        AIPlayer currAI;
        if (GameManager.Instance.GetCurrentPlayer().TryGetComponent<AIPlayer>(out currAI)) {
            if (!currAI.isMakingMove) {
                currAI.StartMove();
            }
            return;
        }


        switch (state)
        {
            case InputState.Wait:
                if (Input.touchCount > 0)               // If touched screen
                {
                    Touch t = Input.GetTouch(0);        // Get first touch
                    if (t.phase == TouchPhase.Began)    // Check if first touch is start of touch
                    {
                        GameObject tempSelected = GetTouchedPiece(t.position);      // Check if touch was on a piece
                        
                        //Check if the selected piece is a chess piece and gets the piece's ChessPiece component to check which team and if it is black's turn
                        if (tempSelected != null && tempSelected.tag == "ChessPiece")
                        {
                            // If selected piece matches current player's turn
                            // if team == false, then black turn, (false != true) = true
                            // if team == true, then white turn, (true != false) = true
                            if (tempSelected.GetComponent<ChessPiece>().team == GameManager.Instance.GetCurrentPlayer().playerTeam) {
                                SelectPiece(tempSelected);
                            }
                        }
                    }
                }
                break;
            case InputState.TouchHold:
                if (Input.touchCount > 0)
                {
                    Touch t = Input.GetTouch(0);
                    currSelected.transform.position = Camera.main.ScreenToWorldPoint(t.position) + new Vector3(0, 0, 10);   // piece follows users finger while holding, add 10 to z to match other pieces
                    
                    // If the player let go
                    if (t.phase == TouchPhase.Ended)
                    {
                        Debug.Log("Released");
                        GameObject tempSelected = GetTouchedPiece(t.position);      // Check if touch was on a piece
                        
                        // if the player let go on top of a possible space
                        if (tempSelected != null && tempSelected.tag == "PossibleSpace")
                        {
                            // Move the piece to the empty space
                            MovePiece(tempSelected.transform.position);
                        }
                        else {
                            // Released over empty space, keep highlight
                            currSelected.transform.position = initialPos;
                            state = InputState.TouchRelease;
                        }
                    }
                }
                break;

            case InputState.TouchRelease:
                if (Input.touchCount > 0)               // If touched screen
                {
                    Touch t = Input.GetTouch(0);        // Get first touch
                    GameObject tempSelected = GetTouchedPiece(t.position);

                    if (tempSelected != null && tempSelected.tag == "ChessPiece")
                    {
                        if (tempSelected.GetComponent<ChessPiece>().team == GameManager.Instance.GetCurrentPlayer().playerTeam) {
                            SelectPiece(tempSelected);
                        }
                    }
                    else if (t.phase == TouchPhase.Ended)    // Check if first touch is end of touch
                    {
                        if (tempSelected != null && tempSelected.tag == "PossibleSpace")
                        {
                            MovePiece(tempSelected.transform.position);
                        }
                        else
                        {
                            // Touched an empty space, go back to wait state and unhighlight
                            GameManager.Instance.board.UnHighlightPieces();
                            state = InputState.Wait;
                        }
                    }
                }
                break;
        }
    }

    /// <summary>
    /// Returns a piece if a player touched one
    /// Returns null if player touched empty space/non piece
    /// </summary>
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

    /// <summary>
    /// Highlights a piece and moves to touchHold state
    /// </summary>
    private void SelectPiece(GameObject tempSelected) {
        currSelected = tempSelected;
        initialPos = currSelected.transform.position;      

        GameManager.Instance.board.UnHighlightPieces();
        GameManager.Instance.board.HighlightPiece(currSelected);
        GameManager.Instance.board.HighlightPossibleMoves(currSelected.GetComponent<ChessPiece>());

        state = InputState.TouchHold;
    }

    /// <summary>
    /// Calls boardManager.movePiece and switch player turns
    /// </summmary>
    private void MovePiece(Vector3 newPos) {
        int newX = (int) newPos.x;
        int newY = (int) newPos.y;

        GameManager.Instance.board.UnHighlightPieces();
        GameManager.Instance.board.MovePiece(currSelected.GetComponent<ChessPiece>(), newX, newY);

        currSelected = null;
        GameManager.Instance.SwitchTeams();
        
        state = InputState.Wait;
    }
}
