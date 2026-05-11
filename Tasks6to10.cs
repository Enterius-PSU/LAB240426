using System.Text;

internal static class Tasks6To10
{
    public static List<int> Task6()
    {
        List<int> list = new List<int>();
        bool validInput = false;
        while (!validInput)
        {
            Console.WriteLine("Введите элементы списка (целые числа) через пробел:");
            string? input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Строка не может быть пустой. Попробуйте снова.");
                continue;
            }

            string[] parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            List<int> tempList = new List<int>();
            foreach (string part in parts)
            {
                if (int.TryParse(part, out int num))
                {
                    tempList.Add(num);
                }
                else
                {
                    Console.WriteLine($"Пропущено некорректное значение: {part}");
                }
            }

            if (tempList.Count == 0)
            {
                Console.WriteLine("Не введено ни одного целого числа. Попробуйте снова.");
                continue;
            }
            list = tempList;
            validInput = true;
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

    public static LinkedList<T> Task7<T>(List<T> list)
    {
        LinkedList<T> doublyList = new LinkedList<T>();
        foreach (T item in list)
        {
            doublyList.AddLast(item);
        }
        return doublyList;
    }

    public static List<int> InputList()
    {
        List<int> list = new List<int>();
        bool validInput = false;
        while (!validInput)
        {
            Console.WriteLine("Введите элементы списка (целые числа) через пробел:");
            string? input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Строка не может быть пустой. Попробуйте снова.");
                continue;
            }

            string[] parts = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            List<int> tempList = new List<int>();
            foreach (string part in parts)
            {
                if (int.TryParse(part, out int num))
                {
                    tempList.Add(num);
                }
                else
                {
                    Console.WriteLine($"Пропущено некорректное значение: {part}");
                }
            }

            if (tempList.Count == 0)
            {
                Console.WriteLine("Не введено ни одного целого числа. Попробуйте снова.");
                continue;
            }
            list = tempList;
            validInput = true;
        }
        return list;
    }

    public static void Task8()
    {
        HashSet<string> allElectives = new HashSet<string>();
        bool electivesEntered = false;
        while (!electivesEntered)
        {
            Console.WriteLine("Введите перечень факультативов через запятую:");
            string? allInput = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(allInput))
            {
                Console.WriteLine("Список факультативов не может быть пустым. Попробуйте снова.");
                continue;
            }
            string[] electiveParts = allInput.Split(',', StringSplitOptions.RemoveEmptyEntries);
            allElectives.Clear();
            foreach (string e in electiveParts)
            {
                string trimmed = e.Trim();
                if (trimmed.Length > 0)
                {
                    allElectives.Add(trimmed);
                }
            }
            if (allElectives.Count == 0)
            {
                Console.WriteLine("Не введено ни одного факультатива. Попробуйте снова.");
                continue;
            }
            electivesEntered = true;
        }

        int studentCount = 0;
        bool validCount = false;
        while (!validCount)
        {
            Console.WriteLine("Введите количество студентов (целое число от 1 до 100):");
            string? countInput = Console.ReadLine();
            if (!int.TryParse(countInput, out studentCount) || studentCount < 1 || studentCount > 100)
            {
                Console.WriteLine("Некорректное количество студентов. Допустимы значения от 1 до 100.");
                continue;
            }
            validCount = true;
        }

        List<HashSet<string>> studentChoices = new List<HashSet<string>>();
        for (int i = 0; i < studentCount; i++)
        {
            HashSet<string> studentSet = new HashSet<string>();
            bool validStudent = false;
            while (!validStudent)
            {
                Console.WriteLine($"Введите факультативы студента {i + 1} через запятую (можно оставить пустым):");
                string? choicesInput = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(choicesInput))
                {
                    string[] choices = choicesInput.Split(',', StringSplitOptions.RemoveEmptyEntries);
                    foreach (string c in choices)
                    {
                        string trimmed = c.Trim();
                        if (trimmed.Length > 0)
                        {
                            studentSet.Add(trimmed);
                        }
                    }
                }
                validStudent = true;
            }
            studentChoices.Add(studentSet);
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
        foreach (HashSet<string> studentSet in studentChoices)
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
        else
        {
            Console.WriteLine("(нет)");
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
            Console.WriteLine($"Файл {filePath} не найден.");
            return;
        }

        string fileContent = File.ReadAllText(filePath, Encoding.UTF8);
        if (string.IsNullOrWhiteSpace(fileContent))
        {
            Console.WriteLine("Файл пуст или содержит только пробельные символы.");
            return;
        }

        Console.WriteLine("Содержимое файла:");
        Console.WriteLine(fileContent);

        char[] separators = { ' ', '\t', '\n', '\r', ',', '.', '!', '?', ';', ':', '(', ')', '-', '"', '\'' };
        string[] words = fileContent.Split(separators, StringSplitOptions.RemoveEmptyEntries);

        if (words.Length == 0)
        {
            Console.WriteLine("В файле нет слов.");
            return;
        }

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

        if (chars.Count == 0)
        {
            Console.WriteLine("Не найдено ни одной буквы в словах с нечётными номерами.");
            return;
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
            Console.WriteLine($"Файл {filePath} не найден.");
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
            Console.WriteLine("Некорректное количество учеников в первой строке (должно быть целое число от 1 до 100).");
            return;
        }

        if (lines.Length - 1 < N)
        {
            Console.WriteLine($"В файле указано {N} учеников, но данных меньше.");
            return;
        }

        Dictionary<string, int> counter = new Dictionary<string, int>();
        List<string> logins = new List<string>();

        for (int i = 1; i <= N; i++)
        {
            string line = lines[i].Trim();
            if (string.IsNullOrEmpty(line))
            {
                Console.WriteLine($"Строка {i} пуста, пропущена.");
                continue;
            }

            string[] parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length < 2)
            {
                Console.WriteLine($"Строка {i} не содержит фамилии и имени: \"{line}\". Пропущена.");
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
