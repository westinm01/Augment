using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GenerateCode : MonoBehaviour
{
    public TextMeshProUGUI codeOutput;
    string code = "None";

    void Start()
    {
        Generation();
    }

    public void Generation()
    {
        int codeNum = Random.Range(0, 999999);
        code = codeNum.ToString("D6");
        codeOutput.text = code;
    }
}
