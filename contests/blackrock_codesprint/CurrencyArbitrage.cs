using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
/// <summary>
/// https://www.hackerrank.com/contests/blackrock-codesprint/challenges/currency-arbitrage
/// </summary>
class Solution1
{
    static void Main(String[] args)
    {
        const double SUM = 100000;

        TextReader tIn = Console.In;
        TextWriter tOut = Console.Out;

//        tIn = new StringReader(@"2
//1.1837 1.3829 0.6102
//1.1234 1.2134 1.2311");

        int N = int.Parse(tIn.ReadLine());

        for (int i = 0; i < N; i++)
        {
            double[] quotes = tIn.ReadLine().Split().Select(p => double.Parse(p)).ToArray();
            double X = SUM;

            for (int j = 0; j < 3; j++) {
                X /= quotes[j];
                X = double.Parse(X.ToString("0.0000"));
            }

            X = (long)(X - SUM);

            tOut.WriteLine(X > 0 ? X : 0);
        }

        //Console.ReadLine();
    }
}
