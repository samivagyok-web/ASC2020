using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Remoting.Messaging;
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
            do
            {
                Console.Write("Introduceti numarul pe care vreti convertit" +
                " la baza tinta (daca inputul este negativ, o sa fi luat modulul): ");
                
                if (!int.TryParse(Console.ReadLine(), out numar))
                {
                    Console.WriteLine("Doar NUMAR.");
                }
                else
                {
                    numar = Math.Abs(numar);
                    double n = Math.Floor(Math.Log10(numar)) + 1;
                    int irrelevant = numar;

                    for (int i = 0; i < n; i++)
                    {
                        int singleNumbers = irrelevant % 10;
                        if (singleNumbers >= bazaFirst)
                        {
                            Console.WriteLine("Invalid number input");
                            break;
                        }
                        irrelevant /= 10;
                    }
                }
            } while (true);

            double sum = 0;
            double numLenght = Math.Floor(Math.Log10(numar)) + 1;
            if (bazaFirst >= 2 && bazaFirst <= 9)
            {
                int irrelevant = numar;
                for (int i = 0; i < numLenght; i++)
                {
                    int singleNumbers = irrelevant % 10;
                    sum = sum + (singleNumbers*Math.Pow(bazaFirst, i));
                    irrelevant /= 10;
                }
            }

            if (bazaFirst > 10 && bazaFirst <= 16)
            {

            }

             string result = "";

             if (bazaTinta >= 2 && bazaTinta <= 9)
             {
                 Stack<int> stiva = new Stack<int>();

                while (sum > 0)
                {
                    int egesz = (int)sum / bazaTinta;
                    int rest = (int)sum % bazaTinta;
                    stiva.Push(rest);
                    sum = (int)sum /  bazaTinta;
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
