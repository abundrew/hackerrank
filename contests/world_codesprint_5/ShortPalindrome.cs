using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
/// <summary>
/// https://www.hackerrank.com/contests/world-codesprint-5/challenges/short-palindrome
/// </summary>
class Solution3
{
    static long R = 1000000007;

    static void Main(String[] args)
    {
        TextReader tIn = Console.In;
        TextWriter tOut = Console.Out;

        tIn = new StringReader(@"ghhggh");

        byte valA = Convert.ToByte('a');

        int[] B = tIn.ReadLine().Select(p => Convert.ToByte(p) - valA).ToArray();
        int M = B.Max() + 1;

        long X = 0;
        int[] NN = new int[M];

        for (int a = 0; a < B.Length - 3; a++)
        {
            Array.Clear(NN, 0, M);
            long Y = 0;
            for (int d = a + 2; d < B.Length; d++)
            {
                Y += NN[B[d - 1]];
                Y %= R;
                NN[B[d - 1]]++;
                if (B[a] == B[d]) X += Y;
            }
            X %= R;
        }
        tOut.WriteLine(X);

        tIn.ReadLine();
    }
}
