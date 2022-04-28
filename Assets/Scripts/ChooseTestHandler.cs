using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChooseTestHandler : MonoBehaviour
{
    [SerializeField] private GameObject wordGO;
    [SerializeField] private Transform content;

    public static Test SelectedTest = null;

    private void Start()
    {
        var tests = ChooseThemeController.SelectedTheme.GetTestNames();
        if (tests.Count == 0)
            return;

        for (int i = 0; i < tests.Count; i++)
        {
            var word = Instantiate(wordGO, content);
            word.GetComponentInChildren<Text>().text = tests[i];
            word.GetComponent<Button>().onClick.AddListener(OnTestStart);
        }
    }

    private void OnTestStart()
    {
        var currentButton = EventSystem.current.currentSelectedGameObject;
        SelectedTest = ChooseThemeController.SelectedTheme.GetTestByTestName(
            currentButton.GetComponentInChildren<Text>().text);

        SceneManager.LoadScene(4);
    }

    public void BackToChooseTheme()
    {
        SelectedTest = null;
        SceneManager.LoadScene(2);
    }
}
