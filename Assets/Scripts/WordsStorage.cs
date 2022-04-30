using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[CreateAssetMenu(fileName = "WordStorage", menuName = "ScriptObjs", order = 50)]
public class WordsStorage : ScriptableObject
{
    public static List<Theme> themes = new List<Theme>();

    public static void AddTheme(Theme theme)
    {
        themes.Add(theme);
    }

    public static List<string> GetAllThemes()
    {
        if (!Directory.Exists(Theme.rootDirectory))
            Directory.CreateDirectory(Theme.rootDirectory);

        var fileNames = Directory.GetDirectories(Theme.rootDirectory);

        var themesList = new List<string>();
        if (themes == null)
            themes = new List<Theme>();

        themes.Clear();
        foreach (var fileName in fileNames)
        {
            string themeName = fileName.Split('\\')[1];
            themesList.Add(themeName);
            Theme theme = new Theme(themeName);
            themes.Add(theme);
        }

        return themesList;
    }

    public static Theme GetThemeByName(string name)
    {
        foreach (var theme in themes)
            if (theme.ThemeName.Equals(name))
                return theme;

        return new Theme();
    }

    // Полное сохранение данных
    public static void SaveData()
    {
        foreach (var theme in themes)
        {
            theme.Serialize();
        }
    }

    public static void DeleteTheme(Theme theme)
    {
        theme.DeleteThisTheme();
        themes.Remove(theme);
    }
}
