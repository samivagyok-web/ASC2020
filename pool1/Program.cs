using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pool1
{
    class Program
    {
        static void Main(string[] args)
        {

            //   gradUnu();

            //   gradDoi();

            //   seDivide();

            //  anBisect();

            // afisareCifra();

            // triunghiChecker();

            // swap();

            // noswap();

            // nrOfDivisors();

            // primChecker();

            // ordInversaCifre();

            // divInInterval();

            // bisectInterval();

            // palindrom();

            // treiCresc();

            cinciCresc();
        }

        public static double getNumber()
        {
            string line;
            bool valid;
            double numar = 0;
            do
            {
                valid = true;
                line = Console.ReadLine();
                try
                {
                    numar = double.Parse(line);
                }
                catch (Exception)
                {
                    valid = false;
                    Console.WriteLine("Please don't do 'funny' things");
                }
            } while (!valid);

            return numar;
        }

        public static void delimiter()
        {
            Console.WriteLine("_________________________________________________________________");
        }

        public static void gradUnu()
        {
            Console.Write("Introduceti a: ");
            double a = getNumber();
            Console.Write("Introduceti b: ");
            double b = getNumber();
            double x = -b / a;
            Console.WriteLine($"{a}*x + {b} = 0 ==> x = {x}");
            delimiter();
        }

        public static void gradDoi()
        {
            Console.Write("Introduceti a: ");
            double a = getNumber();
            Console.Write("Introduceti b: ");
            double b = getNumber();
            Console.Write("Introduceti c: ");
            double c = getNumber();

            double delta = Math.Pow(b, 2) - 4 * a * c;
            if (delta >= 0)
            {
                double x1 = (-b + Math.Sqrt(delta)) / 2 * a;
                double x2 = (-b - Math.Sqrt(delta)) / 2 * a;
                Console.WriteLine($"The roots of {a}x^2 + {b}x + c = 0 are: x1 = {x1} and x2 = {x2}");
            }
            else
            {
                Console.WriteLine("it's a complex number :(");
            }
            delimiter();
        }

        public static void seDivide()
        {
            Console.Write("Introduceti primul numar: ");
            double n = getNumber();
            Console.Write("Introduceti al doilea numar: ");
            double k = getNumber();

            if (n % k == 0)
            {
                Console.WriteLine($"{n} se divide cu {k}");
            }
            else
            {
                Console.WriteLine($"{n} nu se divide cu {k} :(");
            }
            delimiter();
        }

        public static void anBisect()
        {
            Console.Write("Introduceti anul: ");
            int an = (int)getNumber();

            if (an % 4 == 0)
            {
                Console.WriteLine($"{an} este bisect");
            }
            else
            {
                Console.WriteLine($"{an} nu este bisect");
            }

            delimiter();
        }

        public static void afisareCifra()
        {
            Console.Write("Introduceti numarul: ");
            double numar = (int)getNumber();
            Console.Write("A catelea cifra vreti sa vedeti din dreapta: ");
            double cifra = (int)getNumber();
            double lungime = Math.Floor(Math.Log10(numar)) + 1;

            if (lungime >= cifra)
            {
                for (int i = 1; i < cifra; i++)
                {
                    numar = (int)numar / 10;
                }

                Console.WriteLine($"A {cifra}. cifra este {numar % 10}");
            }
            else
            {
                Console.WriteLine("I feel a great disturbance in the force");
            }
            delimiter();
        }

        public static void triunghiChecker()
        {
            Console.Write("Latura a: ");
            double a = getNumber();
            Console.Write("Latura b: ");
            double b = getNumber();
            Console.Write("Latura c: ");
            double c = getNumber();

            if (a + b > c && a + c > b && b + c > a)
            {
                Console.WriteLine("Se poate forma triunghi");
            }
            else 
            {
                Console.WriteLine("Nu prea se poate...");
            }
        }

        public static void swap()
        {
            Console.Write("a: ");
            double a = getNumber();
            Console.Write("b: ");
            double b = getNumber();

            Console.WriteLine($"Aici a = {a}, b = {b}");

            double aux = a;
            a = b;
            b = aux;

            Console.WriteLine($"Aici a = {a}, b = {b}");
            delimiter();
        }

        public static void noswap()
        {
            Console.Write("a: ");
            double a = getNumber();
            Console.Write("b: ");
            double b = getNumber();

            Console.WriteLine($"Aici a = {a}, b = {b}");
            // a = 5 b = 7
            a = a + b; // a = 5 + 7 = 12
            b = a - b; // b = 12 - 7 = 5
            a = a - b; // a = 12 - 5 = 7
            Console.WriteLine($"Aici a = {a}, b = {b}");
            delimiter();
        }

        public static void nrOfDivisors()
        {
            Console.Write("Cate divizori are urmatorul numar intreg: ");
            double numar = (int)getNumber();

            for (int i = 0; i < numar/2 + 1; i++)
            {
                if (numar % i == 0)
                {
                    Console.Write(i + " ");
                }
                if (i == numar/2)
                {
                    Console.WriteLine(numar);
                }
            }
            Console.WriteLine();
            delimiter();
        }

        public static void primChecker()
        {
            Console.Write("Numarul la care doriti sa faceti primcheck: ");
            double numar = (int)getNumber();
            double upperBound = (int)Math.Sqrt(numar);
            int s = 0;

            for (int i = 2; i < upperBound+1; i++)
            {
                if (numar % i == 0)
                {
                    s++;
                }
            }

            if (s == 0)
                Console.WriteLine("Numarul este prim");
            else
                Console.WriteLine("Numarul nu este prim");


            delimiter();
        }

        public static void ordInversaCifre()
        {
            Console.Write("Introduceti numarul: ");
            double numar = (int)getNumber();
            double lungime = Math.Floor(Math.Log10(numar)) + 1;

            int[] cifre = new int[(int)lungime];

            for (int i = 0; i < (int)lungime; i++)
            {
                int whole = (int)numar % 10;
                cifre[i] = whole;
                numar /= 10;
            }

            Array.Sort(cifre);
            Array.Reverse(cifre);

            for (int i = 0; i < (int)lungime; i++)
            {
                Console.Write(cifre[i]);
            }
            Console.WriteLine();
            delimiter();
        }

        public static void divInInterval()
        {
            Console.Write("Introduceti numarul: ");
            double numar = (int)getNumber();
            Console.Write("Introduceti lower bound of interval: ");
            double a = (int)getNumber();
            Console.Write("Introduceti upper bound of interval: ");
            double b = (int)getNumber();

            for (int i = (int)a; i <= b; i++)
            {
                if (i % numar == 0)
                {
                    Console.Write(i + " ");
                }
            }

            delimiter();
        }

        public static void bisectInterval()
        {
            Console.Write("Introduceti lower bound: ");
            double a = (int)getNumber();
            Console.Write("Introduceti upper bound: ");
            double b = (int)getNumber();

            for (int i = (int)a; i <= b; i++)
            {
                if (i % 4 == 0)
                {
                    Console.Write(i + " ");
                }
            }
            delimiter();
        }

        public static void palindrom()
        {
            // 12321 = 1 * 10000 + 2 * 1000 + 3 * 100 + 2 * 10 + 1 * 1 
            // lungime = 5
            Console.Write("Introduceti un numar: ");
            double numar = (int)getNumber();
            double copyofNum = numar;
            double lungime = Math.Floor(Math.Log10(numar)) + 1;
            int n = (int)lungime;
            int sum = 0;

            for (int i = 0; i < n; i++)
            {
                int whole = (int)numar % 10;
                sum = (int)(sum + (whole * Math.Pow(10, n - 1 - i)));
                numar /= 10;
            }
            if (copyofNum == sum)
            {
                Console.WriteLine("Numarul este palindrom");
            }
            else
            {
                Console.WriteLine("Numarul nu este palindrom :(");
            }
        }

        public static void treiCresc()
        {
            // presupun ca nici aici n-am voie sa folosesc array

            Console.Write("Introduceti primul numar: ");
            double a = getNumber();
            Console.Write("Introduceti al doilea numar: ");
            double b = getNumber();
            Console.Write("Introduceti al treilea (cred ca asa se scrie) numar: ");
            double c = getNumber();

            if (a > b && a > c)
            {
                if (b > c)
                    Console.WriteLine($"{c} < {b} < {a}");
                else
                    Console.WriteLine($"{b} < {c} < {a}");
            }
            else if (b > a && b > c)
            {
                if (a > c)
                    Console.WriteLine($"{c} < {a} < {b}");
                else
                    Console.WriteLine($"{a} < {c} < {b}");
            }
            else
            {
                if (a > b)
                    Console.WriteLine($"{b} < {a} < {c}");
                else
                    Console.WriteLine($"{a} < {b} < {c}");
            }
        }

        public static void cinciCresc()
        {

        }
    }
}
