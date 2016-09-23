using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
/// <summary>
/// https://www.hackerrank.com/contests/stryker-codesprint/challenges/minimum-index-difference
/// </summary>
class Solution1
{
    static void Main(String[] args)
    {
        TextReader tIn = Console.In;
        TextWriter tOut = Console.Out;

        tIn = new StringReader(@"6
9 5 1 21 32 8
21 32 9 8 5 1
");

        int N = int.Parse(tIn.ReadLine());
        var A = tIn.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select((p, i) => new { i, P = int.Parse(p) }).OrderBy(p => p.P).ToArray();
        var B = tIn.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select((p, i) => new { i, P = int.Parse(p) }).OrderBy(p => p.P).ToArray();
        int min = Math.Abs(A[0].i - B[0].i);
        int ix = A[0].P;
        for (int i = 1; i < N; i++)
        {
            int dif = Math.Abs(A[i].i - B[i].i);
            if (dif < min || dif == min && A[i].P < ix)
            {
                min = dif;
                ix = A[i].P;
            }
        }
        tOut.WriteLine(ix);

        tIn.ReadLine();
    }
}
