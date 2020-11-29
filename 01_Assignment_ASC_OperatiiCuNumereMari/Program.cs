using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04_Assingment_ASC_OperatiiNumereMari
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] one = hugeNumberInput();

            string line;
            do
            {
                Console.Write("Care operatie doriti sa executati? (+ / - / * / '/' / ^ / sqrt): ");
                line = Console.ReadLine();
            } while (line != "+" && line != "-" && line != "*" && line != "/" && line != "^" && line != "sqrt");

            int[] two = hugeNumberInput();
            Console.WriteLine("asdssasd");
            switch (line)
            {
                case "+":
                    view(addition(one, two));
                    break;
                case "-":
                    view(subtraction(one, two));
                    break;
                case "*":
                    view(multiplication(one, two));
                    break;
                case "/":
                    break;
                case "^":
                    view(putere(one, two));
                    break;
                case "sqrt":
                    break;
                default:
                    break;
            }
        }

        private static int[] putere(int[] one, int[] two)
        {
            int putere = 0;
            int[] fullSum = new int[] { 1 };
            for (int i = 0; i < two.Length; i++)
            {
                putere = (int)(putere + two[i] * Math.Pow(10, two.Length - 1 - i));
            }

            for (int i = 0; i < putere; i++)
            {
                fullSum = multiplication(fullSum, one);
            }
            return fullSum;
        }

        private static int[] multiplication(int[] one, int[] two)
        {
            int[] fullProd = new int[] { };

            int[] prod = new int[one.Length + two.Length];
            int zeroBefore = 0;

            for (int i = two.Length - 1; i >= 0; i--)
            {
                int next = 0;
                int prodLen = prod.Length - 1;
                for (int j = one.Length - 1; j >= 0; j--)
                {
                    prod[prodLen - zeroBefore] = two[i] * one[j] + next;

                    next = 0;
                    if (prod[prodLen - zeroBefore] > 9)
                    {
                        next = prod[prodLen - zeroBefore] / 10;
                        prod[prodLen - zeroBefore] %= 10;
                    }
                    prodLen--;
                }
                prod[prodLen - zeroBefore] = next;
                fullProd = addition(prod, fullProd);
                Array.Clear(prod, 0, prod.Length);
                zeroBefore++;
            }

            return fullProd;
        }

        private static int[] addition(int[] one, int[] two)
        {
            int diff;
            if (biggerNum(one, two))
            {
                diff = one.Length - two.Length;
                two = newArrWithZeros(two, diff);
            }
            else
            {
                diff = two.Length - one.Length;
                one = newArrWithZeros(one, diff);
            }
            int[] n = addCalc(one, two);
            return n;
        }

        private static int[] subtraction(int[] one, int[] two)
        {
            int diff;
            if (biggerNum(one, two))
            {
                diff = one.Length - two.Length;
                two = newArrWithZeros(two, diff);

                return subCalc(one, two);
            }
            else
            {
                diff = two.Length - one.Length;
                one = newArrWithZeros(one, diff);

                Console.Write("-");
                return subCalc(two, one);
            }
        }

        private static int[] subCalc(int[] one, int[] two)
        {
            int[] sum = new int[one.Length];
            for (int i = one.Length - 1; i >= 0; i--)
            {
                sum[i] = one[i] - two[i];

                if (sum[i] < 0)
                {
                    sum[i] = 10 + one[i] - two[i];
                    one[i - 1]--;
                }
            }
            return sum;
        }

        private static int[] addCalc(int[] one, int[] two)
        {
            int[] sum = new int[one.Length + 1];
            int next = 0;

            for (int i = sum.Length - 1; i > 0; i--)
            {
                sum[i] = one[i - 1] + two[i - 1] + next;

                next = 0;
                if (sum[i] > 9)
                {
                    next = sum[i] / 10;
                    sum[i] = sum[i] % 10;
                }
            }
            sum[0] = next;

            return sum;
        }

        private static int[] newArrWithZeros(int[] two, int diff)
        {
            int[] newArr = new int[two.Length + diff];
            for (int i = newArr.Length - 1; i >= diff; i--)
            {
                newArr[i] = two[i - diff];
            }
            return newArr;
        }

        private static bool biggerNum(int[] one, int[] two)
        {
            if (one.Length > two.Length)
            {
                return true;
            }
            else if (two.Length > one.Length)
            {
                return false;
            }
            else
            {
                for (int i = 0; i < one.Length; i++)
                {
                    if (one[i] > two[i])
                        return true;

                    if (two[i] > one[i])
                        return false;
                }
            }
            return true;
        }

        private static int[] hugeNumberInput()
        {
            int[] arr = new int[] { };
            string num;
            bool valid;
            int problem;
            do
            {
                problem = 0;
                valid = true;
                Console.Write("Introduceti un numar cat de mare doriti: ");
                num = Console.ReadLine();
                arr = new int[num.Length];

                for (int i = 0; i < num.Length; i++)
                {
                    try
                    {
                        arr[i] = (int)Char.GetNumericValue(num[i]);
                        if (arr[i] == -1)
                            problem++;
                    }
                    catch (Exception)
                    {
                        valid = false;
                        Console.WriteLine("Invalid input");
                    }
                }
            } while (!valid || problem > 0 || arr.Length == 0);
            return arr;
        }

        private static void view(int[] arr)
        {
            for (int i = 0; i < arr.Length; i++)
            {
                Console.Write($"{arr[i]}");
            }
            Console.WriteLine();
        }
    }
}
