using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TestListController : MonoBehaviour
{
    [SerializeField] private Transform content;
    [SerializeField] private GameObject wordGO;

    public static string NameOfTest = "";

    private void Start()
    {
        var tests = DataParser.GetAllTests();
        if (tests.Count == 0)
            return;

        for (int i = 0; i < tests.Count - 1; i++)
        {
            var word = Instantiate(wordGO, content);
            word.GetComponentInChildren<Text>().text = tests[i];
            word.GetComponent<Button>().onClick.AddListener(StartTesting);
        }
    }
    
    private void StartTesting()
    {
        var selectedTest = EventSystem.current.currentSelectedGameObject;
        NameOfTest = selectedTest.GetComponentInChildren<Text>().text;
        SceneManager.LoadScene(4);
    }
}