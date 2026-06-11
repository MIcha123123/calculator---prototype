using System;
using System.Collections.Generic;

namespace BadTerminalCalculator
{
    public class OperationRecord
    {
        public string OperationName;
        public double Input1;
        public double Input2;
        public double Result;
        public int IterationsCount;
    }

    public class Calculator
    {
        private static List<OperationRecord> _operationHistory = new List<OperationRecord>();

        public double Add(double a, double b)
        {
            double result = a + b;
            SaveToHistory("Dodawanie", a, b, result, 1);
            return result;
        }

        public double Subtract(double a, double b)
        {
            double result = a - b;
            SaveToHistory("Odejmowanie", a, b, result, 1);
            return result;
        }

        public double Multiply(double a, double b)
        {
            double result = a * b;

            result = Math.Round(result, 4);

            SaveToHistory("Mnozenie", a, b, result, 1);

            return result;
        }

        public double Divide(double a, double b)
        {
            if (b == 0)
            {
                throw new DivideByZeroException("Nie mozna dzielic przez zero!");
            }
            double result = a / b;
            SaveToHistory("Dzielenie", a, b, result, 1);
            return result;
        }

        private void SaveToHistory(string opName, double in1, double in2, double res, int iterations)
        {
            
            _operationHistory.Add(new OperationRecord
            {
                OperationName = opName,
                Input1 = in1,
                Input2 = in2,
                Result = res,
                IterationsCount = iterations
            });
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Calculator calc = new Calculator();
            bool keepRunning = true;

            while (keepRunning)
            {
                Console.Clear();
                Console.WriteLine("=== KALKULATOR TERMINALOWY (Z HISTORIA OPERACJI) ===");
                Console.WriteLine("1. Dodawanie");
                Console.WriteLine("2. Odejmowanie");
                Console.WriteLine("3. Mnozenie ");
                Console.WriteLine("4. Dzielenie");
                Console.WriteLine("5. Wyjscie");
                Console.WriteLine("====================================================");
                Console.Write("Wybierz opcje (1-5): ");

                string choice = Console.ReadLine();

                if (choice == "5")
                {
                    keepRunning = false;
                    continue;
                }

                Console.Write("Podaj pierwsza liczbe: ");
                if (!double.TryParse(Console.ReadLine(), out double num1))
                {
                    Console.WriteLine("To nie jest poprawna liczba!");
                    Console.ReadKey();
                    continue;
                }

                Console.Write("Podaj druga liczbe: ");
                if (!double.TryParse(Console.ReadLine(), out double num2))
                {
                    Console.WriteLine("To nie jest poprawna liczba!");
                    Console.ReadKey();
                    continue;
                }

                double result = 0;

                try
                {
                    switch (choice)
                    {
                        case "1":
                            result = calc.Add(num1, num2);
                            Console.WriteLine($"Wynik dodawania: {result}");
                            break;
                        case "2":
                            result = calc.Subtract(num1, num2);
                            Console.WriteLine($"Wynik odejmowania: {result}");
                            break;
                        case "3":
                            Console.WriteLine("Trwa obliczanie i generowanie logow...");
                            result = calc.Multiply(num1, num2);
                            Console.WriteLine($"Wynik mnozenia: {result}");
                            break;
                        case "4":
                            result = calc.Divide(num1, num2);
                            Console.WriteLine($"Wynik dzielenia: {result}");
                            break;
                        default:
                            Console.WriteLine("Nieznana opcja. Sprobuj ponownie.");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Blad: {ex.Message}");
                }

                Console.WriteLine("\nNacisnij dowolny klawisz, aby kontynuowac...");
                Console.ReadKey();
            }
        }
    }
}