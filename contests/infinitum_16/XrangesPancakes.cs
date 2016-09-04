using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
/// <summary>
/// https://www.hackerrank.com/contests/infinitum16/challenges/xrange-and-pizza
/// </summary>
class Solution1
{
    static void Main(String[] args)
    {
        TextReader tIn = Console.In;
        TextWriter tOut = Console.Out;

        tIn = new StringReader(@"5 5
1 1
1 3
2 2
2 2
1 4
");

        int[] nm = tIn.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(p => int.Parse(p)).ToArray();
        int N = nm[0];
        int M = nm[1];

        int P0 = 0;
        bool F = false;
        for (int i = 0; i < M; i++)
        {
            int[] tk = tIn.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(p => int.Parse(p)).ToArray();
            int T = tk[0];
            int K = tk[1];

            switch (T)
            {
                case 1:
                    P0 = (P0 + K) % N;
                    break;
                case 2:
                    P0 = (K + N - P0) % N;
                    F = !F;
                    break;
            }
        }

        tOut.WriteLine("{0} {1}", F ? "2" : "1", F ? P0 : (N - P0) % N);

        tIn.ReadLine();
    }
}
