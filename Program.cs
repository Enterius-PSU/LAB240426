#nullable enable

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

public static class Tasks1To5
{
    public struct Toy
    {
        public string Name;
        public int Cost;
        public int AgeMin;
        public int AgeMax;
    }

    private static readonly Random _random = new Random();

    public static void FillTextFileSingle(string filePath, int count)
    {
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            for (int i = 0; i < count; i++)
            {
                writer.WriteLine(_random.Next(-100, 101));
            }
        }
    }

    public static void FillTextFileMultiple(string filePath, int linesCount, int numbersPerLine)
    {
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            for (int i = 0; i < linesCount; i++)
            {
                StringBuilder line = new StringBuilder();
                for (int j = 0; j < numbersPerLine; j++)
                {
                    if (j > 0)
                    {
                        line.Append(' ');
                    }
                    line.Append(_random.Next(-100, 101));
                }
                writer.WriteLine(line.ToString());
            }
        }
    }

    public static int Task1(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException("Файл не найден", filePath);
        }

        int min = int.MaxValue;
        int max = int.MinValue;

        using (StreamReader reader = new StreamReader(filePath))
        {
            string? line = "";
            while ((line = reader.ReadLine()) != null)
            {
                string trimmed = line.Trim();
                if (string.IsNullOrWhiteSpace(trimmed))
                {
                    continue;
                }
                if (int.TryParse(trimmed, out int number))
                {
                    if (number < min)
                    {
                        min = number;
                    }
                    if (number > max)
                    {
                        max = number;
                    }
                }
                else
                {
                    throw new InvalidDataException("Некорректное число в файле");
                }
            }
        }

        if (min == int.MaxValue || max == int.MinValue)
        {
            throw new InvalidOperationException("Файл не содержит чисел");
        }

        int diff = max - min;
        return diff * diff;
    }

    public static int Task2(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException("Файл не найден", filePath);
        }

        int sum = 0;

        using (StreamReader reader = new StreamReader(filePath))
        {
            string? line = "";
            while ((line = reader.ReadLine()) != null)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }
                string[] parts = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string part in parts)
                {
                    if (int.TryParse(part, out int number))
                    {
                        if (number % 2 != 0)
                        {
                            sum += number;
                        }
                    }
                    else
                    {
                        throw new InvalidDataException("Некорректное число в файле");
                    }
                }
            }
        }

        return sum;
    }

    public static void Task3(string inputFilePath, string outputFilePath)
    {
        if (!File.Exists(inputFilePath))
        {
            throw new FileNotFoundException("Исходный файл не найден", inputFilePath);
        }

        using (StreamReader reader = new StreamReader(inputFilePath))
        using (StreamWriter writer = new StreamWriter(outputFilePath))
        {
            string? line = "";
            while ((line = reader.ReadLine()) != null)
            {
                if (line.Length > 0)
                {
                    char lastChar = line[line.Length - 1];
                    writer.WriteLine(lastChar);
                }
                else
                {
                    writer.WriteLine();
                }
            }
        }
    }

    public static void FillBinaryFileNumbers(string filePath, int count)
    {
        using (BinaryWriter writer = new BinaryWriter(File.Open(filePath, FileMode.Create)))
        {
            for (int i = 0; i < count; i++)
            {
                writer.Write(_random.Next(-100, 101));
            }
        }
    }

    public static int Task4(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException("Файл не найден", filePath);
        }

        int count = 0;

        using (BinaryReader reader = new BinaryReader(File.Open(filePath, FileMode.Open)))
        {
            while (reader.BaseStream.Position < reader.BaseStream.Length)
            {
                int number = reader.ReadInt32();
                if (number % 4 == 2)
                {
                    count++;
                }
            }
        }

        return count;
    }

    public static void FillBinaryFileToys(string filePath, int count)
    {
        List<Toy> toys = new List<Toy>();
        string[] possibleNames = { "Медвежонок", "Кукла", "Машинка", "Конструктор", "Пирамидка", "Мяч" };

        for (int i = 0; i < count; i++)
        {
            Toy toy = new Toy
            {
                Name = possibleNames[_random.Next(possibleNames.Length)] + (i + 1),
                Cost = _random.Next(100, 5000),
                AgeMin = _random.Next(0, 7),
                AgeMax = _random.Next(1, 8)
            };
            toys.Add(toy);
        }

        XmlSerializer serializer = new XmlSerializer(typeof(List<Toy>));
        using (StreamWriter writer = new StreamWriter(filePath))
        {
            serializer.Serialize(writer, toys);
        }
    }

    public static string Task5(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException("Файл не найден", filePath);
        }

        XmlSerializer serializer = new XmlSerializer(typeof(List<Toy>));
        List<Toy> toys;

        using (StreamReader reader = new StreamReader(filePath))
        {
            toys = (List<Toy>)serializer.Deserialize(reader);
        }

        if (toys.Count == 0)
        {
            throw new InvalidOperationException("Нет игрушек");
        }

        Toy cheapest = toys[0];
        for (int i = 1; i < toys.Count; i++)
        {
            if (toys[i].Cost < cheapest.Cost)
            {
                cheapest = toys[i];
            }
        }

        return cheapest.Name;
    }
}

