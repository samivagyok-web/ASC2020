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
            int coin = insertCoin();

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
                    else if (coin == 20)
                    {
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
            }

            

        }

        private static void rest(int price, int coin)
        {
            Console.WriteLine($"Restul e {coin-price}");
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
