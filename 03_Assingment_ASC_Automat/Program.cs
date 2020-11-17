using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03_Assingment_ASC_Automat
{
    class Program
    {
        static void Main(string[] args)
        {
            int price = 20;
            bool valid;

            do
            {
                valid = true;
                Console.Write("Cost of product: ");
                try
                {
                    price = int.Parse(Console.ReadLine());
                }
                catch (Exception)
                {
                    valid = false;
                    Console.WriteLine("Invalid input");
                }
            } while (!valid || price > 100);

            int coin = insertCoin();

        // METHOD 1
            while (!dispense(price, coin))
            {
                coin = coin + insertCoin();
            }

            rest(price, coin);

/*
        // METHOD 2
            // starting coin quarter
            if (coin == 25)
            {
                dispense(price, coin);
                rest(price, coin);
            }

            // starting coin dime
            if (coin == 10)
            {
                coin = coin + insertCoin();

                if (coin == 15)
                {
                    coin = coin + insertCoin();

                    dispense(price, coin);
                    rest(price, coin);
                }
                else
                {
                    dispense(price, coin);
                    rest(price, coin);
                }
            }

            // starting coin nickel
            if (coin == 5)
            {
                coin = coin + insertCoin();

                if (coin == 10)
                {
                    coin = coin + insertCoin();
                    if (coin == 15)
                    {
                        coin = coin + insertCoin();

                        dispense(price, coin);
                        rest(price, coin);
                    }
                    else
                    {
                        dispense(price, coin);
                        rest(price, coin);
                    }
                }
                else if (coin == 15)
                {
                    coin = coin + insertCoin();

                    dispense(price, coin);
                    rest(price, coin);
                }
                else
                {
                    dispense(price, coin);
                    rest(price, coin);
                }
            } */
        }

        private static void rest(int price, int coin)
        {
            int rest = coin - price;
            int quarterCounter = 0;
            int dimeCounter = 0;
            int nickelCounter = 0;

            while (rest >= 25)
            {
                rest = rest - 25;
                quarterCounter++;
            }

            while (rest >= 10)
            {
                rest = rest - 10;
                dimeCounter++;
            }

            while (rest >= 5)
            {
                rest = rest - 5;
                nickelCounter++;
            }

            Console.WriteLine($"Restul e {coin-price} - {quarterCounter} quarter, {dimeCounter} dime, {nickelCounter} nickel");
        }

        private static bool dispense(int price,int coin)
        {
            if (coin >= price)
                return true;
            else
                return false;
        }

        public static int insertCoin()
        {
            bool valid;
            int n = 0;
            do
            {
                valid = true;
                Console.Write("How many cents would you like to introduce (5/10/25): ");
                string line = Console.ReadLine();
                try
                {
                    n = int.Parse(line);
                }
                catch (Exception)
                {
                    valid = false;
                }
            } while (!valid || (n != 5 && n != 10 && n != 25));
            
            return n;
        }
    }
}
