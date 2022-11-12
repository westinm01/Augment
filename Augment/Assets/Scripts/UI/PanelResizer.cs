using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;
using UnityEngine.UI;

public class PanelResizer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        ResizePanel();
    }

    private void ResizePanel()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        VerticalLayoutGroup verticalLayoutGroup = GetComponent<VerticalLayoutGroup>();

        Vector2 currentSize = rectTransform.sizeDelta;
        float newY = 0;
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            RectTransform currentChild = gameObject.transform.GetChild(i).GetComponent<RectTransform>();

            newY += currentChild.sizeDelta.y;
            newY += verticalLayoutGroup.spacing;
        }
        rectTransform.sizeDelta = new Vector2(currentSize.x, newY);
    }
}
