using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
/// <summary>
/// Sherlock's Array Merging Algorithm
/// https://www.hackerrank.com/contests/university-codesprint-2/challenges/sherlocks-array-merging-algorithm
/// </summary>
class Solution5
{
    // ---------------------------------------------------------------------
    static int[][] FiF(int nmax, int modulus)
    {
        int[][] fif = new int[2][];
        fif[0] = new int[nmax + 1];
        fif[0][0] = 1;
        for (int i = 1; i <= nmax; i++) fif[0][i] = (int)(((long)fif[0][i - 1] * i) % modulus);
        long a = fif[0][nmax];
        long b = modulus;
        long p = 1;
        long q = 0;
        while (b > 0)
        {
            long c = a / b;
            long d = a;
            a = b;
            b = d % b;
            d = p;
            p = q;
            q = d - c * q;
        }
        fif[1] = new int[nmax + 1];
        fif[1][nmax] = (int)(p < 0 ? p + modulus : p);
        for (int i = nmax - 1; i >= 0; i--) fif[1][i] = (int)(((long)fif[1][i + 1] * (i + 1)) % modulus);
        return fif;
    }
    static long Choose(int n, int r, int[][] fif, int modulus)
    {
        if (n < 0 || r < 0 || r > n) return 0;
        long factn = fif[0][n];
        long invFactr = fif[1][r];
        long invFactnr = fif[1][n - r];

        long c1 = ((long)fif[0][n] * fif[1][r]) % modulus;
        long c2 = (c1 * fif[1][n - r]) % modulus;

        return ((((long)fif[0][n] * fif[1][r]) % modulus) * fif[1][n - r]) % modulus;
    }
    // ---------------------------------------------------------------------

    const int R = 1000000007;
    static int N = 0;
    static int[] A = null;
    static int[] B = null;
    static int[][] FIF = null;
    static int[][] DP = null;
    static int[][] C = null;

    static int wdp0()
    {
        long ways = 0;
        int wxm = Math.Min(B[0], N);
        for (int iwx = 1; iwx <= wxm; iwx++)
            ways = (ways + wdp(iwx, iwx)) % R;
        return (int)ways;
    }

    static int wdp(int ix, int wx)
    {
        if (ix == N || wx == 1) return 1;
        if (DP[ix][wx] > -1) return DP[ix][wx];

        long ways = 0;
        int wxm = Math.Min(Math.Min(wx, B[ix]), N - ix);

        for (int iwx = 1; iwx <= wxm; iwx++)
        {
            long c = C[wx][iwx];
            if (c < 0)
            {
                c = (Choose(wx, iwx, FIF, R) * FIF[0][iwx]) % R;
                C[wx][iwx] = (int)c;
            }
            ways = (ways + (c * wdp(ix + iwx, iwx)) % R) % R;
        }

        DP[ix][wx] = (int)ways;
        return DP[ix][wx];
    }

    static void Main(String[] args)
    {
        TextReader tIn = Console.In;
        TextWriter tOut = Console.Out;

        tIn = new StringReader(@"3
1 2 3");

        N = int.Parse(tIn.ReadLine());
        A = tIn.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(p => int.Parse(p)).ToArray();

        B = new int[N];
        int b = 1;
        for (int i = N - 1; i >= 0; i--)
        {
            B[i] = b++;
            if (i > 0 && A[i - 1] > A[i]) b = 1;
        }

        FIF = FiF(1200 + 1, R);

        DP = new int[N][];
        for (int i = 0; i < N; i++)
        {
            DP[i] = new int[N + 1];
            for (int j = 0; j <= N; j++) DP[i][j] = -1;
        }

        C = new int[N + 1][];
        for (int i = 0; i <= N; i++)
        {
            C[i] = new int[N + 1];
            for (int j = 0; j <= N; j++) C[i][j] = -1;
        }

        tOut.WriteLine(wdp0());

        tIn.ReadLine();
    }
}
