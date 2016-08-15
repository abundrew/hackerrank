using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
/// <summary>
/// https://www.hackerrank.com/contests/w22/challenges/polygon-making
/// </summary>
class Solution2
{
    static void Main(String[] args)
    {
        TextReader tIn = Console.In;
        TextWriter tOut = Console.Out;

        tIn = new StringReader(@"3
1 2 3
");

        int N = int.Parse(tIn.ReadLine());
        int[] A = tIn.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(p => int.Parse(p)).ToArray();

        int K = 0;
        if (N == 1) K = 2;
        if (N == 2)
        {
            if (A[0] == A[1]) K = 2; else K = 1;
        }
        if (N > 2)
        {
            int maxA = A.Max();
            int othersA = A.Sum() - maxA;
            if (maxA >= othersA) K = 1;
        }

        tOut.WriteLine(K);

        tIn.ReadLine();
    }
}
