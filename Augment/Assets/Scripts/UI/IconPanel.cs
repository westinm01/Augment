using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IconPanel : MonoBehaviour
{
    public GameObject infoPanelObject;
    public string iconName;
    private Button iconButton;

    void Start()
    {
        iconButton = GetComponent<Button>();
        iconButton.onClick.AddListener(DisplayIconInfo);
    }

    void DisplayIconInfo()
    {
        infoPanelObject.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = GetComponent<Image>().sprite;//image
        infoPanelObject.transform.GetChild(1).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = iconName;//name

    }
}
