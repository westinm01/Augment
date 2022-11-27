using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class StartDate : MonoBehaviour
{
    public TextMeshProUGUI startDate;
    string time = "None";

    void Start()
    {
        time = System.DateTime.UtcNow.ToLocalTime().ToString("M/d/yyyy");
        startDate.text = time;
    }
}