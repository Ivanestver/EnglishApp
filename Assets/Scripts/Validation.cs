using UnityEngine;
using UnityEngine.UI;

public class Validation : MonoBehaviour
{
    [SerializeField] private Text info;
    [SerializeField] private InputField inputField;
    [SerializeField] private Button enter;
    [SerializeField] private GameObject passwordPanel;
    [SerializeField] private GameObject editorPanel;

    private int timesPasswordWasEntered = 0;
    private string password = "q";

    public static bool IsValidated = false;

    private void Start()
    {
        if (IsValidated)
        {
            passwordPanel.SetActive(false);
            editorPanel.SetActive(true);
            return;
        }
    }

    public void OnEnterButtonClicked()
    {
        if (timesPasswordWasEntered == 5)
        {
            info.text = "Вы превысили количество попыток ввести пароль!";
            enter.onClick.RemoveAllListeners();
            enter.gameObject.SetActive(false);
            timesPasswordWasEntered = 0;
            return;
        }

        string enteredPassword = inputField.text;
        if (enteredPassword.Equals(password))
        {
            passwordPanel.SetActive(false);
            editorPanel.SetActive(true);
            IsValidated = true;
            return;
        }

        timesPasswordWasEntered++;
    }
}
