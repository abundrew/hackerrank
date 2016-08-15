using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
/// <summary>
/// https://www.hackerrank.com/contests/w22/challenges/box-moving
/// </summary>
class Solution3
{
    static void Main(String[] args)
    {
        TextReader tIn = Console.In;
        TextWriter tOut = Console.Out;

        tIn = new StringReader(@"3
1 2 3
2 3 2
");

        int N = int.Parse(tIn.ReadLine());
        long[] X = tIn.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(p => long.Parse(p)).ToArray();
        long[] Y = tIn.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(p => long.Parse(p)).ToArray();

        long K = 0;
        long sumX = X.Sum();
        long sumY = Y.Sum();

        if (sumX == sumY)
        {
            Array.Sort(X);
            Array.Sort(Y);
            for (int i = 0; i < N; i++) K += Math.Abs(X[i] - Y[i]);
            K /= 2;
        }
        else K = -1;

        tOut.WriteLine(K);

        tIn.ReadLine();
    }
}
