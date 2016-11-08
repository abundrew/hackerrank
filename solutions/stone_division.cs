using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
/// <summary>
/// https://www.hackerrank.com/contests/w25/challenges/stone-division
/// </summary>
class Solution4
{
    static long N = 0;
    static long[] S = null;
    static Dictionary<long, bool> mem = null;
    static bool Win(long n)
    {
        if (mem.ContainsKey(n)) return mem[n];
        bool win = false;
        foreach (long s in S)
        {
            if (n % s == 0) win = !Win(n / s);
            if (win) break;
        }
        mem[n] = win;
        return mem[n];
    }
    static void Main(String[] args)
    {
        TextReader tIn = Console.In;
        TextWriter tOut = Console.Out;

        tIn = new StringReader(@"15 3
5 2 3
");

        long[] nm = tIn.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(p => long.Parse(p)).ToArray();
        N = nm[0];
        S = tIn.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(p => long.Parse(p)).Where(p => N % p == 0).ToArray();

        if (S.Length == 0)
        {
            tOut.WriteLine("Second");
            return;
        }
        if (S.Where(p => p % 2 == 0).Any())
        {
            tOut.WriteLine("First");
            return;
        }
        mem = new Dictionary<long, bool>();
        tOut.WriteLine(Win(N) ? "First" : "Second");

        tIn.ReadLine();
    }
}
