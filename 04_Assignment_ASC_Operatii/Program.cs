using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04_Assignment_ASC_Operatii
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] one = new int[] { 3, 9, 1, 4, 3, 9, 0, 5, 7, 2, 8, 6, 7 };
            int[] two = new int[] { 9, 8, 9, 1, 5, 0, 4, 3, 6, 2, 4, 7, 8 };
            string line;

            do
            {
                Console.Write("Care operatie doriti sa executati? (+ / - / * / '/' / ^ / sqrt): ");
                line = Console.ReadLine();
            } while (line != "+" && line != "-" && line != "*" && line != "/" && line != "^" && line != "sqrt");

            switch (line)
            {
                case "+":
                    addition(one, two);
                    break;
                default:
                    break;
            }
        }

        private static void addition(int[] one, int[] two)
        {
            int[] newArr = new int[] { };
            int[] sum = new int[] { };

            if (one.Length > two.Length)
            {
                newArr = new int[one.Length];
                sum = new int[one.Length];

                int diff = one.Length - two.Length;
                int twoLen = two.Length - 1;

                newArrayLoadUp(one.Length, diff, twoLen, newArr, two);
                sumCalc(newArr.Length, newArr, one, sum);
            }
            else if (two.Length > one.Length)
            {
                newArr = new int[two.Length];
                sum = new int[two.Length];

                int diff = two.Length - one.Length;
                int oneLen = one.Length - 1;

                newArrayLoadUp(two.Length, diff, oneLen, newArr, one);
                sumCalc(newArr.Length, newArr, two, sum);
            }
            else
            {
                sum = new int[one.Length];

                sumCalc(one.Length, two, one, sum);
            }

            for (int j = 0; j < sum.Length; j++)
            {
                Console.Write($"{sum[j]}");
            } 
        }

        private static int[] sumCalc(int newArrLength, int[] newArr, int[] one, int[] sum)
        {
            int next = 0;
            for (int i = newArrLength - 1; i >= 0; i--)
            {
                sum[i] = one[i] + newArr[i] + next;

                next = 0;
                if (sum[i] > 9 && i > 0)
                {
                    next = sum[i] / 10;
                    sum[i] = sum[i] % 10;
                }
            }
            return sum;
        }

        // TAKES THE NUMBER WITH LESS DIGITS, AND ADDS ZERO TO THE START UNTIL IT IS EQUAL WITH THE NUMBER WITH MORE DIGITS THIS PROGRAM MAKES ME WANNA DIE
        private static int[] newArrayLoadUp(int oneLength, int diff, int twoLen, int[] newArr, int[] two)
        {
            for (int i = oneLength - 1; i >= diff; i--, twoLen--)
            {
                newArr[i] = two[twoLen];
            }

            return newArr;
        }
    }
}
