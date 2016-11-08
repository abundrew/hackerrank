using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
/// <summary>
/// https://www.hackerrank.com/contests/moodysuniversityhackathon/challenges/asterisk-expressions
/// </summary>
class Solution3
{
    public static long Power(long a, long b, int modulus)
    {
        long res = 1;
        while (b > 0)
        {
            if ((b & 1) == 1) res = (res * a) % modulus;
            a = (a * a) % modulus;
            b >>= 1;
        }
        return res;
    }
    static void Main(String[] args)
    {
        TextReader tIn = Console.In;
        TextWriter tOut = Console.Out;

        tIn = new StringReader(@"1
3*2**3**2*5
");
        int R = 1000 * 1000 * 1000 + 7;
        int T = int.Parse(tIn.ReadLine());
        for (int t = 0; t < T; t++)
        {
            long E = 1;
            string s = tIn.ReadLine();
            bool ok = true;
            if (s.IndexOf("***") != -1 || s[0] == '*' || s[s.Length - 1] == '*') ok = false;
            if (ok)
            {
                foreach (string m in s.Replace("**", "#").Split(new char[] { '*' }))
                {
                    string[] p = m.Split(new char[] { '#' });
                    long x = long.Parse(p[0]) % R;
                    for (int i = 1; i < p.Length; i++) x = Power(x, long.Parse(p[i]), R);
                    E *= x;
                    E %= R;
                }
            }
            tOut.WriteLine(ok ? E.ToString() : "Syntax Error");
        }

        tIn.ReadLine();
    }
}
