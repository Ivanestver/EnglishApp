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

    public static string NameOfSelectedTest;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        Clear();
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

    private void Clear()
    {
        testButtonImages.Clear();

        for (int i = 0; i < place.childCount; i++)
            Destroy(place.GetChild(i).gameObject);
    }

    public void OnTestButtonClick()
    {
        var clickedButton = EventSystem.current.currentSelectedGameObject;
        var clickedButtonImageComponent = clickedButton.GetComponent<Image>();

        foreach (var image in testButtonImages)
            image.color = defaultColor;

        if (clickedButtonImageComponent.color == defaultColor)
        {
            clickedButtonImageComponent.color = selectedColor;
            NameOfSelectedTest = clickedButton.GetComponentInChildren<Text>().text;
        }
        else
        {
            clickedButtonImageComponent.color = defaultColor;
            NameOfSelectedTest = "";
        }
    }

    public void OnDeleteTestButtonClicked()
    {
        var clickedButton = EventSystem.current.currentSelectedGameObject;
        DataParser.DeleteFile(NameOfSelectedTest);
        Init();
    }
}
