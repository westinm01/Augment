using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardObject : MonoBehaviour
{
    public Vector2Int coord;
    public bool team = true;
    public Augmentor correspondingAugmentor;
    public ChessPiece correspondingPiece;
    public bool blocksEnemies = true;
    public bool blocksAllies = true;
    public bool viewableToEnemies = true;
    public int turnCount = -1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
