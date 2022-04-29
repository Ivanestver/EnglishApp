using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PersonalDataHandler : MonoBehaviour
{
    [SerializeField] private InputField inputName;
    [SerializeField] private InputField inputClass;
    [SerializeField] private InputField inputSchool;
    [SerializeField] private Button submit;
    [SerializeField] private Storage storage;

    [SerializeField] private GameObject listPanel;
    [SerializeField] private GameObject inputPanel;

    [SerializeField] private Text alarm;

    private bool isHeld = false;

    public static bool IsValidated = false;

    private void OnEnable()
    {
        if (IsValidated)
        {
            listPanel.SetActive(true);
            inputPanel.SetActive(false);
            return;
        }
    }

    private void OnDisable()
    {
        IsValidated = false;
    }

    public void OnSubmit()
    {
        if (isHeld)
            return;

        if (IsValidated)
        {
            listPanel.SetActive(true);
            inputPanel.SetActive(false);
            return;
        }

        if (inputName.text.Length == 0)
        {
            StartCoroutine(ShowMessage("Введите ФИО"));
            return;
        }

        if (inputClass.text.Length == 0)
        {
            StartCoroutine(ShowMessage("Введите свой класс"));
            return;
        }

        if (inputSchool.text.Length == 0)
        {
            StartCoroutine(ShowMessage("Введите свою школу"));
            return;
        }

        storage.AddNewEntity(inputName.text, inputClass.text, inputSchool.text);

        listPanel.SetActive(true);
        inputPanel.SetActive(false);
        IsValidated = true;
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }

    private IEnumerator ShowMessage(string message)
    {
        isHeld = true;
        alarm.text = message;
        yield return new WaitForSeconds(1.5f);
        alarm.text = "";
        isHeld = false;
    }
}
