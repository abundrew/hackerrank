using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
/// <summary>
/// https://www.hackerrank.com/contests/w24/challenges/xor-matrix
/// </summary>
class Solution4
{
    static void Main(String[] args)
    {
        TextReader tIn = Console.In;
        TextWriter tOut = Console.Out;

        tIn = new StringReader(@"4 2
6 7 1 3");

        long[] nm = tIn.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(p => long.Parse(p)).ToArray();
        int N = (int)nm[0];
        long M = nm[1] - 1;
        int[] A = tIn.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(p => int.Parse(p)).ToArray();
        int[] B = new int[N];

        long k = 1;
        while (k * 2 <= M) k *= 2;
        while (M > 0)
        {
            for (int i = 0; i < N; i++) B[i] = A[i] ^ A[(k + i) % N];
            Buffer.BlockCopy(B, 0, A, 0, N * 4);
            M -= k;
            while (k > M) k /= 2;
        }

        tOut.WriteLine(string.Join(" ", A.Select(p => p.ToString()).ToArray()));

        tIn.ReadLine();
    }
}
