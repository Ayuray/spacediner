using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuNavigation : MonoBehaviour
{
   public void toAudioSettings()
    {
        SceneManager.LoadScene("AudioSettings");
    }
}
