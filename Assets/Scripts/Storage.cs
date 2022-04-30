using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Storage", menuName = "ScriptObjs/Storage", order = 50)]
public class Storage : ScriptableObject
{
    [SerializeField] private string password = "q";
    public string Password { get => password; set => password = value; }

    // Статистика учеников
    [SerializeField] private List<Pair<PersonalInfo, Statictics>> results = new List<Pair<PersonalInfo, Statictics>>();
    public List<Pair<PersonalInfo, Statictics>> Results => new List<Pair<PersonalInfo, Statictics>>(results);

    public void CheckDay()
    {
        if (DateTime.Today.DayOfWeek == DayOfWeek.Monday)
            ClearStatistics();
    }

    public void AddNewEntity(string name, string classNumber, string school)
    {
        results.Add(new Pair<PersonalInfo, Statictics>(
            new PersonalInfo(name, classNumber, school),
            new Statictics()
            ));
    }

    // Последний добавленный - самый свежий. Поэтому он и возвращается
    public Pair<PersonalInfo, Statictics> CurrentEntity
    {
        get
        {
            if (results.Count == 0)
                return new Pair<PersonalInfo, Statictics>();

            return results[results.Count - 1];
        }
        set
        {
            if (results.Count == 0)
                return;

            results[results.Count - 1] = value;
        }
    }
    
    public void ClearStatistics()
    {
        results.Clear();
    }
}

[Serializable]
public struct PersonalInfo
{
    private string name;
    private string classNumber;
    private string school;

    public PersonalInfo(string name, string classNumber, string school)
    {
        this.name = name;
        this.classNumber = classNumber;
        this.school = school;
    }

    public string Name => name;
    public string ClassNumber => classNumber;
    public string School => school;
}

[Serializable]
public struct Statictics
{
    private string themeName;
    private string testName;
    private float correctPersent;
    private float wrongPersent;

    public Statictics(string themeName, string testName, float correctPersent, float wrongPersent)
    {
        this.themeName = themeName;
        this.testName = testName;
        this.correctPersent = correctPersent;
        this.wrongPersent = wrongPersent;
    }

    public string ThemeName => themeName;
    public string TestName => testName;
    public float CorrectPersent => correctPersent;
    public float WrongPersent => wrongPersent;
}

[Serializable]
public struct Pair<T1, T2>
{
    private T1 first;
    private T2 second;

    public Pair(T1 first, T2 second)
    {
        this.first = first;
        this.second = second;
    }

    public T1 First { get => first; set => first = value; }
    public T2 Second { get => second; set => second = value; }
}