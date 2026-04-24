using System.Text;
using System.Xml.Serialization;

public static class Tasks6To10
{
    public class SinglyLinkedListNode<T>
    {
        public T Data;
        public SinglyLinkedListNode<T>? Next;
        public SinglyLinkedListNode(T data) { 
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
                var current = Head;
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

        string[] parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
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

        Console.WriteLine("Исходный список: " + string.Join(" ", list));

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
        var current = singlyList.Head;
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

        string[] parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
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
        string? allInput = Console.ReadLine();
        string trimmedFaculties = "";
        HashSet<string> allElectives = new HashSet<string>();
        if (!string.IsNullOrWhiteSpace(allInput))
        {
            trimmedFaculties = "";
            foreach (string e in allInput.Split(',', StringSplitOptions.RemoveEmptyEntries))
            {
                trimmedFaculties = e.Trim();
                if (trimmedFaculties.Length > 0)
                {
                    allElectives.Add(trimmedFaculties);
                }
            }
        }

        Console.WriteLine("Введите количество студентов:");
        if (!int.TryParse(Console.ReadLine(), out int studentCount) || studentCount <= 0)
        {
            Console.WriteLine("Некорректное количество студентов.");
            return;
        }

        List<HashSet<string>> studentChoices = new List<HashSet<string>>();
        string? choicesInput = "";
        string trimmedStudentFaculties = "";
        HashSet<string>? studentFacultySet = null;
        for (int i = 0; i < studentCount; i++)
        {
            Console.WriteLine($"Введите факультативы студента {i + 1} через запятую:");
            choicesInput = Console.ReadLine();
            studentFacultySet = new HashSet<string>();
            if (!string.IsNullOrWhiteSpace(choicesInput))
            {
                foreach (string c in choicesInput.Split(',', StringSplitOptions.RemoveEmptyEntries))
                {
                    trimmedStudentFaculties = c.Trim();
                    if (trimmedStudentFaculties.Length > 0)
                    {
                        studentFacultySet.Add(trimmedStudentFaculties);
                    }
                }
            }
            studentChoices.Add(studentFacultySet);
        }

        Console.WriteLine("\n--- Исходные данные ---");
        Console.WriteLine("Все возможные факультативы: " + string.Join(", ", allElectives));
        Console.WriteLine("Количество студентов: " + studentCount);
        for (int i = 0; i < studentChoices.Count; i++)
        {
            Console.WriteLine($"Студент {i + 1}: " + string.Join(", ", studentChoices[i]));
        }
        Console.WriteLine("---");

        HashSet<string> union = new HashSet<string>();
        HashSet<string>? intersection = null;
        foreach (var studentSet in studentChoices)
        {
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

        string fileContent = File.ReadAllText(filePath, Encoding.UTF8);
        Console.WriteLine("Содержимое файла:");
        Console.WriteLine(fileContent);

        string text = fileContent;
        char[] separators = { ' ', '\t', '\n', '\r', ',', '.', '!', '?', ';', ':', '(', ')', '-', '"', '\'' };
        string[] words = text.Split(separators, StringSplitOptions.RemoveEmptyEntries);

        HashSet<char> chars = new HashSet<char>();
        for (int i = 0; i < words.Length; i++)
        {
            if ((i + 1) % 2 != 0) 
            {
                foreach (char c in words[i])
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

        Console.WriteLine("Содержимое файла:");
        foreach (string lineInFile in lines)
        {
            Console.WriteLine(lineInFile);
        }
        Console.WriteLine();

        if (!int.TryParse(lines[0].Trim(), out int N) || N <= 0 || N > 100)
        {
            Console.WriteLine("Некорректное количество учеников (1-100).");
            return;
        }

        Dictionary<string, int> counter = new Dictionary<string, int>();
        List<string> logins = new List<string>();

        string[]? parts = null;
        string? surname = "";
        string? line = "";
        for (int i = 1; i <= N && i < lines.Length; i++)
        {
            line = lines[i].Trim();
            if (string.IsNullOrEmpty(line))
            {
                continue;
            }

            parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length < 2)
            {
                continue;
            }

            surname = parts[0];
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