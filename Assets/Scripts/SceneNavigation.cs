using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNavigation : MonoBehaviour
{
    public void ToVolume()
    {
        SceneManager.LoadScene(2, LoadSceneMode.Single);
    }

    public void GameStart()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public void GameQuit()
    {
        Application.Quit();
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}
