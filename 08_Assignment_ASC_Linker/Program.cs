using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace _08_Assignment_ASC_Linker
{
    class Errors
    {
        public static bool multipleDefinition (List<Tuple<string, int>> definitions, string name)
        {
            for (int i = 0; i < definitions.Count; i++)
            {
                if (definitions[i].Item1 == name)
                {
                    return true;
                }
            }
            return false;
        }

        public static bool outsideModuleDefinition (int num, int numOfAdresses)
        {
            if (num < numOfAdresses)
                return false;
            else
                return true;
        }

        internal static bool definedNotUtilized(string symbol, List<Tuple<string, int>> util)
        {

            for (int i = 0; i < util.Count; i++)
            {
                if (symbol == util[i].Item1)
                    return false;
            }
            return true;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            TextReader file = new StreamReader("input-4");
            string wholeInput = file.ReadToEnd();
            wholeInput = removeSpacesBetweenWords(wholeInput);
            wholeInput = removeSpacesBeginningOfLine(wholeInput);
            wholeInput = removeBlankLines(wholeInput);
            wholeInput = removeNewLines(wholeInput);

            int numOfModules = int.Parse(wholeInput.Split(' ')[0]);
            List<Tuple<string, int>> definitions = new List<Tuple<string, int>>();
            List<Tuple<string, int>> util = new List<Tuple<string, int>>();
            List<int> relativeAddresses = new List<int>();
            relativeAddresses.Add(0);
            List<int> words = new List<int>();

            int pnt = 1;
            int relativeAddress = 0;
            for (int i = 0; i < numOfModules; i++)
            {
                int numOfDefinitions = int.Parse(wholeInput.Split(' ')[pnt]);

                for (int j = pnt + 1; j < pnt + numOfDefinitions * 2 + 1; j = j + 2)
                {
                    string name = wholeInput.Split(' ')[j];
                    int num = int.Parse(wholeInput.Split(' ')[j + 1]) + relativeAddress;
                    Tuple<string, int> pair = new Tuple<string, int>(name, num);
                    if (!Errors.multipleDefinition(definitions, name))
                    {
                        definitions.Add(pair);
                        Console.WriteLine($"{name} multiply defined. First value used.");
                    }
                }
                pnt = pnt + 2 * numOfDefinitions + 1;

                int numOfUtil = int.Parse(wholeInput.Split(' ')[pnt]);

                for (int j = pnt + 1; j < pnt + numOfUtil*2 + 1; j = j + 2)
                {
                    string name = wholeInput.Split(' ')[j];
                    int num = int.Parse(wholeInput.Split(' ')[j + 1]) + relativeAddress;
                    Tuple<string, int> pair = new Tuple<string, int>(name, num);
                    util.Add(pair);
                }

                pnt = pnt + 2 * numOfUtil + 1;

                int numOfAddresses = int.Parse(wholeInput.Split(' ')[pnt]);

                for (int j = pnt + 1; j < pnt + 1 + numOfAddresses; j++)
                {
                    words.Add(int.Parse(wholeInput.Split(' ')[j]));
                }
                relativeAddress = relativeAddress + int.Parse(wholeInput.Split(' ')[pnt]);

                relativeAddresses.Add(relativeAddress);
                pnt = pnt + int.Parse(wholeInput.Split(' ')[pnt]) + 1;
            }

            int t = 0;
            int addToWord = 0;
            for (int i = 0; i < words.Count; i++)
            {
                if (i == relativeAddresses[t+1])
                {
                    t++;
                    addToWord = relativeAddresses[t];
                }

                if (words[i]%10 == 1 || words[i]%10 == 2)
                {
                    words[i] = words[i] / 10;
                }
                else if (words[i]%10 == 3)
                {
                    words[i] = words[i] / 10 + addToWord;
                }
                else if (words[i]%10 == 4 && middleThreeNums(words[i]) != 777 && isUtilized(util, i))
                {
                    int j = i;

                    while (middleThreeNums(words[j]) != 777)
                    {
                        int next = (words[j] / 10) % 10;
                        words[j] = (words[j] / 10000) * 1000;
                        words[j] = words[j] + addingSymbolNumber(util, i, definitions);
                        j = next + addToWord;
                    }

                    if (middleThreeNums(words[j]) == 777)
                    {
                        words[j] = (words[j] / 10000) * 1000;
                        words[j] = words[j] + addingSymbolNumber(util, i, definitions);
                    }
                }
                else if (words[i] % 10 == 4 && middleThreeNums(words[i]) == 777 && isUtilized(util, i))
                {
                    words[i] = (words[i] / 10000) * 1000;
                    words[i] = words[i] + addingSymbolNumber(util, i, definitions);
                }
            }
            Console.WriteLine();

            Console.WriteLine("Util table:");
            for (int i = 0; i < util.Count; i++)
                Console.WriteLine($"{util[i].Item1} - {util[i].Item2}");

            Console.WriteLine();

            Console.WriteLine("Definition table: ");
            for (int i = 0; i < definitions.Count; i++)
                Console.WriteLine($"{definitions[i].Item1} - {definitions[i].Item2}");

            Console.WriteLine("Word table: ");
            for (int i = 0; i < words.Count; i++)
            {
                Console.WriteLine($"{i}: {words[i]}");
            }
            Console.WriteLine();
            Console.WriteLine();
            for (int i = 0; i < words.Count; i++)
            {
                if (words[i] / 10000 > 0)
                {
                    Console.WriteLine($"{i}: {words[i]}");
                }
            }
            Console.WriteLine("Relative addresses");
            for (int i = 0; i < relativeAddresses.Count; i++)
            {
                Console.WriteLine($"{i}: {relativeAddresses[i]}");
            }

            for (int i = 0; i < definitions.Count; i++)
            {
                if (Errors.definedNotUtilized(definitions[i].Item1, util))
                {
                    int j = 0;
                    while (definitions[i].Item2 >= relativeAddresses[j])
                        j++;

                    Console.WriteLine($"Warning: {definitions[i].Item1} was defined in module {j-1} but never used.");
                }
            }
        }

        private static int addingSymbolNumber(List<Tuple<string, int>> util, int n, List<Tuple<string, int>> definitions)
        {
            string symbol = "";
            for (int i = 0; i < util.Count; i++)
            {
                if (util[i].Item2 == n)
                    symbol = util[i].Item1;
            }

            for (int i = 0; i < definitions.Count; i++)
            {
                if (definitions[i].Item1 == symbol)
                    return definitions[i].Item2;
            }
            return -1;
        }

        private static bool isUtilized(List<Tuple<string, int>> util, int n)
        {
            for (int i = 0; i < util.Count; i++)
            {
                if (util[i].Item2 == n)
                    return true;
            }
            return false;
        }

        private static int middleThreeNums (int n)
        {
            n = n / 10;
            int divizor = 10;

            while (n / divizor > 10)
                divizor = divizor * 10;

            return n % divizor;
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