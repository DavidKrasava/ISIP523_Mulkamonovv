using System;
using System.Globalization;
class Bobr
{
    static void OutputData(string[] names, double[] costs)
    {
        Console.WriteLine("\nВсе операции:");
        for (int i = 0; i < names.Length; i++)
        {
            Console.WriteLine($"{names[i]}; {costs[i]:F2} руб.");
        }
    }

    static void ShowStatistics(double[] costs)
    {
        double sum = 0;
        double min = costs[0];
        double max = costs[0];

        for (int i = 0; i < costs.Length; i++)
        {
            sum += costs[i];
            if (costs[i] < min) min = costs[i];
            if (costs[i] > max) max = costs[i];
        }

        double average = sum / costs.Length;
        Console.WriteLine($"\nСтатистика:");
        Console.WriteLine($"Сумма: {sum:F2} руб.");
        Console.WriteLine($"Среднее: {average:F2} руб.");
        Console.WriteLine($"Минимальная сумма: {min:F2} руб.");
        Console.WriteLine($"Максимальная сумма: {max:F2} руб.");
    }

    static void BubbleSort(string[] names, double[] costs)
    {
        for (int i = 0; i < costs.Length - 1; i++)
        {
            for (int j = 0; j < costs.Length - i - 1; j++)
            {
                if (costs[j] > costs[j + 1])
                {
                    double tempCost = costs[j];
                    costs[j] = costs[j + 1];
                    costs[j + 1] = tempCost;

                    string tempName = names[j];
                    names[j] = names[j + 1];
                    names[j + 1] = tempName;
                }
            }
        }
    }

    static void ConvertCurrency(string[] names, double[] costs)
    {
        Console.WriteLine("\nДоступные валюты:");
        Console.WriteLine("1. Доллар (USD)");
        Console.WriteLine("2. Евро (EUR)");
        Console.WriteLine("3. Произвольный курс");

        Console.Write("Выберите валюту: ");
        string currencyChoice = Console.ReadLine();
        double rate;

        switch (currencyChoice)
        {
            case "1":
                rate = 0.011; 
                break;
            case "2":
                rate = 0.010; 
                break;
            case "3":
                Console.Write("Введите курс конвертации (рублей за единицу валюты): ");
                while (!double.TryParse(Console.ReadLine(), NumberStyles.Any, CultureInfo.InvariantCulture, out rate) || rate <= 0)
                {
                    Console.Write("Ошибка ввода. Введите положительное число: ");
                }
                break;
            default:
                Console.WriteLine("Неверный выбор.");
                return;
        }

        Console.WriteLine("\nКонвертированные данные:");
        for (int i = 0; i < costs.Length; i++)
        {
            double convertedValue = costs[i] * rate;
            string currencySymbol = currencyChoice == "1" ? "USD" : currencyChoice == "2" ? "EUR" : "ед.";
            Console.WriteLine($"{names[i]}; {convertedValue:F2} {currencySymbol}");
        }
    }

    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        int n;
        do
        {
            Console.Write("Введите количество операций (от 2 до 40): ");
        } while (!int.TryParse(Console.ReadLine(), out n) || n < 2 || n > 40);

        string[] names = new string[n];
        double[] costs = new double[n];

        for (int i = 0; i < n; i++)
        {
            while (true)
            {
                Console.Write($"Введите операцию {i + 1} в формате (Название; Сумма): ");
                string input = Console.ReadLine();
                string[] parts = input.Split(';');

                if (parts.Length != 2)
                {
                    Console.WriteLine("Ошибка формата. Используйте символ ';' для разделения.");
                    continue;
                }

                names[i] = parts[0].Trim();
                if (!double.TryParse(parts[1].Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out costs[i]))
                {
                    Console.WriteLine("Ошибка ввода суммы. Используйте числовое значение.");
                    continue;
                }

                break;
            }
        }

        while (true)
        {
            Console.WriteLine("\nМеню:");
            Console.WriteLine("1. Вывод данных");
            Console.WriteLine("2. Статистика");
            Console.WriteLine("3. Сортировка по цене");
            Console.WriteLine("4. Конвертация валюты");
            Console.WriteLine("5. Поиск по названию");
            Console.WriteLine("0. Выход");

            Console.Write("Выберите пункт меню: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    OutputData(names, costs);
                    break;
                case "2":
                    ShowStatistics(costs);
                    break;
                case "3":
                    BubbleSort(names, costs);
                    Console.WriteLine("Данные отсортированы.");
                    break;
                case "4":
                    ConvertCurrency(names, costs);
                    break;
                case "5":
                    SearchByName(names, costs);
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Неверный пункт меню.");
                    break;
            }
        }
    }

    
}

