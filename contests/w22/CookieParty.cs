using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
/// <summary>
/// https://www.hackerrank.com/contests/w22/challenges/cookie-party
/// </summary>
class Solution1
{
    static void Main(String[] args)
    {
        TextReader tIn = Console.In;
        TextWriter tOut = Console.Out;

        //tIn = new StringReader(@"3 2");

        int[] nm = tIn.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(p => int.Parse(p)).ToArray();
        int N = nm[0];
        int M = nm[1];

        tOut.WriteLine(M % N == 0 ? 0 : N - M % N);

        //tIn.ReadLine();
    }
}
