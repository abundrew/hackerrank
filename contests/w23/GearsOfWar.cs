using System;
using System.IO;
/// <summary>
/// https://www.hackerrank.com/contests/w23/challenges/gears-of-war
/// </summary>
class Solution1
{
    static void Main(String[] args)
    {
        TextReader tIn = Console.In;
        TextWriter tOut = Console.Out;

        tIn = new StringReader(@"2
3
4");

        int Q = int.Parse(tIn.ReadLine());
        for (int q = 0; q < Q; q++)
        {
            int N = int.Parse(tIn.ReadLine());
            tOut.WriteLine(N == 1 || N % 2 == 0 ? "Yes" : "No");
        }

        tIn.ReadLine();
    }
}
