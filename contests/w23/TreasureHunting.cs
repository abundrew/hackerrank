using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
/// <summary>
/// https://www.hackerrank.com/contests/w23/challenges/treasure-hunting
/// </summary>
class Solution3
{
    static void Main(String[] args)
    {
        TextReader tIn = Console.In;
        TextWriter tOut = Console.Out;

        tIn = new StringReader(@"1000 1000
-1000 -1000
");

        long[] xy = tIn.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(p => long.Parse(p)).ToArray();
        double X = xy[0];
        double Y = xy[1];
        long[] ab = tIn.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(p => long.Parse(p)).ToArray();
        double A = ab[0];
        double B = ab[1];

        double K = (A * X + B * Y) / (A * A + B * B);
        double N = (A * Y - B * X) / (A * A + B * B);

        tOut.WriteLine(K);
        tOut.WriteLine(N);

        tIn.ReadLine();
    }
}
