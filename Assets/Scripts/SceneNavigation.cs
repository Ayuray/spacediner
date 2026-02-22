using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class SceneNavigation : MonoBehaviour
{
    public static event Action<int> OnGameStart;
    public void ToVolume()
    {
        SceneManager.LoadScene(3, LoadSceneMode.Single);
    }

    public void GameStart()
    {
        SceneManager.LoadScene(2, LoadSceneMode.Single);
        OnGameStart?.Invoke(1);
    }

    public void GameQuit()
    {
        Application.Quit();
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
}
