using UnityEngine;

[CreateAssetMenu(fileName = "Storage", menuName = "ScriptObjs/Storage", order = 50)]
public class Storage : ScriptableObject
{
    private string studentName;
    private string studentClass;
    private string studentSchool;
    private string password = "q";

    public string StudentName { get => studentName; set => studentName = value; }
    public string StudentClass { get => studentClass; set => studentClass = value; }
    public string StudentSchool { get => studentSchool; set => studentSchool = value; }
    public string Password { get => password; set => password = value; }
}
