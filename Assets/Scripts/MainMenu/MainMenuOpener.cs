using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuOpener : MonoBehaviour
{
    public void OpenMainMenu()
    {
        Validation.IsValidated = false;
        SceneManager.LoadScene(0);
    }
}
