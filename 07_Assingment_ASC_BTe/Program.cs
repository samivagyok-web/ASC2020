using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// for the people with ocd: sorry for the lowercase e in BTe
namespace _07_Assingment_ASC_BTe
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = "";
            int numar = 0;
            bool valid = true;
            do
            {
                valid = true;
                Console.Write("Introduceti numarul in baza 10: ");
                try
                {
                    input = Console.ReadLine();
                    numar = int.Parse(input);
                }
                catch (Exception)
                {
                    valid = false;
                    Console.WriteLine("Input gresit!");
                }
            } while (!valid);

            decimalToBTE(numar); 

            do
            {
                valid = true;
                Console.Write("Introduceti un numar in BTE (acceptable characters: T/0/1): ");
                input = Console.ReadLine();

                for (int i = 0; i < input.Length; i++)
                {
                    if (input[i] != 'T' && input[i] != '0' && input[i] != '1')
                    {
                        valid = false;
                        break;
                    }
                }
            } while (!valid); 

            BTEtoDecimail(input);
        }

        private static void BTEtoDecimail(string input)
        {
            double result = 0;
            for (int i = input.Length - 1; i >= 0; i--)
            {
                switch (input[i])
                {
                    case 'T':
                        result = result - Math.Pow(3, input.Length - i - 1);
                        break;
                    case '0':
                        break;
                    case '1':
                        result = result + Math.Pow(3, input.Length - i - 1);
                        break;
                    default:
                        break;
                }
            }
            Console.WriteLine(result);
        }

        private static void decimalToBTE(int numar)
        {
            string result = "";
            Stack<string> stiva = new Stack<string>();
            if (numar > 0)
            {
                while (numar > 0)
                {
                    int rest = numar % 3;
                    numar = numar / 3;
                    if (rest == 2)
                    {
                        stiva.Push("T");
                        numar++;
                    }
                    else if (rest == 1)
                    {
                        stiva.Push("1");
                    }
                    else
                    {
                        stiva.Push("0");
                    }
                }
                while (stiva.Count > 0)
                {
                    result = result + stiva.Pop();
                }
            }
            else
            {
                numar = Math.Abs(numar);
                while (numar > 0)
                {
                    int rest = numar % 3;
                    numar = numar / 3;
                    if (rest == 2)
                    {
                        stiva.Push("1");
                        numar++;
                    }
                    else if (rest == 1)
                    {
                        stiva.Push("T");
                    }
                    else
                    {
                        stiva.Push("0");
                    }
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