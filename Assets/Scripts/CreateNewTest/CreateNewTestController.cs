using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using System.Text;

public class CreateNewTestController : MonoBehaviour
{
    [SerializeField] private GameObject optionGM;
    [SerializeField] private Transform contentPlace;
    [SerializeField] private Text testName;

    public void AddNewWord()
    {
        var newOption = Instantiate(optionGM, contentPlace);
        var dropdown = newOption.GetComponent<Dropdown>();
        var words = WordsStorage.GetWords();
        dropdown.ClearOptions();
        dropdown.AddOptions(words);
    }

    public void AddNewTest()
    {
        List<string> words = new List<string>();
        for (int i = 0; i < contentPlace.childCount; i++)
        {
            var option = contentPlace.GetChild(i).GetComponent<Dropdown>();
            string word = option.options[option.value].text;
            words.Add(word);
        }

        DataParser.WriteWordsToFile(testName.text, words);
    }

    public void Cancel()
    {
        SceneManager.LoadScene(1);
    }
}
