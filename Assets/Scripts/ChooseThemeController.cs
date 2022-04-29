using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChooseThemeController : MonoBehaviour
{
    [SerializeField] private Transform themeContent;
    [SerializeField] private Transform testContent;
    [SerializeField] private GameObject wordGO;
    [SerializeField] private Storage storage;

    public static Theme SelectedTheme = null;
    public static Test SelectedTest = null;

    private void OnEnable()
    {
        var themes = WordsStorage.GetAllThemes();
        if (themes.Count == 0)
            return;

        for (int i = 0; i < themes.Count; i++)
        {
            var theme = Instantiate(wordGO, themeContent);
            theme.GetComponentInChildren<Text>().text = themes[i];
            theme.GetComponent<Button>().onClick.AddListener(ShowRelatedTests);
        }
    }
    
    private void ShowRelatedTests()
    {
        var selectedButton = EventSystem.current.currentSelectedGameObject;
        SelectedTheme = WordsStorage.GetThemeByName(selectedButton.GetComponentInChildren<Text>().text);
        PersonalDataHandler.IsValidated = true;

        var tests = SelectedTheme.GetTestNames();
        if (tests.Count == 0)
            return;

        for (int i = 0; i < tests.Count; i++)
        {
            var test = Instantiate(wordGO, testContent);
            test.GetComponentInChildren<Text>().text = tests[i];
            test.GetComponent<Button>().onClick.AddListener(OnTestStart);
        }
    }

    private void OnTestStart()
    {
        var currentButton = EventSystem.current.currentSelectedGameObject;
        SelectedTest = SelectedTheme.GetTestByTestName(
            currentButton.GetComponentInChildren<Text>().text);

        SceneManager.LoadScene(4);
    }

    public void BackToMenu()
    {
        PersonalDataHandler.IsValidated = false;
        SceneManager.LoadScene(0);
    }
}