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
        SceneManager.LoadScene(6);
    }

    public void OpenStat()
    {
        SceneManager.LoadScene(8);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
