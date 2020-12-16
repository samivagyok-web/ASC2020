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
            string line = "";                                    // stringul in care va fi citit liniile din fileul assembly

            string bits = "00000000000000000000000000000000";  

            int lines = numOfLines();                           // numarul liniilor din file
            Regex machCode = new Regex(@"\s+\.");               // regex pt. gasirea pseudo-operatiilor
            for (int i = 0; i < lines; i++)
            {
                StringBuilder bit = new StringBuilder(bits);    // Stringbuilder pt. manipularea stringurilor
                line = assembly.ReadLine();

                // gasim instructiunea care o sa seteze op3 si primele doua bituri din secventa
                Regex firstTwo = new Regex(@"(ld|st|addcc|jmpl)");  
                MatchCollection matches = firstTwo.Matches(line);
                if (firstTwo.IsMatch(line))
                {
                    bit = firstTwoBitsAndOp3(bit, line, firstTwo, matches);
                }


                // determinam daca intr-o linie exista variabila si registru, sau DOAR registru
                Regex reg = new Regex(@"(?<=%r)\d{1,2}");   // ex. %r15, %r2
                Regex var = new Regex(@"(?<=\[).+?(?=\])"); // ex. [x], [y]
                MatchCollection regMatches = reg.Matches(line);
                MatchCollection varMatches = var.Matches(line);

                // daca in linie exista si variabila si registru ex. [x], %r1 
                if (reg.IsMatch(line) && var.IsMatch(line))
                {
                    varAndReg(bit, line, reg, regMatches, var, varMatches);
                }

                // daca in linie exista DOAR registru ex. %r1, %r2, %r3
                if (reg.IsMatch(line) && !(var.IsMatch(line)))
                {
                    onlyRegister(bit, line, reg, regMatches);
                }

                // daca in linie este o variabila declarate ex. x: 15
                // regexul cauta valoarea variabilei, si converteste intro secventa binara de 32 de cifre
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

                // nu afisam codul de masina daca este un preudo-cod ex. .begin, .end
                if (!machCode.IsMatch(line))
                {
                    Console.WriteLine(bit);
                }
            }
        }

        private static StringBuilder varAndReg(StringBuilder bit, string line, Regex reg, MatchCollection regMatches, Regex var, MatchCollection varMatches)
        {
            // cautam ce loc ocupa variabila in memorie, schimbam in binar
            string patHelper = ":";
            foreach (Match match in varMatches)
            {
                string pattern = "^" + match.ToString() + patHelper;
                Regex varDeclar = new Regex(pattern);
                int locInMemorie = memorie(varDeclar);
                bit = regSource2(bit, conversionToBinary(locInMemorie));
            }

            // dupa ce am terminat cu variabila folosim functia pt. registrul
            onlyRegister(bit, line, reg, regMatches);

            return bit;
        }

        private static StringBuilder onlyRegister(StringBuilder bit, string line, Regex reg, MatchCollection regMatches)
        {
            // fiindca conteaza sirul registrelor in linie (de ex. %r1, %2, %r3 != %r2, %r3, %r1) punem intr-o array
            int[] regNumbers = new int[regMatches.Count];

            int i = 0;
            foreach(Match match in regMatches)
            {
                  regNumbers[i] = int.Parse(match.ToString());
                  i++;
            }

            // niste legi predefinite (cred) ce valoare intra in rd, rs1, rs2
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
            // functia care returneaza locul ocupat in memorie a unui variabila
            int memo = -4;
            TextReader code = new StreamReader(@"..\..\data.in");
            Regex pattern = new Regex(@"\s+\.");    // nu numaram pseudo-operatiile
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