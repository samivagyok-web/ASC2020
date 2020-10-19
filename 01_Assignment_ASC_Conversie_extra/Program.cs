using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

// todo - virgula, szakasz

namespace _01_Assignment_ASC_Conversie
{
    class Program
    {
        static void Main(string[] args)
        {
            int bazaFirst;
            do
            {
                Console.Write("Introduceti baza DIN care vreti sa convertiti([2-16]): ");
                if (!int.TryParse(Console.ReadLine(), out bazaFirst))
                {
                    Console.WriteLine("Doar NUMAR intre 2 si 16 inclusiv.");
                }
            } while (bazaFirst < 2 || bazaFirst > 16);

            int bazaTinta;
            do
            {
                Console.Write("Introduceti baza IN care vreti sa convertiti([2-16]): ");
                if (!int.TryParse(Console.ReadLine(), out bazaTinta))
                {
                    Console.WriteLine("Doar NUMAR intre 2 si 16 inclusiv.");
                }
            } while (bazaTinta < 2 || bazaTinta > 16);

            int numar;
            string higherThan10;
            do
            {
                int breaker = 0;


                    Console.Write("Introduceti numarul pe care vreti convertit" 
                        + " la baza tinta (daca inputul este negativ, o sa fi luat modulul): ");

                higherThan10 = Console.ReadLine();

                if (int.TryParse(higherThan10, out numar))
                {
                    numar = Math.Abs(numar);
                    double n = Math.Floor(Math.Log10(numar)) + 1;
                    int irrelevant = numar;

                    for (int i = 0; i < n; i++)
                    {
                        int singleNumbers = irrelevant % 10;

                        if (singleNumbers >= bazaFirst)
                        {
                            Console.WriteLine($"Invalid number input" +
                                $" - acceptable numbers: [0 - {bazaFirst - 1}]");
                            breaker++;
                            break;
                        }
                        irrelevant /= 10;
                    }
                }
                else
                {
                    int strLen = higherThan10.Length;

                    char[] acceptable = {'1', '2', '3', '4', '5', '6',
                        '7', '8', '9', '0', 'A', 'B', 'C', 'D', 'E', 'F'};

                    // arrayul este facut asa daca de ex. bazaFirst = 13, cifrele D, E, F nu vor fi acceptate
                    int goodCharacters = 16 - bazaFirst;
                    int arrLen = acceptable.Length - goodCharacters;

                    for (int i = 0; i < strLen; i++)
                    {
                        higherThan10 = higherThan10.ToUpper();
                        for (int j = 0; j < arrLen; j++)
                        {
                            if (higherThan10[i] == acceptable[j])
                            {
                                breaker = 0;
                                break;
                            }
                            else breaker++;
                        }

                        if (breaker != 0)
                            break;
                    }
                }
                if (breaker == 0) break;
            } while (true); // probabil asta-i overcomplicated, dar macar merge
            // FULL INPUT HANDLING DONE (I THINK)


            double sum = 0;
            double numLenght = Math.Floor(Math.Log10(numar)) + 1;
            if (bazaFirst >= 2 && bazaFirst <= 10)
            {
                int irrelevant = numar;
                for (int i = 0; i < numLenght; i++)
                {
                    int singleNumbers = irrelevant % 10;
                    sum = sum + (singleNumbers * Math.Pow(bazaFirst, i));
                    irrelevant /= 10;
                }
            }

            if (bazaFirst > 10 && bazaFirst <= 16)
            {
                int strLen = higherThan10.Length;

                for (int i = strLen - 1; i >= 0; i--)
                {
                    switch (higherThan10[i])
                    {
                        case '1':
                            sum = sum + (1 * Math.Pow(16, strLen - i - 1));
                            break;
                        case '2':
                            sum = sum + (2 * Math.Pow(16, strLen - i - 1));
                            break;
                        case '3':
                            sum = sum + (3 * Math.Pow(16, strLen - i - 1));
                            break;
                        case '4':
                            sum = sum + (4 * Math.Pow(16, strLen - i - 1));
                            break;
                        case '5':
                            sum = sum + (5 * Math.Pow(16, strLen - i - 1));
                            break;
                        case '6':
                            sum = sum + (6 * Math.Pow(16, strLen - i - 1));
                            break;
                        case '7':
                            sum = sum + (7 * Math.Pow(16, strLen - i - 1));
                            break;
                        case '8':
                            sum = sum + (8 * Math.Pow(16, strLen - i - 1));
                            break;
                        case '9':
                            sum = sum + (9 * Math.Pow(16, strLen - i - 1));
                            break;
                        case 'A':
                            sum = sum + (10 * Math.Pow(16, strLen - i - 1));
                            break;
                        case 'B':
                            sum = sum + (11 * Math.Pow(16, strLen - i - 1));
                            break;
                        case 'C':
                            sum = sum + (12 * Math.Pow(16, strLen - i - 1));
                            break;
                        case 'D':
                            sum = sum + (13 * Math.Pow(16, strLen - i - 1));
                            break;
                        case 'E':
                            sum = sum + (14 * Math.Pow(16, strLen - i - 1));
                            break;
                        case 'F':
                            sum = sum + (15 * Math.Pow(16, strLen - i - 1));
                            break;
                        default:
                            break;
                    }
                }
            }

            string result = "";

            if (bazaTinta >= 2 && bazaTinta <= 10)
            {
                Stack<int> stiva = new Stack<int>();

                while (sum > 0)
                {
                    int egesz = (int)sum / bazaTinta;
                    int rest = (int)sum % bazaTinta;
                    stiva.Push(rest);
                    sum = (int)sum / bazaTinta;
                }

                while (stiva.Count > 0)
                {
                    result = result + stiva.Pop();
                }
            }

            if (bazaTinta > 10 && bazaTinta <= 16)
            {
                Stack<string> stiva = new Stack<string>();

                while (sum > 0)
                {
                    int egesz = (int)sum / bazaTinta;
                    int rest = (int)sum % bazaTinta;

                    switch (rest)
                    {
                        case 10:
                            stiva.Push("A");
                            break;
                        case 11:
                            stiva.Push("B");
                            break;
                        case 12:
                            stiva.Push("C");
                            break;
                        case 13:
                            stiva.Push("D");
                            break;
                        case 14:
                            stiva.Push("E");
                            break;
                        case 15:
                            stiva.Push("F");
                            break;
                        default:
                            stiva.Push($"{rest}");
                            break;
                    }
                    sum = (int)sum / bazaTinta;
                }

                while (stiva.Count > 0)
                {
                    result = result + stiva.Pop();
                }
            }
            Console.WriteLine(result);
        }
    }
}