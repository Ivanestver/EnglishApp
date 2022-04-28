using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChooseThemeController : MonoBehaviour
{
    [SerializeField] private Transform content;
    [SerializeField] private GameObject wordGO;
    [SerializeField] private Text welcomeText;
    [SerializeField] private Storage storage;

    public static Theme SelectedTheme = null;

    private void OnEnable()
    {
        var tests = WordsStorage.GetAllThemes();
        if (tests.Count == 0)
            return;

        for (int i = 0; i < tests.Count; i++)
        {
            var word = Instantiate(wordGO, content);
            word.GetComponentInChildren<Text>().text = tests[i];
            word.GetComponent<Button>().onClick.AddListener(MoveToChoose);
        }

        welcomeText.text = $"Здравствуйте, {storage.StudentName}. Выберите тему";
    }
    
    private void MoveToChoose()
    {
        var selectedButton = EventSystem.current.currentSelectedGameObject;
        SelectedTheme = WordsStorage.GetThemeByName(selectedButton.GetComponentInChildren<Text>().text);
        PersonalDataHandler.IsValidated = true;
        SceneManager.LoadScene(7);
    }

    public void BackToMenu()
    {
        PersonalDataHandler.IsValidated = false;
        SceneManager.LoadScene(0);
    }
}