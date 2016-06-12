using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
/// <summary>
/// https://www.hackerrank.com/contests/blackrock-codesprint/challenges/perfect-separating
/// </summary>
class Solution8
{
    //http://oeis.org/A001700

    static long[] A001700 = new long[] {
        1,
        3,
        10,
        35,
        126,
        462,
        1716,
        6435,
        24310,
        92378,
        352716,
        1352078,
        5200300,
        20058300,
        77558760,
        300540195,
        1166803110,
        4537567650,
        17672631900,
        68923264410,
        269128937220,
        1052049481860,
        4116715363800,
        16123801841550,
        63205303218876,
        247959266474052
    };

    static string A = null;
    static Dictionary<string, long> DP = new Dictionary<string, long>();

    static long PSNumber(string s1, string s2)
    {
        int minlen = Math.Min(s1.Length, s2.Length);
        int maxlen = Math.Max(s1.Length, s2.Length);
        if (maxlen > A.Length / 2) return 0;
        if (s1.Substring(0, minlen) != s2.Substring(0, minlen)) return 0;
        if (maxlen == minlen && maxlen == A.Length / 2) return s1 == s2 ? 1 : 0;

        string key = string.Format("{0} {1}", s1, s2);
        if (DP.ContainsKey(key)) return DP[key];

        int ix = s1.Length + s2.Length;

        DP[key] = PSNumber(s1 + A.Substring(ix, 1), s2) + PSNumber(s1, s2 + A.Substring(ix, 1));

        return DP[key];
    }

    static void Main(String[] args)
    {
        TextReader tIn = Console.In;
        TextWriter tOut = Console.Out;

        tIn = new StringReader(@"aaabab");

        A = tIn.ReadLine();
        int na = A.Count(p => p == 'a');
        int nb = A.Count(p => p == 'b');

        long K = 0;
        if (na % 2 == 0 && nb % 2 == 0)
        {
            if (na == 0 || nb == 0 || !A.Substring(0, na).Contains("b") || !A.Substring(0, nb).Contains("a"))
            {
                long ka = na > 0 ? A001700[na / 2 - 1] * 2 : 1;
                long kb = nb > 0 ? A001700[nb / 2 - 1] * 2 : 1;
                K = ka * kb;
            }
            else
            {
                K = PSNumber("", "");
            }
        }

        tOut.WriteLine(K);

        Console.ReadLine();
    }
}
