using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AugmentorPanel : MonoBehaviour
{
    public GameObject infoPanelObject;
    public string augmentorName;
    public string augmentorTitle;
    [TextArea (3,5)]
    public string augmentDescription;
    [TextArea (5,10)]
    public string augmentorLore;

    private Button augmentorButton;

    void Start()
    {
        augmentorButton = GetComponent<Button>();
        augmentorButton.onClick.AddListener(DisplayAugmentorInfo);
    }

    void DisplayAugmentorInfo()
    {
        infoPanelObject.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = this.transform.GetChild(0).gameObject.GetComponent<Image>().sprite;
        infoPanelObject.transform.GetChild(1).gameObject.GetComponent<Image>().sprite = this.transform.GetChild(0).gameObject.GetComponent<Image>().sprite;
        infoPanelObject.GetComponent<Image>().color = GetComponent<Image>().color;
        infoPanelObject.transform.GetChild(2).GetChild(0).GetChild(0).GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = augmentorLore;
        //Debug.Log(infoPanelObject.transform.GetChild(2).GetChild(0).GetComponent<TextMeshProUGUI>().text);
        infoPanelObject.transform.GetChild(3).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = augmentorName;
        infoPanelObject.transform.GetChild(4).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = augmentorTitle;
        infoPanelObject.transform.GetChild(5).GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = augmentDescription;
    }
}