public static class Tasks6To10
{
    public class SinglyLinkedListNode<T>
    {
        public T Data;
        public SinglyLinkedListNode<T>? Next;

        public SinglyLinkedListNode(T data)
        {
            Data = data;
            Next = null;
        }
    }

    public class SinglyLinkedList<T>
    {
        public SinglyLinkedListNode<T>? Head;

        public void Add(T data)
        {
            if (Head == null)
            {
                Head = new SinglyLinkedListNode<T>(data);
            }
            else
            {
                SinglyLinkedListNode<T> current = Head;
                while (current.Next != null)
                {
                    current = current.Next;
                }
                current.Next = new SinglyLinkedListNode<T>(data);
            }
        }
    }

    public static List<int> Task6()
    {
        Console.WriteLine("Введите элементы списка (целые числа) через пробел:");
        string? input = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(input))
        {
            return new List<int>();
        }
        string[] parts = input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        List<int> list = new List<int>();

        foreach (string part in parts)
        {
            if (int.TryParse(part, out int num))
            {
                list.Add(num);
            }
            else
            {
                Console.WriteLine($"Пропущено некорректное значение: {part}");
            }
        }

        if (list.Count == 0)
        {
            return list;
        }

        List<int> result = new List<int>();
        result.Add(list[0]);
        for (int i = 1; i < list.Count; i++)
        {
            if (list[i] != list[i - 1])
            {
                result.Add(list[i]);
            }
        }

