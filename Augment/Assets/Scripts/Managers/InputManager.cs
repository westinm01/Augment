using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InputState{
    Wait, TouchHold, TouchRelease, Move
}

public class InputManager : MonoBehaviour
{
    public GameObject currSelected;
    private bool blackTurn;

    [SerializeField]
    private InputState state = InputState.Wait;
    private Vector3 initialPos;

    void Awake()
    {   
        //set boolean variable to true if it is black's turn
        blackTurn = true;
    }

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
                        //Check if the selected piece is a chess piece and gets the piece's ChessPiece component to check which team and if it is black's turn
                        if (currSelected != null && currSelected.tag == "ChessPiece" && currSelected.GetComponent<ChessPiece>().team == false && blackTurn)
                        {
                            GameManager.Instance.board.HighlightPiece(currSelected);
                            GameManager.Instance.board.HighlightPossibleMoves(currSelected.GetComponent<ChessPiece>());
                            state = InputState.TouchHold;
                            initialPos = currSelected.transform.position;
                        }
                        //white turn's check
                        else if ( currSelected != null && currSelected.tag == "ChessPiece" && currSelected.GetComponent<ChessPiece>().team == true && !blackTurn)
                        {
                            GameManager.Instance.board.HighlightPiece(currSelected);
                            GameManager.Instance.board.HighlightPossibleMoves(currSelected.GetComponent<ChessPiece>());
                            state = InputState.TouchHold;
                            initialPos = currSelected.transform.position;
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
                        currSelected.transform.position = initialPos;
                        
                        // if the player let go on top of a possible space
                        if (tempSelected != null && tempSelected.tag == "PossibleSpace")
                        {
                            // Move the piece to the empty space
                            GameManager.Instance.board.UnHighlightPieces();
                            int newX = (int)tempSelected.transform.position.x;
                            int newY = (int)tempSelected.transform.position.y;
                            GameManager.Instance.board.MovePiece(currSelected.GetComponent<ChessPiece>(), newX, newY);
                            state = InputState.Wait;
                        }
                        else {
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
                        GameManager.Instance.board.UnHighlightPieces();
                        currSelected = tempSelected;
                        GameManager.Instance.board.HighlightPiece(currSelected);
                        GameManager.Instance.board.HighlightPossibleMoves(currSelected.GetComponent<ChessPiece>());
                        state = InputState.TouchHold;
                        initialPos = currSelected.transform.position;          
                    }
                    else if (t.phase == TouchPhase.Ended)    // Check if first touch is end of touch
                    {
                        GameManager.Instance.board.UnHighlightPieces();
                        if (tempSelected != null && tempSelected.tag == "PossibleSpace")
                        {
                            //Debug.Log("possible space");
                            int newX = (int)tempSelected.transform.position.x;
                            int newY = (int)tempSelected.transform.position.y;
                            //Vector3 pos = new Vector3(tempSelected.transform.position.x, -tempSelected.transform.position.y, 0);
                            //GameManager.Instance.board.MovePiece(currSelected.GetComponent<ChessPiece>(), pos);
                            GameManager.Instance.board.MovePiece(currSelected.GetComponent<ChessPiece>(), newX, newY);
                            state = InputState.Wait;
                            //changes blackTurn boolean to change the players
                            if ( blackTurn )
                            {
                                blackTurn = false;
                            }
                            else
                            {
                                blackTurn = true;
                            }
                        }
                        else
                        {
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
