using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
/// <summary>
/// https://www.hackerrank.com/contests/world-codesprint-6/challenges/bon-appetit
/// </summary>
class Solution1
{
    static void Main(String[] args)
    {
        TextReader tIn = Console.In;
        TextWriter tOut = Console.Out;

        tIn = new StringReader(@"4 1
3 10 2 9
7
");

        int[] nk = tIn.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(p => int.Parse(p)).ToArray();
        int N = nk[0];
        int K = nk[1];
        int[] C = tIn.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(p => int.Parse(p)).ToArray();
        int B = int.Parse(tIn.ReadLine());

        int charge = (C.Sum() - C[K]) / 2;

        tOut.WriteLine(B > charge ? (B - charge).ToString() : "Bon Appetit");

        tIn.ReadLine();
    }
}
