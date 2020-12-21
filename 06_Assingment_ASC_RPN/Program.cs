using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace _06_Assingment_RPN
{
    class Program
    {
        static void Main(string[] args)
        {
            string expressie = "";
            int parChecker = 0;
            bool valid = true;
            do
            {
                Console.Write("Introduceti expressia: ");
                expressie = Console.ReadLine();

                valid = true;
                parChecker = 0;
                for (int i = 0; i < expressie.Length; i++)
                {
                    if (expressie[i] == '(')
                        parChecker++;
                    else if (expressie[i] == ')')
                        parChecker--;

                    if (parChecker < 0)
                    {
                        valid = false;
                        break;
                    }
                }
                if (parChecker > 0)
                {
                    valid = false;
                }


                if (valid)
                {
                    char[] acceptableChar = new char[] { '+', '-', '*', '/', '%', '(', ')', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', ' '};
                    for (int i = 0; i < expressie.Length; i++)
                    {
                        for (int j = 0; j < acceptableChar.Length; j++)
                        {
                            if (expressie[i] == acceptableChar[j])
                            {
                                valid = true;
                                break;
                            }
                            else
                            {
                                valid = false;
                            }
                        }
                        if (!valid)
                        {
                            Console.WriteLine("Input gresit!");
                            break;
                        }
                    }
                }
            } while (!valid);   


            Queue<string> output = new Queue<string>();
            Stack<string> op = new Stack<string>();
            Regex numbers = new Regex(@"(^-\d+|\d+)");
            MatchCollection matches = numbers.Matches(expressie);
            List<int> precedence = new List<int>();
            precedence.Add(0);

            int matchesIndex = 0;
            int listIndex = -1;
            for (int i = 0; i < expressie.Length; i++)
            {
                if (expressie[i] == '+' || expressie[i] == '-' && i != 0)
                {
                    precedence.Add(1);
                    listIndex++;
                }
                else if (expressie[i] == '/' || expressie[i] == '*' || expressie[i] == '%')
                {
                    precedence.Add(2);
                    listIndex++;
                }


                if (expressie[i] == ' ') { }
                else if ((expressie[i] == '+' || expressie[i] == '-' || expressie[i] == '*' || expressie[i] == '/' || expressie[i] == '%') && i != 0)
                {
                    while (op.Count != 0 && precedence[listIndex + 1] <= precedence[listIndex] && op.Peek() != "(")
                    {
                        output.Enqueue(op.Pop());
                        precedence.RemoveAt(listIndex);
                        listIndex--;
                    }       

                    op.Push(expressie[i].ToString());
                }
                else if (expressie[i] == '(')
                {
                    op.Push(expressie[i].ToString());
                }
                else if (expressie[i] == ')')
                {
                    while (op.Peek() != "(")
                    {
                        output.Enqueue(op.Pop());
                    }
                    if (op.Peek() == "(")
                    {
                        op.Pop();
                    }
                }
                else
                {
                    output.Enqueue(matches[matchesIndex].ToString());
                    i = i + matches[matchesIndex].Length - 1;
                    matchesIndex++;
                }
            }

            while (op.Count != 0)
            {
                output.Enqueue(op.Pop());
            }

            List<string> arithmetic = new List<string>();

            while (output.Count != 0)
            {
                arithmetic.Add(output.Dequeue());
            }
            Console.WriteLine();

            Console.WriteLine("Postfix notation: ");
            for (int i = 0; i < arithmetic.Count; i++)
            {
                Console.Write($"{arithmetic[i]} ");
            }
            Console.WriteLine();

            int result = 0;
            while (arithmetic.Count > 1)
            {
                for (int i = 0; i < arithmetic.Count; i++)
                {
                    if (arithmetic[i] == "+" || arithmetic[i] == "-" || arithmetic[i] == "/" || arithmetic[i] == "*" || arithmetic[i] == "%")
                    {
                        int operand1 = int.Parse(arithmetic[i - 2]);
                        int operand2 = int.Parse(arithmetic[i - 1]);
                        switch (arithmetic[i])
                        {
                            case "+":
                                result = operand1 + operand2;
                                break;
                            case "-":
                                result = operand1 - operand2;
                                break;
                            case "*":
                                result = operand1 * operand2;
                                break;
                            case "/":
                                result = operand1 / operand2;
                                break;
                            case "%":
                                result = operand1 % operand2;
                                break;
                            default:
                                break;
                        }

                        arithmetic[i] = result.ToString();
                        arithmetic.RemoveAt(i - 1);
                        arithmetic.RemoveAt(i - 2);
                        break;
                    }
                }
            }
            Console.WriteLine($"Result: {result}");
        }
    }
}