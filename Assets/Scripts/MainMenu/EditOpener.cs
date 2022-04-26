using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class EditOpener : MonoBehaviour
{
    public static bool IsEdit = false;
    public void OpenEditWindow()
    {
        var selectedButton = EventSystem.current.currentSelectedGameObject.name;
        IsEdit = (selectedButton.Equals("Edit Test"));
        if (IsEdit && EditWindowController.NameOfSelectedTest.Length != 0)
            SceneManager.LoadScene(3);
    }

    public void OpenCreateWindow()
    {
        var selectedButton = EventSystem.current.currentSelectedGameObject.name;
        IsEdit = (selectedButton.Equals("Edit Test"));
        if (!IsEdit)
            SceneManager.LoadScene(3);
    }
}
