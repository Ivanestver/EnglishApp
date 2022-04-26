using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[CreateAssetMenu(fileName = "WordStorage", menuName = "ScriptObjs", order = 50)]
public class WordsStorage : ScriptableObject
{
    private static List<WordMeaning> dictionary = new List<WordMeaning>()
    {
        new WordMeaning("Time", "Время"),
        new WordMeaning("Year", "Год"),
        new WordMeaning("People", "Люди"),
        new WordMeaning("Way", "Путь, способ"),
        new WordMeaning("Day", "День"),
        new WordMeaning("Man", "Мужчина, человек"),
        new WordMeaning("Thing", "Вещь"),
        new WordMeaning("Woman", "Женщина"),
        new WordMeaning("Life", "Жизнь"),
        new WordMeaning("Child", "Ребёнок"),
        new WordMeaning("World", "Мир"),
        new WordMeaning("School", "Школа"),
        new WordMeaning("State", "Состояние, штат"),
        new WordMeaning("Family", "Семья"),
        new WordMeaning("Student", "Студент"),
        new WordMeaning("Group", "Группа"),
        new WordMeaning("Country", "Страна"),
        new WordMeaning("Problem", "Проблема"),
        new WordMeaning("Hand", "Рука"),
        new WordMeaning("Part", "Часть"),
        new WordMeaning("Place", "Место"),
        new WordMeaning("Case", "Ситуация, случай"),
        new WordMeaning("Week", "Неделя"),
        new WordMeaning("Company", "Компания"),
        new WordMeaning("System", "Система"),
        new WordMeaning("Program", "Программа"),
        new WordMeaning("Question", "Вопрос"),
        new WordMeaning("Work", "Работа"),
        new WordMeaning("Government", "Правительство"),
        new WordMeaning("Number", "Номер"),
        new WordMeaning("Night", "Ночь"),
        new WordMeaning("Point", "Точка"),
        new WordMeaning("Home", "Домашний очаг"),
        new WordMeaning("Water", "Вода"),
        new WordMeaning("Room", "Комната"),
        new WordMeaning("Mother", "Мать"),
        new WordMeaning("Area", "Область, территория"),
        new WordMeaning("Money", "Деньги"),
        new WordMeaning("Story", "Рассказ, история"),
        new WordMeaning("Fact", "Факт"),
        new WordMeaning("Month", "Месяц"),
        new WordMeaning("Lot", "Партия (н-р, книг)"),
        new WordMeaning("Right", "Право"),
        new WordMeaning("Study", "Исследование, учеба"),
        new WordMeaning("Book", "Книга"),
        new WordMeaning("Eye", "Глаз"),
        new WordMeaning("Job", "Работа"),
        new WordMeaning("Word", "Слово"),
        new WordMeaning("Business", "Дело"),
        new WordMeaning("Issue", "Дело, случай, выпуск"),
        new WordMeaning("Side", "Сторона"),
        new WordMeaning("Kind", "Сорт, класс"),
        new WordMeaning("Head", "Голова"),
        new WordMeaning("House", "Дом"),
        new WordMeaning("Service", "Услуга"),
        new WordMeaning("Friend", "Друг"),
        new WordMeaning("Father", "Отец"),
        new WordMeaning("Power", "Власть, мощь"),
        new WordMeaning("Hour", "Час"),
        new WordMeaning("Game", "Игра"),
        new WordMeaning("Line", "Линия"),
        new WordMeaning("End", "Конец"),
        new WordMeaning("Member", "Член"),
        new WordMeaning("Law", "Закон"),
        new WordMeaning("Car", "Автомобиль"),
        new WordMeaning("City", "Город"),
        new WordMeaning("Community", "Община"),
        new WordMeaning("Name", "Имя"),
        new WordMeaning("President", "Президент"),
        new WordMeaning("Team", "Команда"),
        new WordMeaning("Minute", "Минута"),
        new WordMeaning("Idea", "Идея"),
        new WordMeaning("Kid", "Ребёнок"),
        new WordMeaning("Body", "Тело"),
        new WordMeaning("Information", "Информация"),
        new WordMeaning("Back", "Спина"),
        new WordMeaning("Parent", "Родитель"),
        new WordMeaning("Face", "Лицо"),
        new WordMeaning("Others", "Другие"),
        new WordMeaning("Level", "Уровень"),
        new WordMeaning("Office", "Офис"),
        new WordMeaning("Door", "Дверь"),
        new WordMeaning("Health", "Здоровье"),
        new WordMeaning("Person", "Человек"),
        new WordMeaning("Art", "Искусство"),
        new WordMeaning("War", "Война"),
        new WordMeaning("History", "История"),
        new WordMeaning("Party", "Вечеринка"),
        new WordMeaning("Result", "Результат"),
        new WordMeaning("Change", "Замена"),
        new WordMeaning("Morning", "Утро"),
        new WordMeaning("Reason", "Причина"),
        new WordMeaning("Research", "Исследование"),
        new WordMeaning("Girl", "Девочка, девушка"),
        new WordMeaning("Guy", "Молодой человек, парень"),
        new WordMeaning("Moment", "Момент, мгновение"),
        new WordMeaning("Air", "Воздух"),
        new WordMeaning("Teacher", "Учитель"),
        new WordMeaning("Force", "Сила"),
        new WordMeaning("Education", "Образование")
    };
    public static List<WordMeaning> Dictionary => new List<WordMeaning>(dictionary);

    public static List<string> GetWords()
    {
        var words = new List<string>();

        foreach (var entity in dictionary)
            words.Add(entity.Word);

        return words;
    }

    public static void AddNewEntity(string word, string meaning)
    {
        dictionary.Add(new WordMeaning(word, meaning));
    }

    public static WordMeaning GetEntityByWord(string word)
    {
        foreach (var wordMeaning in dictionary)
            if (wordMeaning.Word.Equals(word))
                return new WordMeaning(wordMeaning);

        return new WordMeaning();
    }

    public static void ChangeMeaningByWord(string meaning, string word)
    {
        for (int i = 0; i < dictionary.Count; i++)
            if (dictionary[i].Word.Equals(word))
            {
                dictionary[i] = new WordMeaning(word, meaning);
                break;
            }
    }

    public static void DeleteEntity(string word)
    {
        for (int i = 0; i < dictionary.Count; i++)
            if (dictionary[i].Word.Equals(word))
            {
                dictionary.Remove(dictionary[i]);
                break;
            }
    }
}

[Serializable]
public struct WordMeaning
{
    private string word;
    private string meaning;

    public WordMeaning(WordMeaning other)
    {
        word = other.word;
        meaning = other.meaning;
    }

    public WordMeaning(string word, string meaning)
    {
        this.word = word;
        this.meaning = meaning;
    }

    public string Word
    {
        get => word;
        set
        {
            if (value.Length != 0)
                word = value;
        }
    }

    public string Meaning
    {
        get => meaning;
        set
        {
            if (value.Length != 0)
                meaning = value;
        }
    }
}
