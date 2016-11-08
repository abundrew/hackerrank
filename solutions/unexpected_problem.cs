using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
/// <summary>
/// https://www.hackerrank.com/contests/w23/challenges/commuting-strings
/// </summary>
class Solution4
{
    static void Main(String[] args)
    {
        TextReader tIn = Console.In;
        TextWriter tOut = Console.Out;

//        tIn = new StringReader(@"abc
//6
//");
        tIn = new StringReader(File.ReadAllText(@"c:\temp\2.txt"));

        const int R = 1000 * 1000 * 1000 + 7;

        string S = tIn.ReadLine();
        long M = long.Parse(tIn.ReadLine());

        int len = S.Length;
        List<int> dv = new List<int>();
        for (int i = 2; i <= len / 2; i++)
            if (len % i == 0) dv.Add(i);
        dv.Add(len);

        int[] A = new int[26];
        foreach (char c in S) A[c - 'a']++;

        for (int i = 0; i < 26; i++)
            if (A[i] > 0)
                for (int j = dv.Count - 1; j >= 0; j--)
                    if (A[i] % dv[j] != 0)
                        dv.RemoveAt(j);

        int W = len;
        for (int i = dv.Count - 1; i >= 0; i--)
        {
            W = len / dv[i];
            for (int j = 0; j < len / W - 1; j++)
            {
                if (S.Substring(j * W, W) != S.Substring((j + 1) * W, W))
                {
                    W = len;
                    break;
                }
            }
            if (W < len) break;
        }

        tOut.WriteLine((M / W) % R);

        tIn.ReadLine();
    }
}
