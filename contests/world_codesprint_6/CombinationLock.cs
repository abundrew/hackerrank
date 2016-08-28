using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
/// <summary>
/// https://www.hackerrank.com/contests/world-codesprint-6/challenges/combination-lock
/// </summary>
class Solution2
{
    static void Main(String[] args)
    {
        TextReader tIn = Console.In;
        TextWriter tOut = Console.Out;

        tIn = new StringReader(@"1 2 9 5 7
1 3 2 0 7
");

        int[] A = tIn.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(p => int.Parse(p)).ToArray();
        int[] B = tIn.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(p => int.Parse(p)).ToArray();

        int K = 0;
        for (int i = 0; i < A.Length; i++)
        {
            int min = Math.Min(A[i], B[i]);
            int max = Math.Max(A[i], B[i]);
            K += Math.Min(max - min, min + 10 - max);
        }
        tOut.WriteLine(K);

        tIn.ReadLine();
    }
}
