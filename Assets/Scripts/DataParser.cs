using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using Newtonsoft.Json;

public class DataParser : ScriptableObject
{
    private static string pathToJsonFolder = "./Тесты";

    public static List<string> GetNamesOfTests()
    {
        var namesOfFiles = Directory.GetFiles(pathToJsonFolder);
        for (int i = 0; i < namesOfFiles.Length; i++)
        {
            var splittedFullName = namesOfFiles[i].Split('\\');
            namesOfFiles[i] = splittedFullName[splittedFullName.Length - 1].Split('.')[0];
        }

        return new List<string>(namesOfFiles);
    }

    public static void WriteWordsToFile(string fileName, List<string> words)
    {
        string jsonString = JsonConvert.SerializeObject(words);

        if (!Directory.Exists(pathToJsonFolder))
            Directory.CreateDirectory(pathToJsonFolder);

        using (FileStream fs = File.Create($"{pathToJsonFolder}/{fileName}.json"))
        {
            byte[] bytes = Encoding.Unicode.GetBytes(jsonString);
            fs.Write(bytes, 0, bytes.Length);
        }
    }

    public static List<string> ReadWordsFromFile(string fileName)
    {
        if (!Directory.Exists(pathToJsonFolder))
            Directory.CreateDirectory(pathToJsonFolder);

        using (FileStream fs = File.Open($"{pathToJsonFolder}/{fileName}.json", FileMode.OpenOrCreate))
        {
            byte[] bytes = new byte[256];
            fs.Read(bytes, 0, (int)fs.Length);
            string jsonString = Encoding.Unicode.GetString(bytes);
            var words = JsonConvert.DeserializeObject<List<string>>(jsonString);

            return words;
        }
    }
}
