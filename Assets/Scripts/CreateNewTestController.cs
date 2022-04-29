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
        testName.text = EditWindowController.SelectedTest.TestName;
        var tests = EditWindowController.SelectedTest.GetQuestionInstructions();
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
            SelectedQuestion = EditWindowController.SelectedTest.GetQuestionByInstruction(clickedButton.GetComponentInChildren<Text>().text);
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
        EditWindowController.SelectedTest.TestName = testName.text;
        EditWindowController.SelectedTest.AddNewQuestion(SelectedQuestion);
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

        EditWindowController.SelectedTest.DeleteQuestion(SelectedQuestion);
        FillFields();
        SelectedQuestion = null;
    }

    public void Done()
    {
        EditWindowController.SelectedTest.TestName = testName.text;
        string path = $"{Theme.rootDirectory}/{EditWindowController.SelectedTheme.ThemeName}";
        EditWindowController.SelectedTest.Serialize(path);
        SceneManager.LoadScene(6);
    }

    public void Cancel()
    {
        if (EditWindowController.SelectedTest.TestName.Length == 0)
        {
            EditWindowController.SelectedTest.DeleteQuestion(SelectedQuestion);
            EditWindowController.SelectedTheme.DeleteTest(EditWindowController.SelectedTest);
            EditWindowController.SelectedTest = null;
            SelectedQuestion = null;
        }        

        SceneManager.LoadScene(6);
    }

    public void OnMoveUpButtonClicked()
    {
        if (SelectedQuestion == null)
            return;

        EditWindowController.SelectedTest.MoveQuestionUp(SelectedQuestion);
        FillFields();
        SelectedQuestion = null;
    }

    public void OnMoveDownButtonClicked()
    {
        if (SelectedQuestion == null)
            return;

        EditWindowController.SelectedTest.MoveQuestionDown(SelectedQuestion);
        FillFields();
        SelectedQuestion = null;
    }
}
