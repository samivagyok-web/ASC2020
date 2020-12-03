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

            int[] two = new int[] { };

            if (line != "sqrt")
            {
                two = hugeNumberInput();
            }

            switch (line)
            {
                case "+":
                    view(removeZero(addition(one, two)));
                    break;
                case "-":
                    view(removeZero(subtraction(one, two)));
                    break;
                case "*":
                    view(removeZero(multiplication(one, two)));
                    break;
                case "/":
                    view(division(one, two));
                    break;
                case "^":
                    view(removeZero(putere(one, two)));
                    break;
                case "sqrt":
                    radacinaPatrate(one);
                    break;
                default:
                    break;
            }
        }

        private static void radacinaPatrate(int[] one)  // WORK IN PROGRESS
        {
            int[] result = new int[one.Length/2];
            int numar = firstPair(one);
            int loopLen = 0;

            for (int i = 0; i*i <= numar; i++)
            {
                result[0] = i;
            }

            int nextStep = result[0] * 2;

            numar = ((numar - result[0] * result[0]) * 100) + addingNextPair(one, lenOfNum(numar));


            int minusDinNumar = 0;
            int nextPair = 4;
            for (int i = 1; i < one.Length / 2; i++)
            {
                for (int j = 1; j < 10; j++)
                {
                    if (((nextStep * 10) + j) * j <= numar)
                    {
                        minusDinNumar = ((nextStep * 10) + j) * j;
                        result[i] = j;
                    }
                }


                numar = (numar - minusDinNumar) * 100 + addingNextPair(one, nextPair);

                nextStep = arrayToNum(result) * 2;
                nextPair = nextPair + 2;

                Console.WriteLine($"numar: {numar} nextStep: {nextStep}");
            }
            view(result);

            Console.Write(".");

            for (int i = 0; i < 2; i++)
            {
                
            }
        }

        private static int arrayToNum(int[] arr)
        {
            int sum = 0;
            arr = removeZeroesFromEnd(arr);

            for (int i = 0; i < arr.Length; i++)
            {
                sum = (int)(sum + arr[i] * Math.Pow(10, arr.Length - 1 - i));
            }
            return sum;
        }

        private static int[] removeZeroesFromEnd(int[] arr)
        {
            int i = arr.Length - 1;
            while (arr[i] == 0)
            {
                i--;
            }

            int[] newArr = new int[i+1];

            for (int j = 0; j <= i; j++)
            {
                newArr[j] = arr[j];
            }
            return newArr;
        }
    

        private static int lenOfNum(int numar)
        {
            return (int)Math.Floor(Math.Log10(numar)) + 1;
        }

        private static int firstPair(int[] arr)
        {
            if (arr.Length % 2 == 1)
            {
                return arr[0];
            }
            else
            {
                return addingNextPair(arr, 0);
            }
        }

        private static int addingNextPair(int[] arr, int fromIndex)
        {
            int sum = 0;
            int j = 1;
            for (int i = fromIndex; i <= fromIndex + 1 ; i++)
            {
                sum = (int)(sum + arr[i] * Math.Pow(10, j));
                j--;
            }
            return sum;
        }

        // simple division using multiple subtraction
        private static int[] division(int[] one, int[] two)
        {
            int whole = 0;
            while (biggerNum(one, two))
            {
                one = removeZero(subtraction(one, two));
                whole++;
            }
            Console.WriteLine($"Partea intreaga: {whole}");
            Console.Write("Rest: ");
            return one;
        }

        // simple ^ calculation using multiple multiplication
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

        // multiplication as you'd do it on paper; shifting the digits after completing each phase of it, and filling with zeroes
        // and the beginning and at the end
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

        // takes as input the array which has less digits, and the number of digits differentiating the two arrays;
        // loads up with zeroes at the beginning so operations are more easily performed
        private static int[] newArrWithZeros(int[] two, int diff)
        {
            int[] newArr = new int[two.Length + diff];
            for (int i = newArr.Length - 1; i >= diff; i--)
            {
                newArr[i] = two[i - diff];
            }
            return newArr;
        }

        // compares the two arrays and returns true if the first is bigger, false in case the second
        // quite useful at not commutative operations
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

        // a string is read-in (which will we be an eventual operand); this function takes a look at the validity of
        // the number and loads it into an array
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

        // after certain operations zeroes might appear in front (because of transport e.g.); this array takes the array as the 
        // input and returns the same array without the zeroes in front
        private static int[] removeZero(int[] arr)
        {
            int i = 0;

            while (i < arr.Length && arr[i] == 0)
            {
                i++;
            }

            int[] newArr = new int[arr.Length-i];

            for (int j = 0; j < newArr.Length; j++)
            {
                newArr[j] = arr[j + i];
            }

            return newArr;
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
