using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
/// <summary>
/// Choosing White Balls
/// https://www.hackerrank.com/contests/w28/challenges/choosing-white-balls
/// </summary>
class Solution5
{
    // ---------------------------------------------------------------------
    static uint[] B32 = new uint[] {
        0x1, 0x2, 0x4, 0x8,
        0x10, 0x20, 0x40, 0x80,
        0x100, 0x200, 0x400, 0x800,
        0x1000, 0x2000, 0x4000, 0x8000,
        0x10000, 0x20000, 0x40000, 0x80000,
        0x100000, 0x200000, 0x400000, 0x800000,
        0x1000000, 0x2000000, 0x4000000, 0x8000000,
        0x10000000, 0x20000000, 0x40000000, 0x80000000,
    };
    static uint[] F32 = new uint[] {
        0x0,
        0x1, 0x3, 0x7, 0xF,
        0x1F, 0x3F, 0x7F, 0xFF,
        0x1FF, 0x3FF, 0x7FF, 0xFFF,
        0x1FFF, 0x3FFF, 0x7FFF, 0xFFFF,
        0x1FFFF, 0x3FFFF, 0x7FFFF, 0xFFFFF,
        0x1FFFFF, 0x3FFFFF, 0x7FFFFF, 0xFFFFFF,
        0x1FFFFFF, 0x3FFFFFF, 0x7FFFFFF, 0xFFFFFFF,
        0x1FFFFFFF, 0x3FFFFFFF, 0x7FFFFFFF, 0xFFFFFFFF
    };
    static uint RemoveBit(uint bits, int bit)
    {
        return ((bits >> 1) & (~F32[bit])) | (bits & F32[bit]);
    }
    static uint ReverseBits(uint bits, int n)
    {
        uint revs = 0;
        for (int i = 0; i < n; i++)
            if ((bits & B32[i]) == B32[i]) revs |= B32[n - 1 - i];
        return revs;
    }
    static Dictionary<long, double> dp = null;
    static double Estimate(int n, int k, uint balls)
    {
        if (k == 0 || balls == 0) return 0;

        long key = ((long)balls * 31 + n) * 31 + k;
        if (dp.ContainsKey(key)) return dp[key];

        long rkey = ((long)ReverseBits(balls, n) * 31 + n) * 31 + k;
        if (dp.ContainsKey(rkey)) return dp[rkey];

        double e = 0;
        for (int i = 0; i < n / 2; i++)
        {
            e += Math.Max(
                ((balls & B32[i]) == B32[i] ? 1 : 0) + Estimate(n - 1, k - 1, RemoveBit(balls, i)),
                ((balls & B32[n - 1 - i]) == B32[n - 1 - i] ? 1 : 0) + Estimate(n - 1, k - 1, RemoveBit(balls, n - 1 - i))
            ) * 2;
        }
        if (n % 2 != 0)
        {
            int i = n / 2;
            e += ((balls & B32[i]) == B32[i] ? 1 : 0) + Estimate(n - 1, k - 1, RemoveBit(balls, i));
        }
        e /= n;
        dp[key] = e;

        return e;
    }
    // ---------------------------------------------------------------------
    static void Main(String[] args)
    {
        TextReader tIn = Console.In;
        TextWriter tOut = Console.Out;

        tIn = new StringReader(@"30 22
WBWBWBWBWBWBWBWBWBWBWBWBWBWBWB");

        int[] nk = tIn.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(p => int.Parse(p)).ToArray();
        int N = nk[0];
        int K = nk[1];
        uint balls = Convert.ToUInt32(new string(tIn.ReadLine().Select(p => p == 'W' ? '1' : '0').ToArray()), 2);

        dp = new Dictionary<long, double>();

        tOut.WriteLine(Estimate(N, K, balls));

        tIn.ReadLine();
    }
}
