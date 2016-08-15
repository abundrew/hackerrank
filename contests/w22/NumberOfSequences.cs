using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
/// <summary>
/// https://www.hackerrank.com/contests/w22/challenges/number-of-sequences
/// </summary>
class Solution4
{
    const long R = 1000000007;
    static int N = 0;
    static int[] A = null;

    static bool CheckLR(int k)
    {
        if (A[k] == -1)
        {
            int m = k + (k + 1);
            while (m < N)
            {
                if (A[m] == -1)
                {
                    if (!CheckLR(m)) return false;
                }
                if (A[m] > -1)
                {
                    A[k] = A[m] % (k + 1);
                    break;
                }
                m += k + 1;
            }
        }
        if (A[k] > -1)
        {
            int m = k + (k + 1);
            while (m < N)
            {
                if (A[m] > -1 && A[k] != A[m] % (k + 1)) return false;
                m += k + 1;
            }
        }
        return true;
    }

    static void Main(String[] args)
    {
        TextReader tIn = Console.In;
        TextWriter tOut = Console.Out;

        tIn = new StringReader(@"3
0 -1 -1
");

        N = int.Parse(tIn.ReadLine());
        A = tIn.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(p => int.Parse(p)).ToArray();

        A[0] = 0;
        bool ok = true;

        for (int k = 1; k < N; k++)
        {
            ok = CheckLR(k);
            if (!ok) break;
        }

        long K = 0;
        if (ok)
        {
            Dictionary<int, int> plus = new Dictionary<int, int>();
            Dictionary<int, int> minus = new Dictionary<int, int>();

            List<int> primes = new List<int>(new int[] { 2 });
            int maxP = (int)Math.Sqrt(N) + 1;
            for (int k = 0; k < N; k++)
            {
                int n = k + 1;
                Dictionary<int, int> pm = A[k] == -1 ? plus : minus;

                foreach (int p in primes)
                {
                    if (p >= maxP) break;
                    int m = 0;
                    while (n % p == 0)
                    {
                        n /= p;
                        m++;
                    }
                    if (!pm.ContainsKey(p)) pm[p] = 0;
                    pm[p] = Math.Max(pm[p], m);
                }
                if (n > 1)
                {
                    primes.Add(n);
                    pm[n] = 1;
                }
            }

            K = 1;

            foreach (int p in plus.Keys)
            {
                int n = plus[p];
                if (minus.ContainsKey(p)) n -= minus[p];
                for (int i = 0; i < n; i++)
                {
                    K *= p;
                    K %= R;
                }
            }
        }

        tOut.WriteLine(K);

        tIn.ReadLine();
    }
}
