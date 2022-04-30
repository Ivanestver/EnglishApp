using System.Collections.Generic;
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
    [SerializeField] private Color selectedColor;

    public static Theme SelectedTheme = null;
    public static Test SelectedTest = null;

    // Чтобы проще назначать цвета
    private List<Image> themeButtonImages = new List<Image>();
    private Color defaultColor;

    private void OnEnable()
    {
        defaultColor = wordGO.GetComponent<Image>().color;
        var themes = WordsStorage.GetAllThemes();
        if (themes.Count == 0)
            return;

        for (int i = 0; i < themes.Count; i++)
        {
            var theme = Instantiate(wordGO, themeContent);
            theme.GetComponentInChildren<Text>().text = themes[i];
            theme.GetComponent<Button>().onClick.AddListener(ShowRelatedTests);
            themeButtonImages.Add(theme.GetComponent<Image>());
        }
    }
    
    private void ShowRelatedTests()
    {
        var selectedButton = EventSystem.current.currentSelectedGameObject; 
        var clickedButtonImageComponent = selectedButton.GetComponent<Image>();

        foreach (var image in themeButtonImages)
            image.color = defaultColor;

        clickedButtonImageComponent.color = selectedColor;

        SelectedTheme = WordsStorage.GetThemeByName(selectedButton.GetComponentInChildren<Text>().text);
        PersonalDataHandler.IsValidated = true;

        ClearTests();
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

    private void ClearThemes()
    {
        if (themeContent.childCount == 0)
            return;

        for (int i = 0; i < themeContent.childCount; i++)
            Destroy(themeContent.GetChild(i).gameObject);
    }

    private void ClearTests()
    {
        if (testContent.childCount == 0)
            return;

        for (int i = 0; i < testContent.childCount; i++)
            Destroy(testContent.GetChild(i).gameObject);
    }

    private void OnTestStart()
    {
        var currentButton = EventSystem.current.currentSelectedGameObject;
        SelectedTest = SelectedTheme.GetTestByTestName(
            currentButton.GetComponentInChildren<Text>().text);

        SceneManager.LoadScene(6);
    }

    public void BackToMenu()
    {
        PersonalDataHandler.IsValidated = false;
        SceneManager.LoadScene(0);
    }
}