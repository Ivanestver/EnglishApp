using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StatController : MonoBehaviour
{
    [SerializeField] private Storage storage;
    [SerializeField] private GameObject statGO;
    [SerializeField] private Transform place;

    private void Start()
    {
        var results = storage.Results;

        foreach (var result in results)
        {
            var stat = Instantiate(statGO, place);

            stat.transform.Find("Name").GetComponentInChildren<Text>().text = result.First.Name;
            stat.transform.Find("Theme Name").GetComponentInChildren<Text>().text = result.Second.ThemeName;
            stat.transform.Find("Test").GetComponentInChildren<Text>().text = result.Second.TestName;
            stat.transform.Find("Correct").GetComponentInChildren<Text>().text = result.Second.CorrectPersent.ToString();
            stat.transform.Find("Wrong").GetComponentInChildren<Text>().text = result.Second.WrongPersent.ToString();
        }
    }

    public void ClearStat()
    {
        storage.ClearStatistics();
        for (int i = 0; i < place.childCount; i++)
            Destroy(place.GetChild(i).gameObject);

        Start();
    }

    public void BackToMenu()
    {
        Validation.IsValidated = false;
        SceneManager.LoadScene(0);
    }
}
