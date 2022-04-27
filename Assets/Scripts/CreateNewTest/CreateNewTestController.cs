using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreateNewTestController : MonoBehaviour
{
    [SerializeField] private GameObject optionGM;
    [SerializeField] private Transform contentPlace;
    [SerializeField] private InputField testName;

    private void Start()
    {
        if (EditOpener.IsEdit)
            FillFields();
    }

    private void FillFields()
    {
        testName.text = EditWindowController.NameOfSelectedTest;
        var wordsInTest = DataParser.ReadWordsFromFile(EditWindowController.NameOfSelectedTest);

        var words = WordsStorage.GetWords();
        foreach (var word in wordsInTest)
        {
            var newOption = Instantiate(optionGM, contentPlace);
            var dropdown = newOption.GetComponent<Dropdown>();
            dropdown.ClearOptions();
            dropdown.AddOptions(words);
            dropdown.value = dropdown.options.FindIndex(option => option.text == word);
        }
    }

    public void AddNewWord()
    {
        var newOption = Instantiate(optionGM, contentPlace);
        var dropdown = newOption.GetComponent<Dropdown>();
        var words = WordsStorage.GetWords();
        dropdown.ClearOptions();
        dropdown.AddOptions(words);
    }

    public void DeleteLastWord()
    {
        if (contentPlace.childCount == 0)
            return;

        Destroy(contentPlace.GetChild(contentPlace.childCount - 1).gameObject);
    }

    public void Done()
    {
        List<string> words = new List<string>();
        for (int i = 0; i < contentPlace.childCount; i++)
        {
            var option = contentPlace.GetChild(i).GetComponent<Dropdown>();
            string word = option.options[option.value].text;
            words.Add(word);
        }

        DataParser.DeleteFile(EditWindowController.NameOfSelectedTest);
        DataParser.AddNewTest(testName.text);
        DataParser.WriteWordsToFile(testName.text, words);
    }

    public void Cancel()
    {
        Validation.IsValidated = true;
        SceneManager.LoadScene(1);
    }
}