        return result;
    }

    public static LinkedList<T> Task7<T>(SinglyLinkedList<T> singlyList)
    {
        LinkedList<T> doublyList = new LinkedList<T>();
        SinglyLinkedListNode<T>? current = singlyList.Head;

        while (current != null)
        {
            doublyList.AddLast(current.Data);
            current = current.Next;
        }

        return doublyList;
    }

    public static SinglyLinkedList<int> InputSinglyLinkedList()
    {
        Console.WriteLine("Введите элементы однонаправленного списка (целые числа) через пробел:");
        string? input = Console.ReadLine();
        SinglyLinkedList<int> list = new SinglyLinkedList<int>();

        if (string.IsNullOrWhiteSpace(input))
        {
            return list;
        }

        string[] parts = input.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        foreach (string part in parts)
        {
            if (int.TryParse(part, out int num))
            {
                list.Add(num);
            }
            else
            {
                Console.WriteLine($"Пропущено некорректное значение: {part}");
            }
        }

        return list;
    }

    public static void Task8()
    {
        Console.WriteLine("Введите перечень факультативов через запятую:");
        string? input = Console.ReadLine();
        HashSet<string> allElectives = new HashSet<string>();

        if (!string.IsNullOrWhiteSpace(input))
        {
            string[] electives = input.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string e in electives)
            {
                string trimmed = e.Trim();
                if (trimmed.Length > 0)
                {
                    allElectives.Add(trimmed);
                }
            }
        }

        Console.WriteLine("Введите количество студентов:");
        if (!int.TryParse(Console.ReadLine(), out int studentCount) || studentCount <= 0)
        {
            Console.WriteLine("Некорректное количество студентов.");
            return;
        }

        HashSet<string> union = new HashSet<string>();
        HashSet<string>? intersection = null;

        for (int i = 0; i < studentCount; i++)
        {
            Console.WriteLine($"Введите факультативы студента {i + 1} через запятую:");
            string? choicesInput = Console.ReadLine();
            HashSet<string> studentSet = new HashSet<string>();

            if (!string.IsNullOrWhiteSpace(choicesInput))
            {
                string[] choices = choicesInput.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string c in choices)
                {
                    string trimmed = c.Trim();
                    if (trimmed.Length > 0)
                    {
                        studentSet.Add(trimmed);
                    }
                }
            }

            foreach (string s in studentSet)
            {
                union.Add(s);
            }

            if (intersection == null)
            {
                intersection = new HashSet<string>(studentSet);
            }
            else
            {
                intersection.IntersectWith(studentSet);
            }
        }

        Console.WriteLine("\nФакультативы, которые посещает хотя бы один студент:");
        foreach (string e in union)
        {
            Console.WriteLine(e);
        }

        Console.WriteLine("\nФакультативы, которые посещают все студенты:");
        if (intersection != null)
        {
            foreach (string e in intersection)
            {
                Console.WriteLine(e);
            }
        }

        HashSet<string> none = new HashSet<string>(allElectives);
        none.ExceptWith(union);
        Console.WriteLine("\nФакультативы, которые не посещает ни один студент:");
        foreach (string e in none)
        {
            Console.WriteLine(e);
        }
    }

    public static void Task9(string filePath)
    {
        if (!File.Exists(filePath))
        {
            Console.WriteLine("Файл не найден.");
            return;
        }

        string text = File.ReadAllText(filePath, Encoding.UTF8);
        char[] separators = { ' ', '\t', '\n', '\r', ',', '.', '!', '?', ';', ':', '(', ')', '-', '"', '\'' };
        string[] words = text.Split(separators, StringSplitOptions.RemoveEmptyEntries);

        HashSet<char> chars = new HashSet<char>();

        for (int i = 0; i < words.Length; i++)
        {
            if ((i + 1) % 2 != 0)
            {
                string word = words[i];
                foreach (char c in word)
                {
                    if (char.IsLetter(c))
                    {
                        chars.Add(char.ToLower(c));
                    }
                }
            }
        }

        List<char> sortedChars = new List<char>(chars);
        for (int i = 0; i < sortedChars.Count - 1; i++)
        {
            for (int j = 0; j < sortedChars.Count - i - 1; j++)
            {
                if (sortedChars[j] > sortedChars[j + 1])
                {
                    char temp = sortedChars[j];
                    sortedChars[j] = sortedChars[j + 1];
                    sortedChars[j + 1] = temp;
                }
            }
        }

        Console.WriteLine("Символы (буквы) из слов с нечётными номерами в алфавитном порядке:");
        foreach (char c in sortedChars)
        {
            Console.Write(c + " ");
        }
        Console.WriteLine();
    }

    public static void Task10(string filePath)
    {
        if (!File.Exists(filePath))
        {
            Console.WriteLine("Файл не найден.");
            return;
        }

        string[] lines = File.ReadAllLines(filePath, Encoding.UTF8);
        if (lines.Length == 0)
        {
            Console.WriteLine("Файл пуст.");
            return;
        }

        if (!int.TryParse(lines[0].Trim(), out int N) || N <= 0 || N > 100)
        {
            Console.WriteLine("Некорректное количество учеников (1-100).");
            return;
        }

        Dictionary<string, int> counter = new Dictionary<string, int>();
        List<string> logins = new List<string>();

        for (int i = 1; i <= N && i < lines.Length; i++)
        {
            string line = lines[i].Trim();
            if (string.IsNullOrEmpty(line))
            {
                continue;
            }

            string[] parts = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length < 2)
            {
                Console.WriteLine($"Пропущена некорректная строка: {line}");
                continue;
            }

            string surname = parts[0];

            if (counter.ContainsKey(surname))
            {
                counter[surname]++;
                logins.Add(surname + counter[surname]);
            }
            else
            {
                counter[surname] = 1;
                logins.Add(surname);
            }
        }

        Console.WriteLine("Сформированные логины:");
        foreach (string login in logins)
        {
            Console.WriteLine(login);
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;

        try
        {
            string file1 = "numbers_single.txt";
            Tasks1To5.FillTextFileSingle(file1, 10);
            int result1 = Tasks1To5.Task1(file1);
            Console.WriteLine($"Задание 1: квадрат разности = {result1}");

            string file2 = "numbers_multiple.txt";
            Tasks1To5.FillTextFileMultiple(file2, 5, 4);
            int result2 = Tasks1To5.Task2(file2);
            Console.WriteLine($"Задание 2: сумма нечётных = {result2}");

            string file3in = "source_text.txt";
            File.WriteAllText(file3in, "Привет\nМир\nКак дела?\n");
            string file3out = "last_chars.txt";
            Tasks1To5.Task3(file3in, file3out);
            Console.WriteLine("Задание 3: файл создан.");

            string file4 = "numbers_binary.dat";
            Tasks1To5.FillBinaryFileNumbers(file4, 20);
            int result4 = Tasks1To5.Task4(file4);
            Console.WriteLine($"Задание 4: количество удвоенных нечётных = {result4}");

            string file5 = "toys.xml";
            Tasks1To5.FillBinaryFileToys(file5, 5);
            string cheapest = Tasks1To5.Task5(file5);
            Console.WriteLine($"Задание 5: самая дешёвая игрушка = {cheapest}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка в заданиях 1-5: {ex.Message}");
        }

        try
        {
            Console.WriteLine("\n--- Задание 6 ---");
            List<int> compressed = Tasks6To10.Task6();
            Console.WriteLine("Результат: " + string.Join(" ", compressed));

            Console.WriteLine("\n--- Задание 7 ---");
            Tasks6To10.SinglyLinkedList<int> singly = Tasks6To10.InputSinglyLinkedList();
            LinkedList<int> doubly = Tasks6To10.Task7(singly);
            Console.WriteLine("Двунаправленный список: " + string.Join(" ", doubly));

            Console.WriteLine("\n--- Задание 8 ---");
            Tasks6To10.Task8();

            Console.WriteLine("\n--- Задание 9 ---");
            string textFile = "russian_text.txt";
            File.WriteAllText(textFile, "Мама мыла раму. Папа мыл пол. Дочка играла.", Encoding.UTF8);
            Tasks6To10.Task9(textFile);

            Console.WriteLine("\n--- Задание 10 ---");
            string studentsFile = "students.txt";
            File.WriteAllLines(studentsFile, new string[] { "5", "Иванова Мария", "Петров Сергей", "Бойцова Екатерина", "Петров Иван", "Иванова Наташа" }, Encoding.UTF8);
            Tasks6To10.Task10(studentsFile);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка в заданиях 6-10: {ex.Message}");
        }
    }
}