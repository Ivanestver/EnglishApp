using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using Newtonsoft.Json;
using System;

public class DataParser : ScriptableObject
{
    private static string pathToJsonFolder = "./Тесты";

    public static void WriteAllTests(List<string> tests)
    {
        StringBuilder builder = new StringBuilder();
        string pathToOrderFile = $"{pathToJsonFolder}/order.json";
        if (!File.Exists(pathToOrderFile))
            return;

        foreach (var test in tests)
            builder.Append(test + ";");

        File.WriteAllText(pathToOrderFile, builder.ToString());
    }

    public static void DeleteTest(string testName)
    {
        string pathToOrderFile = $"{pathToJsonFolder}/order.json";
        if (!File.Exists(pathToOrderFile))
            return;

        string order = File.ReadAllText(pathToOrderFile);
        var tests = new List<string>(order.Split(';'));

        List<string> newListOfTests = new List<string>();
        for (int i = 0; i < tests.Count - 2; i++)
        {
            if (tests[i].Equals(testName))
                continue;
            newListOfTests.Add(tests[i]);
        }

        WriteAllTests(newListOfTests);
    }

    public static string AddNewTest(string testName)
    {
        try
        {
            string pathToOrderFile = $"{pathToJsonFolder}/order.json";
            if (!File.Exists(pathToOrderFile))
                return "no file";

            string order = File.ReadAllText(pathToOrderFile);
            var tests = new List<string>(order.Split(';'));
            if (tests.IndexOf(testName) != -1)
                return "no tests";

            order += testName + ";";
            File.WriteAllText(pathToOrderFile, order);

            return "success adding";
        }
        catch(Exception e)
        {
            return e.Message;
        }
    }

    public static List<string> GetAllTests()
    {
        if (!Directory.Exists(pathToJsonFolder))
        {
            Directory.CreateDirectory(pathToJsonFolder);
        }

        string pathToOrderFile = $"{pathToJsonFolder}/order.json";
        if (!File.Exists(pathToOrderFile))
        {
            File.Create(pathToOrderFile);
            return new List<string>();
        }

        string order = File.ReadAllText(pathToOrderFile);
        return new List<string>(order.Split(';'));
    }

    public static string WriteWordsToFile(string fileName, List<string> words)
    {
        try
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(words[0]);
            for (int i = 1; i < words.Count; i++)
                builder.Append(";" + words[i]);

            if (!Directory.Exists(pathToJsonFolder))
            {
                Directory.CreateDirectory(pathToJsonFolder);
            }

            using (var fs = File.CreateText($"{pathToJsonFolder}/{fileName}.json"))
            {
                fs.Write(builder.ToString());
            }

            return "Success writing";
        }
        catch(Exception e)
        {
            return e.Message;
        }
    }

    public static List<string> ReadWordsFromFile(string fileName)
    {
        if (!Directory.Exists(pathToJsonFolder))
        {
            Directory.CreateDirectory(pathToJsonFolder);
            File.Create($"{pathToJsonFolder}/order.json");
        }

        using (var fs = File.OpenText($"{pathToJsonFolder}/{fileName}.json"))
        {
            string wordsInString = fs.ReadToEnd();
            var words = new List<string>(wordsInString.Split(';'));

            return words;
        }
    }

    public static void DeleteFile(string fileName)
    {
        string path = $"{pathToJsonFolder}/{fileName}.json";
        if (File.Exists(path))
            File.Delete(path);
    }
}
