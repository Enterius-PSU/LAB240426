using System.Text;
using System.Xml.Serialization;

internal class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;

        try
        {
            string file1 = "numbers_single.txt";
            Tasks1To5.FillTextFileSingle(file1, 10);
            Console.WriteLine("Содержимое файла numbers_single.txt:");
            PrintTextFile(file1);
            int result1 = Tasks1To5.Task1(file1);
            Console.WriteLine($"Квадрат разности = {result1}\n");

            string file2 = "numbers_multiple.txt";
            Tasks1To5.FillTextFileMultiple(file2, 5, 4);
            Console.WriteLine("Содержимое файла numbers_multiple.txt:");
            PrintTextFile(file2);
            int result2 = Tasks1To5.Task2(file2);
            Console.WriteLine($"Сумма нечётных = {result2}\n");

            string file3in = "source_text.txt";
            File.WriteAllText(file3in, "Каждый день\nНепогода\nКак же так?\n", Encoding.UTF8);
            string file3out = "last_chars.txt";
            Console.WriteLine("Содержимое исходного файла source_text.txt:");
            PrintTextFile(file3in);
            Tasks1To5.Task3(file3in, file3out);
            Console.WriteLine("Задание 3: файл last_chars.txt создан.\n");

            string file4 = "numbers_binary.dat";
            Tasks1To5.FillBinaryFileNumbers(file4, 20);
            Console.WriteLine("Содержимое бинарного файла numbers_binary.dat (целые числа):");
            PrintBinaryFileNumbers(file4);
            int result4 = Tasks1To5.Task4(file4);
            Console.WriteLine($"Количество удвоенных нечётных = {result4}\n");

            string file5 = "toys.xml";
            Tasks1To5.FillBinaryFileToys(file5, 5);
            Console.WriteLine("Содержимое файла toys.xml:");
            PrintTextFile(file5);
            string cheapest = Tasks1To5.Task5(file5);
            Console.WriteLine($"Самая дешёвая игрушка = {cheapest}\n");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка в заданиях 1-5: {ex.Message}");
        }

        try
        {
            List<int> compressed = Tasks6To10.Task6();
            Console.WriteLine("Результат: " + string.Join(" ", compressed) + "\n");

            var singly = Tasks6To10.InputSinglyLinkedList();
            Console.Write("Введённый односвязный список: ");
            var current = singly.Head;
            while (current != null)
            {
                Console.Write(current.Data + " ");
                current = current.Next;
            }
            Console.WriteLine();
            LinkedList<int> doubly = Tasks6To10.Task7(singly);
            Console.WriteLine("Двунаправленный список: " + string.Join(" ", doubly) + "\n");

            Tasks6To10.Task8();
            Console.WriteLine();

            string textFile = "russian_text.txt";
            File.WriteAllText(textFile, "Мама мыла раму. Папа мыл пол. Дочка играла.", Encoding.UTF8);
            Tasks6To10.Task9(textFile);  
            Console.WriteLine();

            string studentsFile = "students.txt";
            File.WriteAllLines(studentsFile, new string[] { "7", "Иванова Мария", "Петров Сергей", "Бойцова Екатерина", "Петров Иван", "Иванова Наташа", "Петров Евгений", "Кушев Александр" }, Encoding.UTF8);
            Tasks6To10.Task10(studentsFile);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка в заданиях 6-10: {ex.Message}");
        }
    }

    static void PrintTextFile(string path)
    {
        if (!File.Exists(path))
        {
            Console.WriteLine("Файл не найден.");
            return;
        }
        string content = File.ReadAllText(path, Encoding.UTF8);
        Console.WriteLine(content);
        if (!content.EndsWith("\n"))
        {
            Console.WriteLine();
        }
    }

    static void PrintBinaryFileNumbers(string path)
    {
        if (!File.Exists(path))
        {
            Console.WriteLine("Файл не найден.");
            return;
        }
        using (BinaryReader reader = new BinaryReader(File.Open(path, FileMode.Open)))
        {
            List<int> numbers = new List<int>();
            while (reader.BaseStream.Position < reader.BaseStream.Length)
            {
                numbers.Add(reader.ReadInt32());
            }
            Console.WriteLine(string.Join(" ", numbers));
        }
    }
}