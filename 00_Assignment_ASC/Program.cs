using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;


namespace _00_Assignment_ASC
{
    class Program
    {
        static void Main(string[] args)
        {
            /* y = an; n = putere
             *|---------------------------------------------------------------|
             *|  y  |  0  |  1.5  |  3  |  4.5  |  6   |  7.5 |   9   |  10.5 |
             *|---------------------------------------------------------------|
             *|  n  |  1  |   2   |  4  |   8   |  16  |  32  |   64  |  128  |
             *|---------------------------------------------------------------|
             *
             * punctul de baza este prezentul (anul 0, putere 1), si cum putem vedea din exemplu,
             * peste 9-10.5 ani vom avea o putere de calcul de 100 de ori mai mare. trebuie sa precizam
             * o formula, cand o sa avem cu "n" mai multa putere decat astazi.
             * 
             * din "grafica" de mai sus putem observa ca "n" se mareste exponential, se dubleaza in 
             * fiecare 1.5 ani. fiindca periodicitatea este in 1.5 ani-in-ani, si in 1.5 ani se dubleaza
             * puterea, obtinem ca 2^(y/1.5) = n; unde n este puterea in viitor, y = an. avem nevoie de "y/1.5", 
             * pt. ca nu chiar de ani avem nevoie, ci perioade ex. 4.5/1.5 = 3 (3 perioade intre 
             * anii 0-1.5, 1.5-3, si 3-4.5). si de aici ca sa aducem "y" "jos", inmultim amandoua
             * parti cu log2, si sa lasam "y" singur, inca inmultim cu 1.5 --> y = 1.5*log(2)n.
             * 
             * sper ca ati inteles justificarea, chiar folosesc prima data in viata mea termeni de mate
             * pe limba romana. :)
             */

            Console.WriteLine("Legea lui Moore (Gordon Moore fondatorul Intel), spune că " +
                "puterea de calcul se dublează la fiecare 18 luni iar prețul rămâne același.");
            Console.Write("Enter the power multiplier compared to today: ");

            // un while loop, care da reprompt userului, in cazul inputului invalid
            while (true)
            {
                Console.Write("Enter the power multiplier compared to today: ");

                try
                {
                    string line1 = Console.ReadLine();
                    int power = int.Parse(line1);
                    double year = Math.Log(power, 2) * 1.5;
                    double month = Math.Round(year * 12); // am rotunjit luna, ca sa avem un numar rotund, max error jumat de luna.
                    Console.WriteLine($"After {year} years (~{month} months) we will have {power} times more power than today.");
                    break;
                }
                catch (Exception)
                {
                    Console.WriteLine("Error: only natural number input. (and under 4.2 billion...)");
                }
            }
        }
    }
}
