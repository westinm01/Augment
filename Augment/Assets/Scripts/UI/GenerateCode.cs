using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GenerateCode : MonoBehaviour
{
    public TextMeshProUGUI codeOutput;
    string code = "None";

    public void Generation()
    {
        int codeNum = Random.Range(1, 1000000);
        code = codeNum.ToString("D6");
        codeOutput.text = code;
    }
}
