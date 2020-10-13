using System;
using System.Collections.Generic;
using System.Linq;
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

            Console.Write("Introduceti baza de tinta: ");
            int bazaTinta = int.Parse(Console.ReadLine());

            string result = "";
            Stack<int> stiva = new Stack<int>();

            if (bazaTinta >= 2 && bazaTinta <= 9)
            {
                while (numar > 0)
                {
                    int egesz = numar / bazaTinta;
                    int maradek = numar % bazaTinta;
                    stiva.Push(maradek);
                    numar /= bazaTinta;
                }
            }

            while (stiva.Count > 0)
            {
                result = result + stiva.Pop();
            }

            Console.WriteLine(result);

        }
    }
}
