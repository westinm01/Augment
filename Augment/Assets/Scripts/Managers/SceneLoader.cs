using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }
    public void LoadNextScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.buildIndex + 1);
        Time.timeScale = 1f;
    }
    public void ReloadScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.buildIndex);
        Time.timeScale = 1f;
    }
    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadSceneName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
        Time.timeScale = 1f;
    }
}
