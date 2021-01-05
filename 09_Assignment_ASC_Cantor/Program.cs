using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _09_Assignment_ASC_Cantor
{
    class Program
    {
        static void Main(string[] args)
        {
            // 1
            int[] numereDeConvertit = new int[] { 2, 7, 19, 87, 1000, 1000000 };
            for (int i = 0; i < numereDeConvertit.Length; i++)
            {
                toCantor(numereDeConvertit[i]);
            }

            // 2
            int numar = getNumber();
            toCantor(numar);

            // 3
            Console.Write("Introduceti o expansie cantor (format: a_n * n! + a_n2 * (n-1)!... - spaces between members): ");
            string cantor = Console.ReadLine();
            List<int> coeff = cantorParsing(cantor);
            Console.WriteLine(toDecimal(coeff));

            // 4 
            Console.Write("Introduceti o expansie cantor (format: a_n * n! + a_n2 * (n-1)!... - spaces between members): ");
            cantor = Console.ReadLine();
            Console.Write("Introduceti un alt expansie cantor (format: a_n * n! + a_n2 * (n-1)!... - spaces between members): ");
            string cantor2 = Console.ReadLine();

            List<int> coeff2 = cantorParsing(cantor);
            int first = toDecimal(coeff2);
            List<int> coeff3 = cantorParsing(cantor2);
            int second = toDecimal(coeff3);
            toCantor(first+second);

        }

        private static int toDecimal(List<int> coeff)
        {
            int x = 0;
            int n = coeff.Count;
            for (int i = 0; i < coeff.Count; i++, n--)
            {
                x = x + coeff[i];
                x = x * n;
            }

            return x;
        }

        private static List<int> cantorParsing(string cantor)
        {
            List<int> coeff = new List<int>();

            for (int i = 0; i < cantor.Length; i++)
            {
                if (cantor[i] == '*')
                    coeff.Add(int.Parse(cantor[i-2].ToString()));             
            }
            return coeff;
        }

        private static void toCantor(int numar)
        {
            int n = 1;
            int copyNumar = numar;
            List<int> coefficients = new List<int>();

            while (copyNumar != 0)
            {
                int a_n = copyNumar % (n + 1);
                coefficients.Add(a_n);
                copyNumar = (copyNumar - a_n) / (n + 1);
                n++;
            }

            Console.Write($"{numar} = ");
            for (int i = coefficients.Count - 1; i >= 1; i--)
            {
                Console.Write($"{coefficients[i]} * {i + 1}! + ");
            }
            Console.Write($"{coefficients[0]} * {1}!");
            Console.WriteLine();
        }

        private static int getNumber()
        {
            bool valid = true; 
            int num = 0;
            do
            {
                valid = true;
                try
                {
                    Console.Write("Introduceti un numar: ");
                    num = int.Parse(Console.ReadLine());
                }
                catch (Exception)
                {
                    valid = false;
                }
            } while (!valid);

            return num;
        }
    }
}