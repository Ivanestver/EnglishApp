using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreateNewThemeController : MonoBehaviour
{
    [SerializeField] private GameObject optionGM;
    [SerializeField] private Transform contentPlace;
    [SerializeField] private InputField themeName;
    [SerializeField] private Color selectedColor;

    public static Test SelectedTest = null;

    private Color defaultColor;
    // Чтобы проще назначать цвета
    private List<Image> testButtonImages = new List<Image>();

    private void Start()
    {
        defaultColor = optionGM.GetComponent<Image>().color;
        FillFields();
    }

    private void FillFields()
    {
        themeName.text = EditWindowController.SelectedTheme.ThemeName;
        var tests = EditWindowController.SelectedTheme.GetTestNames();
        foreach (var test in tests)
        {
            var newtest = Instantiate(optionGM, contentPlace);
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
            SelectedTest = EditWindowController.SelectedTheme.GetTestByTestName(clickedButton.GetComponentInChildren<Text>().text);
        }
        else
        {
            clickedButtonImageComponent.color = defaultColor;
            SelectedTest = null;
        }
    }

    public void AddNewTest()
    {
        EditWindowController.SelectedTheme.ThemeName = themeName.text;
        SelectedTest = new Test();
        EditWindowController.SelectedTheme.AddNewTest(SelectedTest);

        SceneManager.LoadScene(3);
    }

    public void EditSelectedTest()
    {
        if (SelectedTest == null)
            return;

        SceneManager.LoadScene(3);
    }

    public void DeleteLastTest()
    {
        if (contentPlace.childCount == 0)
            return;

        EditWindowController.SelectedTheme.DeleteTest(SelectedTest);
        Destroy(contentPlace.GetChild(contentPlace.childCount - 1).gameObject);
    }

    public void Done()
    {
        EditWindowController.SelectedTheme.ThemeName = themeName.text;
        EditWindowController.SelectedTheme.Serialize();
        SceneManager.LoadScene(6);
    }

    public void Cancel()
    {
        Validation.IsValidated = true;
        SceneManager.LoadScene(6);
    }
}
