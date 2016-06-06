using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
/// <summary>
/// https://www.hackerrank.com/contests/zalando-codesprint/challenges/make-as-many-customers-happy-as-possible-
/// </summary>
class Solution6
{
    static void Main(String[] args)
    {
        TextReader tIn = Console.In;
        TextWriter tOut = Console.Out;

//        tIn = new StringReader(@"2 2 10
//5
//A,B
//A,C
//A,C
//C,B
//B,C");

        int[] Nabc = tIn.ReadLine().Split().Select(p => int.Parse(p)).ToArray();
        int Na = Nabc[0];
        int Nb = Nabc[1];
        int Nc = Nabc[2];

        Dictionary<string, int> orders = new Dictionary<string, int>() { { "A", 0 }, { "B", 0 }, { "C", 0 }, { "AB", 0 }, { "AC", 0 }, { "BC", 0 }, { "ABC", 0 } };

        int M = int.Parse(tIn.ReadLine());
        for (int i = 0; i < M; i++)
            orders[string.Join("", tIn.ReadLine().Split(',').OrderBy(p => p).ToArray())]++;

        int K = 0;

        int k = Math.Min(Na, orders["A"]);
        Na -= k;
        K += k;

        k = Math.Min(Nb, orders["B"]);
        Nb -= k;
        K += k;

        k = Math.Min(Nc, orders["C"]);
        Nc -= k;
        K += k;

        while (true)
        {
            int ab = Math.Min(orders["AB"], Math.Min(Na, Nb));
            int bc = Math.Min(orders["BC"], Math.Min(Nb, Nc));
            int ac = Math.Min(orders["AC"], Math.Min(Na, Nc));

            if (ab + bc + ac == 0) break;

            if ((ab > bc || ab == bc && Na >= Nc) && (ab > ac || ab == ac && Nb >= Nc))
            {
                orders["AB"]--;
                Na--;
                Nb--;
                K++;
                continue;
            }

            if ((bc > ab || bc == ab && Nc >= Na) && (bc > ac || bc == ac && Nb >= Na))
            {
                orders["BC"]--;
                Nb--;
                Nc--;
                K++;
                continue;
            }

            if ((ac > ab || ac == ab && Nc >= Nb) && (ac > bc || ac == bc && Na >= Nb))
            {
                orders["AC"]--;
                Na--;
                Nc--;
                K++;
                continue;
            }
        }

        int Kabc = Math.Min(Math.Min(Math.Min(Na, Nb), Nc), orders["ABC"]);

        tOut.WriteLine(K + Kabc);

//        Console.ReadLine();
    }
}
