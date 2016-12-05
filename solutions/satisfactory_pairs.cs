using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
/// <summary>
/// https://www.hackerrank.com/contests/w26/challenges/pairs-again
/// </summary>
class Solution5
{
    static int gcd(int a, int b)
    {
        while (b != 0) b = a % (a = b);
        return a;
    }
    static void Main(String[] args)
    {
        TextReader tIn = Console.In;
        TextWriter tOut = Console.Out;

        tIn = new StringReader(@"300000");

        int N = int.Parse(tIn.ReadLine());

        long K = 0;

        int[] div1 = new int[N];
        int[] div2 = new int[N];
        for (int n1 = 1; n1 < N; n1++)
        {
            int ndiv1 = 0;
            int nr1 = (int)Math.Sqrt(n1);
            div1[ndiv1++] = 1;
            if (n1 > 1) div1[ndiv1++] = n1;
            for (int d = 2; d <= nr1; d++)
                if (n1 % d == 0)
                {
                    div1[ndiv1++] = d;
                    int n1d = n1 / d;
                    if (d != n1d) div1[ndiv1++] = n1d;
                }
            int n2 = N - n1;
            int ndiv2 = 0;
            int nr2 = (int)Math.Sqrt(n2);
            div2[ndiv2++] = 1;
            if (n2 > 1) div2[ndiv2++] = n2;
            for (int d = 2; d <= nr2; d++)
                if (n2 % d == 0)
                {
                    div2[ndiv2++] = d;
                    int n2d = n2 / d;
                    if (d != n2d) div2[ndiv2++] = n2d;
                }

            for (int i1 = 0; i1 < ndiv1; i1++)
                for (int i2 = 0; i2 < ndiv2; i2++)
                {
                    int a = div1[i1];
                    int b = div2[i2];

                    if (a < b && n1 / a - b / gcd(a, b) <= 0) K++;
                }
        }

        tOut.WriteLine(K);

        tIn.ReadLine();
    }
}
