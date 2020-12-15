using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;

namespace _05_Assingment_ASC_Asamblor
{
    class Program
    {
        static void Main(string[] args)
        {
            TextReader assembly = new StreamReader(@"..\..\data.in");
            string line = "";

            string bits = "00000000000000000000000000000000";

            int lines = numOfLines();
            for (int i = 0; i < lines; i++)
            {
                StringBuilder bit = new StringBuilder(bits);
                line = assembly.ReadLine();

                Regex firstTwo = new Regex(@"(ld|st|addcc|jmpl)");
                MatchCollection matches = firstTwo.Matches(line);

                if (firstTwo.IsMatch(line))
                {
                    bit = firstTwoBitsAndOp3(bit, line, firstTwo, matches);
                }

                Regex numbers = new Regex(@"(%r(\d+)|\[[a-z]\])");
                MatchCollection numMatches = numbers.Matches(line);

                registers(bit, line, numbers, numMatches, lines);
                Console.WriteLine(bit);
            }
        }

        private static StringBuilder registers(StringBuilder bit, string line, Regex pattern, MatchCollection matches, int lineNum)
        {
            string[] asd = new string[matches.Count];

            int i = 0;
            foreach (Match match in matches)
            {
                string regNumber = match.ToString();
                asd[i] = regNumber;
                i++;
            }
            Regex registerNum = new Regex(@"\d{1,2}");

            for (int j = asd.Length - 1; j >= 0; j--)
            {
                if (registerNum.IsMatch(asd[j]))
                {
                    MatchCollection number = registerNum.Matches(asd[j]);
                    foreach (Match match2 in number)
                    {
                        int regNum = int.Parse(match2.ToString());
                        StringBuilder reg = conversionToBinary(regNum);

                        if (regNum == 15)
                        {
                            for (int z = 0; z < reg.Length; z++)
                            {
                                bit[z + 13] = reg[z];
                            }

                            StringBuilder four = conversionToBinary(4);
                            for (int z = 0; z < four.Length; z++)
                            {
                                bit[z + 28] = four[z];
                            }
                        }
                        else
                        {
                            if (j == asd.Length - 1)
                            {
                                for (i = 0; i < reg.Length; i++)
                                {
                                    bit[i + 3] = reg[i];
                                }
                            }

                            if (j == asd.Length - 2)
                            {
                                for (int t = 0; t < reg.Length; t++)
                                {
                                    bit[t + 13] = reg[t];
                                }
                            }

                            if (j == asd.Length - 3)
                            {
                                for (int t = 0; t < reg.Length; t++)
                                {
                                    bit[t + 28] = reg[t];
                                }
                            }
                        }
                    }
                }
            }
            return bit;
        }

        private static StringBuilder conversionToBinary(int n)
        {
            int numInBin = 0;
            int putere = 1;

            while (n != 0)
            {
                int ultimCif = n % 2;
                n = n / 2;
                numInBin = numInBin + ultimCif * putere;
                putere = putere * 10;
            }

            string num = numInBin.ToString().PadLeft(4, '0');
            StringBuilder numar = new StringBuilder(num);

            return numar;
        }

        private static StringBuilder settingBitsToOne(int fromIndex, int toIndex, StringBuilder bit)
        {
            for (int i = fromIndex; i <= toIndex; i++)
            {
                bit[i] = '1';
            }
            return bit;
        }

        static private StringBuilder firstTwoBitsAndOp3(StringBuilder bit, string line, Regex pattern, MatchCollection matches)
        {
            foreach (Match match in matches)
            {
                string instruction = match.ToString();

                if (instruction == "ld" || instruction == "st")
                {
                    settingBitsToOne(0, 1, bit);

                    if (instruction == "st")
                    {
                        bit[10] = '1';
                    }
                }
                else if (instruction == "addcc" || instruction == "jmpl")
                {
                    bit[0] = '1';
                    if (instruction == "addcc")
                    {
                        bit[8] = '1';
                    }
                    else
                    {
                        settingBitsToOne(7, 9, bit);
                    }
                }
            }
            return bit;
        }

        static private void memorie(int n)
        {
            int memo = -4;
            TextReader code = new StreamReader(@"..\..\data.in");
            Regex pattern = new Regex(@"\s+\.");

            string buffer = "";
            for (int i = 0; i < n; i++)
            {
                buffer = code.ReadLine();
                if (!pattern.IsMatch(buffer))
                {
                    memo = memo + 4;
                }
            }
            Console.WriteLine(memo);
        }

        static private int numOfLines()
        {
            TextReader code = new StreamReader(@"..\..\data.in");
            int count = 0;
            while (code.ReadLine() != null)
            {
                count++;
            }
            return count;
        }
    }
}