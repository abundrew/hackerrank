using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
/// <summary>
/// https://www.hackerrank.com/contests/w21/challenges/kangaroo
/// </summary>
class Solution1
{
    static void Main(String[] args)
    {
        TextReader tIn = Console.In;
        TextWriter tOut = Console.Out;

        tIn = new StringReader(@"3 1 3 2");

        int[] xv = tIn.ReadLine().Split().Select(p => int.Parse(p)).ToArray();
        int x1 = xv[0];
        int v1 = xv[1];
        int x2 = xv[2];
        int v2 = xv[3];

        tOut.WriteLine((x1 == x2 && v1 == v2) || ((v1 != v2) && Math.Abs(x2 - x1) % Math.Abs(v1 - v2) == 0 && (x2 - x1) / (v1 - v2) > 0) ? "YES" : "NO");

        tIn.ReadLine();
    }
}
