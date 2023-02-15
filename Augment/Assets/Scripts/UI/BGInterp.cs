using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGInterp : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera camera;
    public RawImage piecesImage;
    private Color newBGColor;
    private Color currentBGColor;
    private float BGTimer;
    [SerializeField] float changeTime = 1f;

    private Color newPieceColor;
    private Color currentPieceColor;
    private float pieceTimer;

    private bool newBGColorDetected = false;
    private bool newPieceColorDetected = false;

    //private Color lerpedColor;

    private List<Color> colorList = new List<Color>();
    void Start()
    {
        
        colorList.Add(new Color(0,0,0, 1f)); // BLACK 0
        colorList.Add(new Color(1f, 1f, 1f, 1f)); // WHITE 1
        colorList.Add(new Color(1f, 0f, 0f, 1f)); // RED 2
        colorList.Add(new Color(0f, 1f, 0f, 1f)); //GREEN 3
        colorList.Add(new Color(0f, 0f, 1f, 1f)); //BLUE 4
        colorList.Add(new Color (1f, 1f, 0f, 1f)); //YELLOW 5
        colorList.Add(new Color(1f,0f,1f,1f)); //MAGENTA 6
        colorList.Add(new Color(.5f, .25f, 0f, 1f)); //BROWN 7
        colorList.Add(new Color(.1f,.1f,.1f, 1f)); // DARK GREY 8
        colorList.Add(new Color(15f/255f,89f/255f,209f/255f, 1f)); // Calmer Blue 9
        colorList.Add(new Color(32f/255f,120f/255f,0f, 1f)); // Calmer Green 10

        currentBGColor = camera.backgroundColor;
        currentPieceColor = piecesImage.color;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (newBGColorDetected)
        {
            BGTimer += Time.deltaTime;
            camera.backgroundColor = Color.Lerp(currentBGColor, newBGColor, BGTimer/changeTime);
            if (BGTimer >= changeTime)
            {
                BGTimer = 0f;
                newBGColorDetected = false;
                currentBGColor = newBGColor;
            }
        }
        if (newPieceColorDetected)
        {
            pieceTimer += Time.deltaTime;
            piecesImage.color = Color.Lerp(currentPieceColor, newPieceColor, pieceTimer/changeTime);
            if (pieceTimer >= changeTime)
            {
                pieceTimer = 0f;
                newPieceColorDetected = false;
                currentPieceColor = newPieceColor;
            }
        }
        
    }

    public void SetBGColor(int index)
    {

        newBGColor = colorList[index];
        newBGColorDetected = true;
        
    }

    public void SetPieceColor(int index)
    {
        newPieceColor = colorList[index];
        newPieceColor.r /= 2f;
        newPieceColor.b /=2f;
        newPieceColor.g /= 2f;
        newPieceColor.a = .8f;
        
        newPieceColorDetected = true;
    }

    

}
