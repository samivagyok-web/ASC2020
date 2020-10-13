using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace _01_Assignment_ASC_Conversie
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.Write("Introduceti numarul in baza 10 pe care vreti" +
                " la baza tinta: ");
            int numar = int.Parse(Console.ReadLine());

            int bazaTinta;
            do
            {
                Console.Write("Introduceti baza de tinta: ");
                bazaTinta = int.Parse(Console.ReadLine());
            } while (bazaTinta < 2 || bazaTinta > 16);


            string result = "";


            if (bazaTinta >= 2 && bazaTinta <= 9)
            {
                Stack<int> stiva = new Stack<int>();

                while (numar > 0)
                {
                    int egesz = numar / bazaTinta;
                    int maradek = numar % bazaTinta;
                    stiva.Push(maradek);
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
                    int maradek = numar % bazaTinta;

                    switch (maradek)
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
                            stiva.Push($"{maradek}");
                            break;
                    }
                    numar /= bazaTinta;
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
