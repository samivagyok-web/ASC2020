using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace _02_Assingment_ASC_HexViewer
{
    class Program
    {
        static void Main(string[] args)
        {
            
            string filename = @"C:\Users\Sami\Desktop\könyv\hexviewT.txt";

            using (FileStream
                f = new FileStream(filename, FileMode.Open, FileAccess.Read))
            {
                int hexIn;
                int s = 0;
                int index = 0;
                

                for (int i = 0; (hexIn = f.ReadByte()) != -1; i++)
                {
                    if (s % 16 == 0)
                    {
                        Console.Write($"{convertToHex(index)}: ");
                        index++;
                    }

                    Console.Write($"{hexIn:X2} ");

                    s++;
                    if (s % 8 == 0)
                        Console.Write("| ");


                    

                    if (s % 16 == 0)
                        Console.WriteLine("");
                }
            }

            Console.WriteLine("");
               
        }

        private static string convertToHex(int index)
        {
            Stack<string> stiva = new Stack<string>();
            string result = "";

            while (index > 0)
            {
                int _ = index / 16;
                int rest = index % 16;

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

                index /= 16;
            }

            while (stiva.Count > 0)
            {
                result = result + stiva.Pop();
            }
          
            return result.PadLeft(7, '0').PadRight(8, '0');
        }
    }
}
