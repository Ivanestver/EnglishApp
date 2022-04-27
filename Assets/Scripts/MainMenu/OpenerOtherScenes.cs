using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenerOtherScenes : MonoBehaviour
{
    public void OpenStartTesting()
    {
        SceneManager.LoadScene(2);
    }

    public void OpenEditor()
    {
        SceneManager.LoadScene(1);
    }

    public void Exit()
    {
        print("Done");
        Application.Quit();
    }
}
