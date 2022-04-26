using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EditWindowController : MonoBehaviour
{
    [SerializeField] private Transform place;
    [SerializeField] private GameObject textObj;
    [SerializeField] private Color selectedColor;

    private Color defaultColor;

    private List<Image> testButtonImages = new List<Image>();

    void Start()
    {
        var tests = DataParser.GetNamesOfTests();
        if (tests.Count == 0)
            return;

        foreach (var word in tests)
        {
            var wordGO = Instantiate(textObj, place);
            wordGO.transform.GetComponentInChildren<Text>().text = word;
            wordGO.GetComponent<Button>().onClick.AddListener(OnTestButtonClick);
            testButtonImages.Add(wordGO.GetComponent<Image>());
        }

        defaultColor = textObj.GetComponent<Image>().color;
    }

    public void OnTestButtonClick()
    {
        var clickedButton = EventSystem.current.currentSelectedGameObject;
        var clickedButtonImageComponent = clickedButton.GetComponent<Image>();

        foreach (var image in testButtonImages)
            image.color = defaultColor;

        if (clickedButtonImageComponent.color == defaultColor)
            clickedButtonImageComponent.color = selectedColor;
        else
            clickedButtonImageComponent.color = defaultColor;

    }
}
