using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreateNewTestController : MonoBehaviour
{
    [SerializeField] private GameObject optionGM;
    [SerializeField] private Transform contentPlace;
    [SerializeField] private InputField testName;
    [SerializeField] private Color selectedColor;

    public static Question SelectedQuestion = null;

    private Color defaultColor;
    private List<Image> testButtonImages = new List<Image>();

    private void Start()
    {
        defaultColor = optionGM.GetComponent<Image>().color;
        FillFields();
    }

    private void FillFields()
    {
        Clear();
        testName.text = CreateNewThemeController.SelectedTest.TestName;
        var tests = CreateNewThemeController.SelectedTest.GetQuestionInstructions();
        foreach (var test in tests)
        {
            var newQuestion = Instantiate(optionGM, contentPlace);
            newQuestion.GetComponentInChildren<Text>().text = test;
            newQuestion.GetComponentInChildren<Button>().onClick.AddListener(OnQuestionButtonClick);
            testButtonImages.Add(newQuestion.GetComponent<Image>());
        }
    }

    private void Clear()
    {
        if (contentPlace.childCount == 0)
            return;

        testButtonImages.Clear();

        for (int i = 0; i < contentPlace.childCount; i++)
            Destroy(contentPlace.GetChild(i).gameObject);
    }

    public void OnQuestionButtonClick()
    {
        var clickedButton = EventSystem.current.currentSelectedGameObject;
        var clickedButtonImageComponent = clickedButton.GetComponent<Image>();

        foreach (var image in testButtonImages)
            image.color = defaultColor;

        if (clickedButtonImageComponent.color == defaultColor)
        {
            clickedButtonImageComponent.color = selectedColor;
            SelectedQuestion = CreateNewThemeController.SelectedTest.GetQuestionByInstruction(clickedButton.GetComponentInChildren<Text>().text);
        }
        else
        {
            clickedButtonImageComponent.color = defaultColor;
            SelectedQuestion = null;
        }
    }

    public void AddNewQuestion()
    {
        SelectedQuestion = new Question();
        CreateNewThemeController.SelectedTest.TestName = testName.text;
        CreateNewThemeController.SelectedTest.AddNewQuestion(SelectedQuestion);
        SceneManager.LoadScene(5);
    }

    public void EditQuestion()
    {
        if (SelectedQuestion == null)
            return;

        SceneManager.LoadScene(5);
    }

    public void DeleteLastWord()
    {
        if (contentPlace.childCount == 0)
            return;

        CreateNewThemeController.SelectedTest.DeleteQuestion(SelectedQuestion);
        FillFields();
        SelectedQuestion = null;
    }

    public void Done()
    {
        CreateNewThemeController.SelectedTest.TestName = testName.text;
        string path = $"{Theme.rootDirectory}/{EditWindowController.SelectedTheme}";
        CreateNewThemeController.SelectedTest.Serialize(path);
        SceneManager.LoadScene(1);
    }

    public void Cancel()
    {
        if (CreateNewThemeController.SelectedTest.TestName.Length == 0)
        {
            CreateNewThemeController.SelectedTest.DeleteQuestion(SelectedQuestion);
            EditWindowController.SelectedTheme.DeleteTest(CreateNewThemeController.SelectedTest);
            CreateNewThemeController.SelectedTest = null;
            SelectedQuestion = null;
        }        

        SceneManager.LoadScene(1);
    }

    public void OnMoveUpButtonClicked()
    {
        if (SelectedQuestion == null)
            return;

        CreateNewThemeController.SelectedTest.MoveQuestionUp(SelectedQuestion);
        FillFields();
        SelectedQuestion = null;
    }

    public void OnMoveDownButtonClicked()
    {
        if (SelectedQuestion == null)
            return;

        CreateNewThemeController.SelectedTest.MoveQuestionDown(SelectedQuestion);
        FillFields();
        SelectedQuestion = null;
    }
}
