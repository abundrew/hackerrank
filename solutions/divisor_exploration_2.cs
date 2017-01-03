using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
/// <summary>
/// Divisor Exploration II
/// https://www.hackerrank.com/contests/infinitum17/challenges/divisor-exploration-2
/// 
/// Product (k from 0 less than m) of
/// Pk^(a+k+3)-1-(Pk-1)(a+k+3)/(Pk-1)^2
/// 
/// </summary>
class Solution5
{
    // ---------------------------------------------------------------------
    public static long Multiply(long a, long b, int modulus)
    {
        return (a * b) % modulus;
    }
    public static long Divide(long a, long b, int modulus)
    {
        return (a * Inverse(b, modulus)) % modulus;
    }
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
    public static long Choose(long n, int r, int modulus)
    {
        long choose = 1;
        while (true)
        {
            if (r == 0) break;
            long N = n % modulus;
            int R = r % modulus;
            if (N < R) return 0;

            for (int i = 0; i < R; i++)
                choose = (choose * (N - i)) % modulus;
            long factR = 1;
            for (int i = 0; i < R; i++)
                factR = (factR * (i + 1)) % modulus;
            choose = Divide(choose, factR, modulus);
            if (choose < 0) choose += modulus;
            n /= modulus;
            r /= modulus;
        }
        return choose;
    }
    static long Inverse(long a, int modulus)
    {
        long b = modulus;
        long p = 1, q = 0;
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
        return p < 0 ? p + modulus : p;
    }
    // ---------------------------------------------------------------------
    public static List<int> Primes(int limit)
    {
        List<int> primes = new List<int>();
        for (int k = 2; primes.Count < limit; k++)
        {
            bool prime = true;
            int rk = (int)Math.Sqrt(k);
            foreach (int p in primes)
            {
                if (rk < p) break;
                if (k % p == 0)
                {
                    prime = false;
                    break;
                }
            }
            if (prime) primes.Add(k);
        }
        return primes;
    }
    // ---------------------------------------------------------------------
    static void Main(String[] args)
    {
        TextReader tIn = Console.In;
        TextWriter tOut = Console.Out;

        tIn = new StringReader(@"3
2 0
3 0
2 4
");

        int MMAX = 200000;
        int R = 1000000007;

        List<int> primes = Primes(MMAX + 1);
        long[] inverses = new long[primes.Count];
        for (int i = 0; i < inverses.Length; i++)
            inverses[i] = Inverse(((long)primes[i] - 1) * (primes[i] - 1), R);

        int Q = int.Parse(tIn.ReadLine());
        for (int q = 0; q < Q; q++)
        {
            string[] ma = tIn.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToArray();
            int M = int.Parse(ma[0]);
            int A = int.Parse(ma[1]);

            long S = 1;
            for (int k = 0; k < M; k++)
            {
                long Pk = primes[k];
                long X = Power(Pk, A + k + 3, R) - 1 - (Pk - 1) * (A + k + 3);
                while (X < 0) X += R;
                X %= R;
                S = Multiply(S, Multiply(X, inverses[k], R), R);
            }

            tOut.WriteLine(S);
        }

        tIn.ReadLine();
    }
}
