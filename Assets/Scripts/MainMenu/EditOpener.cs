using UnityEngine;
using UnityEngine.SceneManagement;

public class EditOpener : MonoBehaviour
{
    public void OpenEditWindow()
    {
        SceneManager.LoadScene(3);
    }
}
