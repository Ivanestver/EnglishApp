using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuOpener : MonoBehaviour
{
    public void OpenMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
