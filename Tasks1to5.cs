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
            StringBuilder? line = null;
            for (int i = 0; i < linesCount; i++)
            {
                line = new StringBuilder();
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
            throw new FileNotFoundException("Файл не найден:", filePath);
        }

        int min = int.MaxValue;
        int max = int.MinValue;
        using (StreamReader reader = new StreamReader(filePath))
        {
            string? line = "";
            while ((line = reader.ReadLine()) != null)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }
                if (!int.TryParse(line.Trim(), out int number))
                {
                    throw new InvalidDataException("Некорректное число в файле!");
                }
                if (number < min)
                {
                    min = number;
                }
                if (number > max)
                {
                   max = number; 
                }
            }
        }
        if (min == int.MaxValue || max == int.MinValue)
        {
            throw new InvalidOperationException("Файл не содержит чисел!");
        }

        return (max - min) * (max - min);
    }

    public static int Task2(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException("Файл не найден:", filePath);
        }

        int sum = 0;
        using (StreamReader reader = new StreamReader(filePath))
        {
            string? line = "";
            string[]? parts = null;
            while ((line = reader.ReadLine()) != null)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    continue;
                }
                parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                foreach (string part in parts)
                {
                    if (!int.TryParse(part, out int number))
                    {
                        throw new InvalidDataException("Некорректное число в файле!");
                    }
                    if (number % 2 != 0)
                    {
                        sum += number;
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
                writer.WriteLine(line.Length > 0 ? line[line.Length - 1] : "");
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
            throw new FileNotFoundException("Файл не найден:", filePath);
        }

        int count = 0;
        using (BinaryReader reader = new BinaryReader(File.Open(filePath, FileMode.Open)))
        {
            while (reader.BaseStream.Position < reader.BaseStream.Length)
            {
                if (reader.ReadInt32() % 4 == 2)
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
            toys.Add(new Toy
            {
                Name = possibleNames[_random.Next(possibleNames.Length)] + (i + 1),
                Cost = _random.Next(100, 5000),
                AgeMin = _random.Next(0, 7),
                AgeMax = _random.Next(1, 8)
            });
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
            throw new FileNotFoundException("Файл не найден:", filePath);
        }

        XmlSerializer serializer = new XmlSerializer(typeof(List<Toy>));
        List<Toy> toys;
        using (StreamReader reader = new StreamReader(filePath))
        {
            toys = (List<Toy>)serializer.Deserialize(reader)!;
        }
        if (toys.Count == 0)
        {
            throw new InvalidOperationException("Нет игрушек!");
        }

        Toy cheapest = toys[0];
        for (int i = 1; i < toys.Count; i++)
            if (toys[i].Cost < cheapest.Cost)
            {
                cheapest = toys[i];
            }
        return cheapest.Name;
    }
}