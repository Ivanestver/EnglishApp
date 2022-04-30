using System.Collections.Generic;
using System.Text;
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
        for (int i = 0; i < tests.Count; i++)
        {
            var newQuestion = Instantiate(optionGM, contentPlace);
            newQuestion.GetComponentInChildren<Text>().text = $"#{i + 1}. {tests[i]}";
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
            var splitted = clickedButton.GetComponentInChildren<Text>().text.Split(' ');
            StringBuilder builder = new StringBuilder();
            builder.Append(splitted[1]);
            for (int i = 2; i < splitted.Length; i++)
                builder.Append(" " + splitted[i]);

            SelectedQuestion = EditWindowController.SelectedTest.
                GetQuestionByInstruction(builder.ToString());
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
        SceneManager.LoadScene(2);
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

        SceneManager.LoadScene(2);
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
