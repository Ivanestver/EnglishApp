using System.Collections.Generic;
using System.IO;

public class Theme
{
    private List<Test> tests = new List<Test>();
    private string themeName = "";
    public static string rootDirectory = "./Тесты";

    public string ThemeName { get => themeName; set => themeName = value; }

    public Theme() { }

    public Theme(string themeName)
    {
        this.themeName = themeName;
        Deserialize();
    }

    public void AddNewTest(Test test)
    {
        tests.Add(test);
    }

    public void DeleteTest(Test test)
    {
        string path = $"{rootDirectory}/{themeName}";
        test.DeleteItself(path);
        tests.Remove(test);
    }

    public void DeleteQuestionInTest(Question question, Test test)
    {
        test.DeleteQuestion(question);
    }

    public void Serialize()
    {
        string path = $"{rootDirectory}/{themeName}";
        if (Directory.Exists(path))
            Directory.Delete(path, true);

        Directory.CreateDirectory(path);

        foreach (Test test in tests)
            test.Serialize(path);
    }

    public void DeleteThisTheme()
    {
        string path = $"{rootDirectory}/{themeName}";
        if (Directory.Exists(path))
            Directory.Delete(path, true);
    }

    public void Deserialize()
    {
        string path = $"{rootDirectory}/{themeName}";
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);

        var testsDirectories = Directory.GetDirectories(path);
        foreach (var dir in testsDirectories)
        {
            var test = new Test(dir.Split('\\')[1]);
            test.Deserialize(path);
            tests.Add(test);
        }
    }

    public List<string> GetTestNames()
    {
        List<string> testNames = new List<string>();
        foreach (var test in tests)
            testNames.Add(test.TestName);

        return testNames;
    }

    public Test GetTestByTestName(string testName)
    {
        foreach (var test in tests)
            if (test.TestName.Equals(testName))
                return test;

        return new Test();
    }
}

public class Test
{
    private List<Question> questions = new List<Question>();
    private string testName = "";

    public string TestName { get => testName; set => testName = value; }
    public List<Question> Questions => questions;

    public Test()
    { }

    public Test(string name)
    {
        testName = name;
    }

    public void AddNewQuestion(Question question)
    {
        if (question == null)
            return;

        questions.Add(question);
    }

    public void DeleteQuestion(Question question)
    {
        if (question == null)
            return;

        questions.Remove(question);
    }

    public void Serialize(string folderName)
    {
        string path = $"{folderName}/{testName}";
        if (Directory.Exists(path))
            Directory.Delete(path, true);

        Directory.CreateDirectory(path);

        for (int i = 0; i < questions.Count; i++)
            using (var fs = File.CreateText($"{path}/{i}.json"))
            {
                fs.WriteLine(questions[i].Serialize());
            }
    }

    public void DeleteItself(string folderName)
    {
        string path = $"{folderName}/{testName}";
        if (Directory.Exists(path))
            Directory.Delete(path, true);
    }

    public void DeleteQuestion(Question question, string folderName)
    {
        for (int i = 0; i < questions.Count; i++)
        {
            if (questions[i].Equals(question))
            {
                string path = $"{folderName}/{testName}/{i}.json";
                if (File.Exists(path))
                    File.Delete(path);
            }
        }
    }

    public void Deserialize(string folderName)
    {
        string path = $"{folderName}/{testName}";
        var fileNames = Directory.GetFiles(path);
        for (int i = 0; i < fileNames.Length; i++)
            using (var fs = File.OpenText($"{path}/{i}.json"))
            {
                Question question = new Question();
                question.Deserialize(fs.ReadLine());
                questions.Add(question);
            }
    }

    public List<string> GetQuestionInstructions()
    {
        List<string> questionNames = new List<string>();
        foreach (var question in questions)
            questionNames.Add(question.Instruction);

        return questionNames;
    }

    public Question GetQuestionByInstruction(string instruction)
    {
        foreach (var question in questions)
            if (question.Instruction.Equals(instruction))
                return question;

        return null;
    }

    public void MoveQuestionUp(Question question)
    {
        if (questions[0] == question)
            return;

        List<Question> newQuestions = new List<Question>();
        for (int i = 1; i < questions.Count; i++)
        {
            if (questions[i] != question)
            {
                newQuestions.Add(questions[i - 1]);
            }
            else
            {
                newQuestions.Add(questions[i]);
                newQuestions.Add(questions[i - 1]);
            }
        }

        questions = newQuestions;
    }

    public void MoveQuestionDown(Question question)
    {
        if (questions[questions.Count - 1] == question)
            return;

        List<Question> newQuestions = new List<Question>();
        for (int i = 0; i < questions.Count; i++)
        {
            if (questions[i] != question)
            {
                newQuestions.Add(questions[i]);
            }
            else
            {
                newQuestions.Add(questions[i + 1]);
                newQuestions.Add(questions[i]);
                i++;
            }
        }

        questions = newQuestions;
    }
}

public class Question
{
    private string instruction = "";
    private string[] answers = new string[3]; // Первый всегда верный

    public string Instruction { get => instruction; set => instruction = value; }
    public string TrueValue => Encriptor.Decrypt(answers[0]);

    public void SetAnswer(int number, string answer)
    {
        if (number >= answers.Length)
            return;

        answers[number] = Encriptor.Encript(answer);
    }

    public void SetAnswer(string[] answers)
    {
        this.answers = answers;
    }

    public string GetAnswerByNumber(int number)
    {
        if (number >= answers.Length)
            return "";

        return Encriptor.Decrypt(answers[number]);
    }

    public string Serialize()
    {
        return $"{answers[0]};{answers[1]};{answers[2]};{instruction.Replace(' ', '_')}";
    }

    public void Deserialize(string str)
    {
        var splitted = str.Split(';');
        answers[0] = splitted[0];
        answers[1] = splitted[1];
        answers[2] = splitted[2];
        instruction = splitted[3].Replace('_', ' ');
    }
}