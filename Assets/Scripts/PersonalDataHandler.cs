using UnityEngine;
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

    public static bool IsValidated = false;

    private void OnDisable()
    {
        IsValidated = false;
        storage.StudentName = "";
        storage.StudentClass = "";
        storage.StudentSchool = "";
    }

    public void OnSubmit()
    {
        if (IsValidated)
        {
            listPanel.SetActive(true);
            inputPanel.SetActive(false);
            return;
        }

        storage.StudentName = inputName.text;
        storage.StudentClass = inputClass.text;
        storage.StudentSchool = inputSchool.text;

        listPanel.SetActive(true);
        inputPanel.SetActive(false);
        IsValidated = true;
    }
}
