using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

// todo - virgula

namespace _01_Assignment_ASC_Conversie
{
    class Program
    {
        static void Main(string[] args)
        { 
            int numar;            
            do
            {
                Console.Write("Introduceti numarul in baza 10 pe care vreti" +
                " la baza tinta (daca inputul este negativ, o sa fi luat modulul): ");
                if (!int.TryParse(Console.ReadLine(), out numar))
                {
                    Console.WriteLine("Doar NUMAR.");
                }
                else
                {
                    numar = Math.Abs(numar);
                    break;
                }
            } while (true);


            int finishResult = numar;
            int bazaTinta;
            do
            {
                Console.Write("Introduceti baza de tinta([2-16]): ");
                if (!int.TryParse(Console.ReadLine(), out bazaTinta))
                {
                    Console.WriteLine("Doar NUMAR.");
                }
            } while (bazaTinta < 2 || bazaTinta > 16);

            string result = "";


            if (bazaTinta >= 2 && bazaTinta <= 9)
            {
                Stack<int> stiva = new Stack<int>();

                while (numar > 0)
                {
                    int egesz = numar / bazaTinta;
                    int rest = numar % bazaTinta;
                    stiva.Push(rest);
                    numar /= bazaTinta;
                }


                while (stiva.Count > 0)
                {
                    result = result + stiva.Pop();
                }
            }


            if (bazaTinta > 10 && bazaTinta <= 16)
            {
                Stack<string> stiva = new Stack<string>();

                while (numar > 0)
                {
                    int egesz = numar / bazaTinta;
                    int rest = numar % bazaTinta;

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
                    numar /= bazaTinta;
                }

                while (stiva.Count > 0)
                {
                    result = result + stiva.Pop();
                }
            }
            Console.WriteLine($"{finishResult} in baza 10 este {result} in baza {bazaTinta}");
        }
    }
}
