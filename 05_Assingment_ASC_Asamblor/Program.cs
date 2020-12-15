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
            Regex machCode = new Regex(@"\s+\.");
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

                Regex reg = new Regex(@"%r\d+");
                Regex var = new Regex(@"(?<=\[).+?(?=\])");
                MatchCollection regMatches = reg.Matches(line);
                MatchCollection varMatches = var.Matches(line);

                if (reg.IsMatch(line) && var.IsMatch(line))
                {
                    varAndReg(bit, line, reg, regMatches, var, varMatches);
                }

                if (reg.IsMatch(line) && !(var.IsMatch(line)))
                {
                    onlyRegister(bit, line, reg, regMatches);
                }

                Regex varValue = new Regex(@"(?<=\s+)\d{1,2}$");
                MatchCollection m = varValue.Matches(line);

                foreach(Match match in m)
                {
                    int value = int.Parse(match.ToString());
                    StringBuilder val = new StringBuilder(conversionToBinary(value).ToString());
                    string bitVal = val.ToString();
                    bitVal = bitVal.PadLeft(32, '0');
                    bit = new StringBuilder(bitVal);
                }

                if (!machCode.IsMatch(line))
                {
                    Console.WriteLine(bit);
                }
            }
        }

        private static StringBuilder varAndReg(StringBuilder bit, string line, Regex reg, MatchCollection regMatches, Regex var, MatchCollection varMatches)
        {
            string patHelper = ":";
            foreach (Match match in varMatches)
            {
                string pattern = "^" + match.ToString() + patHelper;
                Regex varDeclar = new Regex(pattern);
                int locInMemorie = memorie(varDeclar);
                bit = regSource2(bit, conversionToBinary(locInMemorie));
            }

            onlyRegister(bit, line, reg, regMatches);

            return bit;
        }

        private static StringBuilder onlyRegister(StringBuilder bit, string line, Regex reg, MatchCollection regMatches)
        {
            int[] regNumbers = new int[regMatches.Count];
            Regex digits = new Regex(@"\d{1,2}");

            int i = 0;
            foreach(Match match in regMatches)
            {
                string register = match.ToString();
                MatchCollection matches = digits.Matches(register);
                
                foreach (Match match2 in matches)
                {
                    regNumbers[i] = int.Parse(match2.ToString());
                    i++;
                }
            }

            if (regNumbers.Length == 3)
            {
                bit = regDestination(bit, conversionToBinary(regNumbers[2]));
                bit = regSource1(bit, conversionToBinary(regNumbers[0]));
                bit = regSource2(bit, conversionToBinary(regNumbers[1]));
            }
            else if (regNumbers.Length == 2)
            {
                if (regNumbers[0] == 15)
                {
                    bit = regSource2(bit, conversionToBinary(4));
                    bit = regSource1(bit, conversionToBinary(15));
                    bit = regDestination(bit, conversionToBinary(regNumbers[1]));
                }
                else
                {
                    bit = regDestination(bit, conversionToBinary(regNumbers[1]));
                    bit = regSource1(bit, conversionToBinary(regNumbers[0]));
                }
            }
            else
            {
                bit = regDestination(bit, conversionToBinary(regNumbers[0]));
            }
            return bit;
        }

        private static StringBuilder regDestination(StringBuilder bit, StringBuilder rd)
        {
            for (int i = 0; i < rd.Length; i++)
            {
                bit[i + 2] = rd[i];
            }
            return bit;
        }

        private static StringBuilder regSource1(StringBuilder bit, StringBuilder rs1)
        {
            for (int i = 0; i < rs1.Length; i++)
            {
                bit[i + 13] = rs1[i];
            }
            return bit;
        }

        private static StringBuilder regSource2(StringBuilder bit, StringBuilder rs2)
        {
            for (int i = 0; i < rs2.Length; i++)
            {
                bit[i + 27] = rs2[i];
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

            string num = numInBin.ToString().PadLeft(5, '0');
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

        static private int memorie(Regex variable)
        {
            int memo = -4;
            TextReader code = new StreamReader(@"..\..\data.in");
            Regex pattern = new Regex(@"\s+\.");
            string buffer = " ";

            while (!variable.IsMatch(buffer))
            {
                buffer = code.ReadLine();
                if (!pattern.IsMatch(buffer))
                {
                    memo = memo + 4;
                }
            }
            return memo;
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