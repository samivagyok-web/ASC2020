using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace _08_Assignment_ASC_Linker
{
    class Program
    {
        static void Main(string[] args)
        {
            TextReader file = new StreamReader("input-2");
            string wholeInput = file.ReadToEnd();
            wholeInput = removeSpacesBetweenWords(wholeInput);
            wholeInput = removeSpacesBeginningOfLine(wholeInput);
            wholeInput = removeBlankLines(wholeInput);
            wholeInput = removeNewLines(wholeInput);

            int numOfModules = int.Parse(wholeInput.Split(' ')[0]);
            List<Tuple<string, int>> definitions = new List<Tuple<string, int>>();
            List<int> adress = new List<int>();

            int pnt = 1;
            int relativeAddress = 0;
            for (int i = 0; i < numOfModules; i++)
            {
                int numOfDefinitions = int.Parse(wholeInput.Split(' ')[pnt]);

                for (int j = pnt + 1; j < pnt + numOfDefinitions*2 + 1; j = j + 2)
                {
                    string name = wholeInput.Split(' ')[j];
                    int num = int.Parse(wholeInput.Split(' ')[j + 1]) + relativeAddress;
                    Tuple<string, int> pair = new Tuple<string, int>(name, num);
                    definitions.Add(pair);
                }
                pnt = pnt + 2 * numOfDefinitions + 1;

                int numOfUtil = int.Parse(wholeInput.Split(' ')[pnt]);

                pnt = pnt + 2 * numOfUtil + 1;

                int numOfAddresses = int.Parse(wholeInput.Split(' ')[pnt]);

                for (int j = pnt + 1; j < pnt + 1 + numOfAddresses; j++)
                {
                    adress.Add(int.Parse(wholeInput.Split(' ')[j]));
                }

                relativeAddress = relativeAddress + int.Parse(wholeInput.Split(' ')[pnt]);
                pnt = pnt + int.Parse(wholeInput.Split(' ')[pnt]) + 1;
                
            }

            Console.WriteLine("Symbol Table: ");
            for (int i = 0; i < definitions.Count; i++)
            {
                Console.Write($"{definitions[i].Item1} = {definitions[i].Item2}");
                Console.WriteLine();
            }

            for (int i = 0; i < adress.Count; i++)
            {
                Console.WriteLine($"{i}: {adress[i]}");
            }

        }

        private static string removeNewLines(string wholeInput)
        {
            string text = "";
            for (int i = 0; i < wholeInput.Length; i++)
            {
                if (wholeInput[i] != 10)
                {
                    text = text + wholeInput[i];
                }
                else
                {
                    text = text + " ";
                }
            }

            return text;
        }

        private static string removeBlankLines(string text)
        {
            string input = "";

            for (int i = 0; i < text.Length; i++)
            {
                input = input + text[i];

                while (i < text.Length - 1 && text[i + 1] == 10 && text[i] == 10)
                    i++;
            }
            return input;
        }

        private static string removeSpacesBeginningOfLine(string wholeInput)
        {
            string input = "";

            if (wholeInput[0] != 32)
                input = input + wholeInput[0];

            for (int i = 1; i < wholeInput.Length - 1; i++)
            {
                input = input + wholeInput[i];

                while (wholeInput[i] == 10 && wholeInput[i+1] == 32)
                {
                    i++;
                }
            }
            return input;
        }

        private static string removeSpacesBetweenWords(string text)
        {
            string input = "";

            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] != 32)
                {
                    input = input + text[i];
                }
                else
                {
                    input = input + text[i];

                    while (text[i + 1] == 32)
                        i++;
                }
            }
            return input;
        }
    }
}