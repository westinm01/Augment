using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessPiece : MonoBehaviour
{
    public float xPosition;
    public float yPosition;
    public List<float> possibleSpaces = new List<float>(); //calculated at the start of your turn.
    public bool team = true; //true if white, false if black. True by default.
    public bool canJump = false; //setToTrue for Knight.
    public int statusTimer; //number of turns the current status lasts.
    Augmentation pieceAugmentation;
    public int status = 0; //to determine what status it has.
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    virtual public void Move()
    {

    }

    //Eating & CheckMate will be done by a game manager
}
