using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CurrentGold : MonoBehaviour
{

    public TextMeshProUGUI currGold;
    public int goldAmount = 0;

    // Start is called before the first frame update
    void Start()
    {
        currGold.text = goldAmount.ToString();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
