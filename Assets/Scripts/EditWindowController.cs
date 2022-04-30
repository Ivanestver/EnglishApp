using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EditWindowController : MonoBehaviour
{
    [SerializeField] private Transform themeContent;
    [SerializeField] private Transform testContent;
    [SerializeField] private GameObject textObj;
    [SerializeField] private Color selectedColor;
    [SerializeField] private InputField themeName;
    private Color defaultColor;
    // Чтобы проще назначать цвета для тем
    private List<Image> themeButtonImages = new List<Image>();
    // Чтобы проще назначать цвета для тестов
    private List<Image> testButtonImages = new List<Image>();

    // Так как нет встроенных способов передачи информации между сценами,
    // приходится исползовать статические переменые
    public static Theme SelectedTheme = null;
    public static Test SelectedTest = null;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        ClearThemes();
        var tests = WordsStorage.GetAllThemes();
        if (tests.Count == 0)
            return;

        for (int i = 0; i < tests.Count; i++)
        {
            var themeGO = Instantiate(textObj, themeContent);
            themeGO.transform.GetComponentInChildren<Text>().text = tests[i];
            themeGO.GetComponent<Button>().onClick.AddListener(OnThemeButtonClick);
            themeButtonImages.Add(themeGO.GetComponent<Image>());
        }

        defaultColor = textObj.GetComponent<Image>().color;
    }

    private void ClearThemes()
    {
        themeButtonImages.Clear();

        for (int i = 0; i < themeContent.childCount; i++)
            Destroy(themeContent.GetChild(i).gameObject);

        SelectedTheme = null;
    }

    private void ClearTests()
    {
        if (testContent.childCount == 0)
            return;

        testButtonImages.Clear();

        for (int i = 0; i < testContent.childCount; i++)
            Destroy(testContent.GetChild(i).gameObject);
    }

    public void OnThemeButtonClick()
    {
        var clickedButton = EventSystem.current.currentSelectedGameObject;
        var clickedButtonImageComponent = clickedButton.GetComponent<Image>();

        foreach (var image in themeButtonImages)
            image.color = defaultColor;

        clickedButtonImageComponent.color = selectedColor;
        SelectedTheme = WordsStorage.GetThemeByName(clickedButton.GetComponentInChildren<Text>().text);
        themeName.text = SelectedTheme.ThemeName;
        ShowTests();
    }

    private void ShowTests()
    {
        ClearTests();
        var tests = SelectedTheme.GetTestNames();
        foreach (var test in tests)
        {
            var newtest = Instantiate(textObj, testContent);
            newtest.GetComponentInChildren<Text>().text = test;
            newtest.GetComponent<Button>().onClick.AddListener(OnTestButtonClick);
            testButtonImages.Add(newtest.GetComponent<Image>());
        }
    }

    public void OnTestButtonClick()
    {
        var clickedButton = EventSystem.current.currentSelectedGameObject;
        var clickedButtonImageComponent = clickedButton.GetComponent<Image>();

        foreach (var image in testButtonImages)
            image.color = defaultColor;

        if (clickedButtonImageComponent.color == defaultColor)
        {
            clickedButtonImageComponent.color = selectedColor;
            SelectedTest = SelectedTheme.GetTestByTestName(clickedButton.GetComponentInChildren<Text>().text);
        }
        else
        {
            clickedButtonImageComponent.color = defaultColor;
            SelectedTest = null;
        }
    }

    public void OnDeleteThemeButtonClicked()
    {
        if (SelectedTheme == null)
            return;

        WordsStorage.DeleteTheme(SelectedTheme);
        SelectedTheme = null;
        SelectedTest = null;
        ClearTests();
        Init();
    }

    public void OnDeleteTestButtonClicked()
    {
        if (SelectedTheme == null || SelectedTest == null)
            return;

        SelectedTheme.DeleteTest(SelectedTest);
        ShowTests();
    }

    public void BackToMenu()
    {
        Validation.IsValidated = false;
        WordsStorage.SaveData();
        SceneManager.LoadScene(0);
    }

    public void OnEndThemeNameEdit(string name)
    {
        SelectedTheme.ThemeName = name;
        SelectedTheme.Serialize();
        Init();
    }

    public void AddNewTheme()
    {
        SelectedTheme = new Theme();
        ClearTests();
        var themeGO = Instantiate(textObj, themeContent);
        themeGO.GetComponent<Button>().onClick.AddListener(OnThemeButtonClick);
        themeButtonImages.Add(themeGO.GetComponent<Image>());
    }

    public void AddNewTest()
    {
        SelectedTest = new Test();
        SelectedTheme.AddNewTest(SelectedTest);
        SceneManager.LoadScene(4);
    }

    public void OnEditTestButtonClicked()
    {
        if (SelectedTest == null)
            return;

        SceneManager.LoadScene(4);
    }
}
