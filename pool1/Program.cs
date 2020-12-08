using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace pool1
{
    class Program
    {
        static void Main(string[] args)
        {

            // [1-14]

              // gradUnu();

            //   gradDoi();

            //   seDivide();

           //   anBisect();

            // afisareCifra();

            // triunghiChecker();

            // swap();

            // noswap();

           //  nrOfDivisors();

            // primChecker();

            // ordInversaCifre();

            // divInInterval();

            // bisectInterval();

             // palindrom();

            // treiCresc();

            ///   cinciCresc();

            // euclidAlg();

            //   descomp();

            // douaCifre();

            /// zecimalPer();

            // hiLo();

            // anBisectInterval();
        }

        private static void anBisectInterval()
        {
            int a = (int)getNumber();
            int b = (int)getNumber();
            int counter = 0;
            int newA = a;

            Stopwatch sw = new Stopwatch();
            sw.Start();

            while (a % 4 != 0)
            {
                a++;
            }

            while (a <= b)
            {
                if (((a % 4 == 0) && (a % 100 != 0)) || (a % 400 == 0))
                {
                    counter++;
                }
                a = a + 4;
            }
            sw.Stop();
            Console.WriteLine($"elapsed ms {sw.ElapsedMilliseconds}");
            Console.WriteLine($"{counter}");

            counter = 0;
            Stopwatch stopw = new Stopwatch();
            stopw.Start();

            for (int i = newA; i <= b; i++)
            {
                if (((i % 4 == 0) && (i % 100 != 0)) || (i % 400 == 0))
                {
                    counter++;
                }
            }
            stopw.Stop();
            Console.WriteLine($"elapsed ms {stopw.ElapsedMilliseconds}");

            Console.WriteLine($"{counter}");
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

            if (((an % 4 == 0) && (an % 100 != 0)) || (an % 400 == 0))
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

            for (int i = 0; i < Math.Sqrt(numar); i++)
            {
                if (numar % i == 0)
                {
                    Console.Write($"{i} ");
                    if (numar/i != i)
                    {
                        Console.Write($"{numar/i} ");
                    }
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
            bool prim = true;

            for (int i = 2; i <= upperBound; i++)
            {
                if (numar % i == 0)
                {
                    prim = false;
                    break;
                }
            }

            if (prim)
                Console.WriteLine("Numarul este prim");
            else
                Console.WriteLine("Numarul nu este prim");


            delimiter();
        }

        public static void ordInversaCifre()
        {
            int n = (int)getNumber();
            int newNum = 0;

            while (n > 0)
            {
                newNum = newNum * 10 + n % 10;
                n /= 10;
            }
            Console.WriteLine($"{newNum}");
        }

        public static void divInInterval()
        {
            Console.Write("Introduceti numarul: ");
            int numar = (int)getNumber();
            Console.Write("Introduceti lower bound of interval: ");
            int a = (int)getNumber();
            Console.Write("Introduceti upper bound of interval: ");
            int b = (int)getNumber();

            for (int i = a; i <= b; i++)
            {
                if (i % numar == 0)
                {
                    Console.Write(i + " ");
                }
            }

            delimiter();
        }

        public static void palindrom()
        {
            int n = (int)getNumber();
            int nCopy = n;
            int newNum = 0;
            
            while (n > 0)
            {
                newNum = newNum * 10 + n % 10;
                n = n / 10;
            }


            if (newNum == nCopy)
                Console.WriteLine("Este palindrom");
            else
                Console.WriteLine("Nu este palindrom");
        }

        public static void treiCresc()
        {
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

        public static void euclidAlg()
        {
            Console.Write("Introduceti primul numar: ");
            double numar1 = getNumber();
            Console.Write("Introduceti al doilea numar: ");
            double numar2 = getNumber();
            double aux;
            double produs = numar1 * numar2;
            
            while (numar2 > 0)
            {
                aux = numar2;
                numar2 = numar1 % numar2;
                numar1 = aux;
            }
            Console.WriteLine($"Cel mai mare divizor comun este {numar1}");

            Console.WriteLine($"Cel mai mic multiplu comun este {(produs/numar1)}");

            delimiter();
        }

        public static void cinciCresc()
        {
            Console.Write("a: ");
            double a = getNumber();
            Console.Write("b: ");
            double b = getNumber();
            Console.Write("c: ");
            double c = getNumber();
            Console.Write("d: ");
            double d = getNumber();
            Console.Write("e: ");
            double e = getNumber();

            
        }

        public static void descomp()
        {
            Console.Write("Introduceti un numar: ");
            int n = (int)getNumber();

            int divizor = 2;
            int putere;
            while (n != 1)
            {
                putere = 0;
                while (n % divizor == 0)
                {
                    putere++;
                    n = n / divizor;
                }

                if (putere != 0)
                {
                    Console.WriteLine($"{divizor}^{putere}");

                    if (n != 1)
                    {
                        Console.WriteLine("* ");
                    }
                }
                divizor++;
            }

        }

        public static void douaCifre()
        {
            Console.WriteLine("Introduceti un numar: ");
            double numar = (int)getNumber();
            double lungime = (int)(Math.Floor(Math.Log10(numar)));
       //     int[] numere = new int[lungime]; 
            int first = (int)numar % 10;
            
            Console.WriteLine($"{first}");

            for (int i = 0; i < lungime+1; i++)
            {
                
            }
        } 

     /*   public static void zecimalPer()
        {
            Console.Write("a: ");
            double a = getNumber();
            Console.Write("b: ");
            double b = getNumber();


        } */

        public static void hiLo()
        {
            Random rnd = new Random();
            int x = rnd.Next(1024);
            
            double n;
            int s = 0;
            do
            {
                Console.Write("Introduceti un numar intre [1-1024]: ");
                n = getNumber();
                s++;
                if (n < x)
                    Console.WriteLine("Prea mic.");
                else if (n > x)
                    Console.WriteLine("Prea mare");
                else
                    Console.WriteLine($"Ai ghicit numarul din {s} incercari!");

            } while (n != x);
        }

    }
}
