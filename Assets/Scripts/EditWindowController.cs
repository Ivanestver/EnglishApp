using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EditWindowController : MonoBehaviour
{
    [SerializeField] private Transform place;
    [SerializeField] private GameObject textObj;
    [SerializeField] private Color selectedColor;

    private Color defaultColor;
    private List<Image> testButtonImages = new List<Image>();

    public static Theme SelectedTheme = null;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        Clear();
        var tests = WordsStorage.GetAllThemes();
        if (tests.Count == 0)
            return;

        for (int i = 0; i < tests.Count; i++)
        {
            var themeGO = Instantiate(textObj, place);
            themeGO.transform.GetComponentInChildren<Text>().text = tests[i];
            themeGO.GetComponent<Button>().onClick.AddListener(OnThemeButtonClick);
            testButtonImages.Add(themeGO.GetComponent<Image>());
        }

        defaultColor = textObj.GetComponent<Image>().color;
    }

    private void Clear()
    {
        testButtonImages.Clear();

        for (int i = 0; i < place.childCount; i++)
            Destroy(place.GetChild(i).gameObject);

        SelectedTheme = null;
    }

    public void OnThemeButtonClick()
    {
        var clickedButton = EventSystem.current.currentSelectedGameObject;
        var clickedButtonImageComponent = clickedButton.GetComponent<Image>();

        foreach (var image in testButtonImages)
            image.color = defaultColor;

        if (clickedButtonImageComponent.color == defaultColor)
        {
            clickedButtonImageComponent.color = selectedColor;
            SelectedTheme = WordsStorage.GetThemeByName(clickedButton.GetComponentInChildren<Text>().text);
        }
        else
        {
            clickedButtonImageComponent.color = defaultColor;
            SelectedTheme = null;
        }
    }

    public void OnDeleteThemeButtonClicked()
    {
        WordsStorage.DeleteTheme(SelectedTheme);
        SelectedTheme = null;
        Init();
    }

    public void BackToMenu()
    {
        Validation.IsValidated = false;
        SceneManager.LoadScene(0);
    }

    public void AddNewTheme()
    {
        SelectedTheme = new Theme();
        SceneManager.LoadScene(1);
    }

    public void EditTheme()
    {
        SceneManager.LoadScene(1);
    }
}
