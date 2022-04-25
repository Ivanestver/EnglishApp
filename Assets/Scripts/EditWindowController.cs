using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditWindowController : MonoBehaviour
{
    [SerializeField] private Transform place;
    [SerializeField] private GameObject textObj;

    void Start()
    {
        var tests = WordsStorage.NamesOfTests;
        if (tests.Count == 0)
            return;

        foreach (var word in tests)
        {
            var wordGO = Instantiate(textObj, place);
            wordGO.transform.GetComponentInChildren<Text>().text = word;
        }
    }
}
