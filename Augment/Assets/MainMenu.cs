using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        //Load game scene
        SceneManager.LoadScene(1);
    }

    public void OpenShop()
    {
        //Load shop scene
        SceneManager.LoadScene(2);
    }
}
