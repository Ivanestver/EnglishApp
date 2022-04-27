﻿using System.Collections;
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

    [SerializeField] private GameObject testPanel;
    [SerializeField] private GameObject resultPanel;

    [SerializeField] private Text correct;
    [SerializeField] private Text correctPersent;
    [SerializeField] private Text wrong;
    [SerializeField] private Text wrongPersent;

    [SerializeField] private Sprite successSprite;
    [SerializeField] private Sprite failSprite;

    private string testName = "";
    private List<string> words;
    private int correctCount = 0;
    private int wrongCount = 0;
    private int count = 0;
    private WordMeaning currentTest = new WordMeaning();
    private Color defaultColor = new Color();
    private bool isHeld = false;

    private void Start()
    {
        testName = TestListController.NameOfTest;
        words = DataParser.ReadWordsFromFile(testName);
        foreach (var option in options)
            option.GetComponent<Button>().onClick.AddListener(OnOptionButtonClick);

        count = words.Count;
        defaultColor = options[0].GetComponent<Image>().color;

        Step();
    }

    private void OnOptionButtonClick()
    {
        if (isHeld)
            return;

        var selectedOption = EventSystem.current.currentSelectedGameObject;
        if (selectedOption.GetComponentInChildren<Text>().text.Equals(currentTest.Meaning))
        {
            correctCount++;
            StartCoroutine(OnSuccess(selectedOption.transform.GetChild(1).gameObject));
        }
        else
        {
            wrongCount++;
            int correntNumber = 0;
            for (int i = 0; i < options.Length; i++)
                if (options[i].GetComponentInChildren<Text>().text.Equals(currentTest.Meaning))
                {
                    correntNumber = i;
                    break;
                }

            StartCoroutine(OnFail(options[correntNumber].transform.GetChild(1).gameObject, 
                selectedOption.transform.GetChild(1).gameObject));
        }
    }

    private IEnumerator OnSuccess(GameObject result)
    {
        isHeld = true;

        result.SetActive(true);
        result.GetComponent<Image>().sprite = successSprite;

        yield return new WaitForSeconds(secondsToWait);

        result.SetActive(false);

        words.Remove(currentTest.Word);
        if (!Step())
            FinishTest();

        isHeld = false;
    }

    private IEnumerator OnFail(GameObject correct, GameObject wrong)
    {
        isHeld = true;

        correct.SetActive(true);
        wrong.SetActive(true);
        correct.GetComponent<Image>().sprite = successSprite;
        wrong.GetComponent<Image>().sprite = failSprite;

        yield return new WaitForSeconds(secondsToWait);

        correct.SetActive(false);
        wrong.SetActive(false);

        words.Remove(currentTest.Word);
        if (!Step())
            FinishTest();

        isHeld = false;
    }

    private bool Step()
    {
        if (words.Count == 0)
            return false;

        int currentTestNumber = Random.Range(0, words.Count);
        string currentWord = words[currentTestNumber];
        string currentMeaning = WordsStorage.GetMeaningByWord(currentWord);
        currentTest.Word = currentWord;
        currentTest.Meaning = currentMeaning;

        var wrongOptionOne = WordsStorage.GetRandomEntity();
        var wrongOptionTwo = WordsStorage.GetRandomEntity();

        int currentWordPlace = Random.Range(0, 3);
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

        wordField.text = currentWord;
        options[currentWordPlace].GetComponentInChildren<Text>().text = currentMeaning;
        options[wrongOptionOnePlace].GetComponentInChildren<Text>().text = wrongOptionOne.Meaning;
        options[wrongOptionTwoPlace].GetComponentInChildren<Text>().text = wrongOptionTwo.Meaning;

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
        SceneManager.LoadScene(2);
    }
}