using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
/// <summary>
/// https://www.hackerrank.com/contests/world-codesprint-6/challenges/functional-palindromes
/// </summary>
class Solution7
{
    const int A = 100001;
    const int M = 1000000007;
    static int F(string s)
    {
        long f = 0;
        foreach (char c in s)
        {
            f = f * A + (int)c;
            f %= M;
        }
        return (int)f;
    }

    static List<string> Palindroms(string s, long[] kk)
    {
        int radius = 0;
        int N = s.Length;
        int[,] pTable = new int[2, N + 1];

        s = "@" + s + "#";

        for(int j = 0; j <= 1; j++)
        {
            radius = 0;
            pTable[j, 0] = 0;
            int i = 1;
            while(i <= N)
            {
                while(s[i - radius - 1] == s[i + j + radius]) radius++;
                pTable[j, i] = radius;
                int k = 1;
                while((pTable[j, i - k] != radius - k) && (k < radius))
                {
                    pTable[j, i + k] = Math.Min(pTable[j, i - k], radius - k);
                    k++;
                }
                radius = Math.Max(radius - k, 0);
                i += k;
            }
        }

        s = s.Substring(1, N);

        Dictionary<string, long> pDic = new Dictionary<string, long>();
        for (int i = 0; i < N; i++)
        {
            string a = s.Substring(i, 1);
            if (!pDic.ContainsKey(a)) pDic[a] = 0;
            pDic[a]++;
        }
        for (int i = 1; i <= N; i++)
        {
            for(int j = 0; j <= 1; j++)
                for(int rp = pTable[j, i]; rp > 0; rp--)
                {
                    string a = s.Substring(i - rp - 1, 2 * rp + j);
                    if (!pDic.ContainsKey(a)) pDic[a] = 0;
                    pDic[a]++;
                }
        }
        string[] keys = pDic.Keys.OrderBy(p => p).ToArray();
        long[] qtys = new long[keys.Length + 1];
        for (int i = 0; i < keys.Length; i++)
            qtys[i + 1] = qtys[i] + pDic[keys[i]];

        List<string> result = new List<string>();
        foreach (long k in kk)
        {
            int index = Array.BinarySearch(qtys, k + 1);
            if (index < 0) index = ~index;

            result.Add(index > keys.Length ? "" : keys[index - 1]);
        }
        return result;
    }

    static void Main(String[] args)
    {
        TextReader tIn = Console.In;
        TextWriter tOut = Console.Out;

        tIn = new StringReader(@"5 7
abcba
1
2
3
4
6
7
8
");

        int[] nq = tIn.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(p => int.Parse(p)).ToArray();
        int N = nq[0];
        int Q = nq[1];
        string S = tIn.ReadLine().Trim();

        long[] kk = new long[Q];
        for (int q = 0; q < Q; q++)
        {
            kk[q] = long.Parse(tIn.ReadLine()) - 1;
        }
        List<string> palindroms = Palindroms(S, kk);
        for (int q = 0; q < Q; q++)
        {
            tOut.WriteLine(palindroms[q] == "" ? -1 : F(palindroms[q]));
        }

        tIn.ReadLine();
    }
}
