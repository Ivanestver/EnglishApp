using UnityEngine;
using UnityEngine.SceneManagement;

// Содержит функции для перехода из главного меню
public class OpenerOtherScenes : MonoBehaviour
{
    [SerializeField] private Storage storage;

    private void Awake()
    {
        storage.CheckDay();
    }

    public void OpenStartTesting()
    {
        SceneManager.LoadScene(1);
    }

    public void OpenEditor()
    {
        SceneManager.LoadScene(2);
    }

    public void OpenStat()
    {
        SceneManager.LoadScene(3);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
