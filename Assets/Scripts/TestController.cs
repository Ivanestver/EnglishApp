using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TestController : MonoBehaviour
{
    [SerializeField] private Text wordField;
    [SerializeField] private GameObject[] options;
    [SerializeField] private float secondsToWait;
    [SerializeField] private Button submit, nextQuestion;

    [SerializeField] private GameObject testPanel;
    [SerializeField] private GameObject resultPanel;

    [SerializeField] private Text correct;
    [SerializeField] private Text correctPersent;
    [SerializeField] private Text wrong;
    [SerializeField] private Text wrongPersent;

    [SerializeField] private Sprite successSprite;
    [SerializeField] private Sprite failSprite;

    [SerializeField] private Color selectedColor;

    private List<Question> words;
    private int correctCount = 0;
    private int wrongCount = 0;
    private int count = 0;
    private Color defaultColor = new Color();
    private Question currentQuestion = null;
    private int currentWordPlace = 0;
    private Image[] testButtonImages = new Image[3];
    private bool isChosen = false;

    private void Start()
    {
        words = ChooseTestHandler.SelectedTest.Questions;
        count = words.Count;

        submit.onClick.AddListener(OnSubmitButtonClick);
        nextQuestion.onClick.AddListener(OnNextPressed);

        testButtonImages[0] = options[0].GetComponent<Image>();
        testButtonImages[1] = options[1].GetComponent<Image>();
        testButtonImages[2] = options[2].GetComponent<Image>();
        foreach (var option in options)
        {
            option.GetComponent<Button>().onClick.AddListener(OnOptionClick);
        }

        defaultColor = options[0].GetComponent<Image>().color;

        Step();
    }

    public void OnOptionClick()
    {
        var clickedButton = EventSystem.current.currentSelectedGameObject;
        var clickedButtonImageComponent = clickedButton.GetComponent<Image>();

        foreach (var image in testButtonImages)
            image.color = defaultColor;

        if (clickedButtonImageComponent.color == defaultColor)
        {
            clickedButtonImageComponent.color = selectedColor;
        }
        else
        {
            clickedButtonImageComponent.color = defaultColor;
        }
    }

    private void OnSubmitButtonClick()
    {
        for (int i = 0; i < options.Length; i++)
        {
            if (options[i].GetComponent<Image>().color != defaultColor)
            {
                if (options[i].GetComponentInChildren<Text>().text.Equals(currentQuestion.TrueValue))
                {
                    correctCount++;
                    OnSuccess(options[i].transform.GetChild(1).gameObject);
                }
                else
                {
                    wrongCount++;
                    OnFail(options[currentWordPlace].transform.GetChild(1).gameObject, options[i].transform.GetChild(1).gameObject);
                }

                break;
            }
        }

        isChosen = true;
    }

    private void OnSuccess(GameObject result)
    {
        result.SetActive(true);
        result.GetComponent<Image>().sprite = successSprite;
    }

    private void OnFail(GameObject correct, GameObject wrong)
    {
        correct.SetActive(true);
        wrong.SetActive(true);
        correct.GetComponent<Image>().sprite = successSprite;
        wrong.GetComponent<Image>().sprite = failSprite;
    }

    private void OnNextPressed()
    {
        if (!isChosen)
            return;

        foreach (var btn in options)
        {
            btn.transform.GetChild(1).gameObject.SetActive(false);
            btn.GetComponent<Image>().color = defaultColor;
        }

        if (!Step())
            FinishTest();
        isChosen = false;
    }

    private bool Step()
    {
        if (words.Count == 0)
            return false;

        currentQuestion = words[Random.Range(0, words.Count)];

        currentWordPlace = Random.Range(0, 3);
        int wrongOptionOnePlace = Random.Range(0, 3);
        while (wrongOptionOnePlace == currentWordPlace)
            wrongOptionOnePlace = Random.Range(0, 3);

        int wrongOptionTwoPlace = 0;
        if (currentWordPlace == 0)
            wrongOptionTwoPlace = wrongOptionOnePlace == 1 ? 2 : 1;
        else if (currentWordPlace == 1)
            wrongOptionTwoPlace = wrongOptionOnePlace == 0 ? 2 : 0;
        else
            wrongOptionTwoPlace = wrongOptionOnePlace == 0 ? 1 : 0;

        wordField.text = currentQuestion.Instruction;
        options[currentWordPlace].GetComponentInChildren<Text>().text = currentQuestion.TrueValue;
        options[wrongOptionOnePlace].GetComponentInChildren<Text>().text = currentQuestion.GetAnswerByNumber(1);
        options[wrongOptionTwoPlace].GetComponentInChildren<Text>().text = currentQuestion.GetAnswerByNumber(2);

        words.Remove(currentQuestion);

        return true;
    }

    private void FinishTest()
    {
        testPanel.SetActive(false);
        resultPanel.SetActive(true);

        correct.text = correctCount.ToString();
        correctPersent.text = ((double)correctCount / count).ToString();
        wrong.text = wrongCount.ToString();
        wrongPersent.text = ((double)wrongCount / count).ToString();
    }

    public void BackToChooseTest()
    {
        PersonalDataHandler.IsValidated = true;
        SceneManager.LoadScene(2);
    }
}
