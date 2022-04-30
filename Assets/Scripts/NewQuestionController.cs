using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NewQuestionController : MonoBehaviour
{
    [SerializeField] private InputField instructionField, trueField, falseField1, falseField2;

    void Start()
    {
        var question = CreateNewTestController.SelectedQuestion;

        instructionField.text = question.Instruction;
        trueField.text = question.GetAnswerByNumber(0);
        falseField1.text = question.GetAnswerByNumber(1);
        falseField2.text = question.GetAnswerByNumber(2);
    }

    public void OnSubmit()
    {
        string instruction = instructionField.text;
        string trueFieldText = trueField.text;
        string falseField1Text = falseField1.text;
        string falseField2Text = falseField2.text;

        var question = CreateNewTestController.SelectedQuestion;

        question.Instruction = instructionField.text;
        question.SetAnswer(0, trueField.text);
        question.SetAnswer(1, falseField1.text);
        question.SetAnswer(2, falseField2.text);

        SceneManager.LoadScene(4);
    }

    public void OnCancel()
    {
        if (CreateNewTestController.SelectedQuestion.Instruction.Length == 0)
        {
            CreateNewThemeController.SelectedTest.DeleteQuestion(CreateNewTestController.SelectedQuestion);
            CreateNewTestController.SelectedQuestion = null;
        }
        
        SceneManager.LoadScene(4);
    }
}
