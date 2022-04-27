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

    public static string NameOfSelectedTest = "";

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        Clear();
        var tests = DataParser.GetAllTests();
        if (tests.Count == 0)
            return;

        for (int i = 0; i < tests.Count - 1; i++)
        {
            var wordGO = Instantiate(textObj, place);
            wordGO.transform.GetComponentInChildren<Text>().text = tests[i];
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

        NameOfSelectedTest = "";
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
        if (NameOfSelectedTest.Length == 0)
            return;

        DataParser.DeleteTest(NameOfSelectedTest);
        DataParser.DeleteFile(NameOfSelectedTest);
        Init();
    }

    public void OnMoveUpButtonClicked()
    {
        var clickedButton = EventSystem.current.currentSelectedGameObject;
        if (clickedButton.name.Equals("Move Up"))
        {
            var tests = DataParser.GetAllTests();
            if (tests.Count == 0)
                return;

            string selectedTest = "";
            for (int i = 0; i < place.childCount; i++)
                if (place.GetChild(i).GetComponent<Image>().color == selectedColor)
                {
                    selectedTest = place.GetChild(i).GetComponentInChildren<Text>().text;
                    break;
                }

            if (selectedTest.Length == 0)
                return;

            var testsInNewOrder = new List<string>();
            for (int i = 0; i < tests.Count - 1; i++)
            {
                if (tests[i + 1].Equals(selectedTest))
                {
                    testsInNewOrder.Add(tests[i + 1]);
                    testsInNewOrder.Add(tests[i]);
                    i++;
                    continue;
                }
                testsInNewOrder.Add(tests[i]);
            }

            DataParser.WriteAllTests(testsInNewOrder);

            Init();
        }
    }

    public void OnMoveDownButtonClicked()
    {
        var clickedButton = EventSystem.current.currentSelectedGameObject;
        if (clickedButton.name.Equals("Move Down"))
        {
            var tests = DataParser.GetAllTests();
            if (tests.Count == 0)
                return;

            string selectedTest = "";
            for (int i = 0; i < place.childCount; i++)
                if (place.GetChild(i).GetComponent<Image>().color == selectedColor)
                {
                    selectedTest = place.GetChild(i).GetComponentInChildren<Text>().text;
                    break;
                }

            if (selectedTest.Length == 0 || 
                tests.IndexOf(selectedTest) == tests.Count - 2)
                return;

            var testsInNewOrder = new List<string>();
            for (int i = 0; i < tests.Count - 1; i++)
            {
                if (tests[i].Equals(selectedTest))
                {
                    testsInNewOrder.Add(tests[i + 1]);
                    testsInNewOrder.Add(tests[i]);
                    i++;
                    continue;
                }
                testsInNewOrder.Add(tests[i]);
            }

            DataParser.WriteAllTests(testsInNewOrder);

            Init();
        }
    }
}
