using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// https://www.hackerrank.com/contests/5-days-of-game-theory/challenges/tower-breakers-2
/// </summary>

class Solution8
{
    static List<int> primes = new List<int>() { 2 };

    static void Main(String[] args)
    {
        int T = int.Parse(Console.ReadLine());
        for (int t = 0; t < T; t++)
        {
            Console.ReadLine();
            int[] h = Console.ReadLine().Trim().Split(' ').Select(p => int.Parse(p)).ToArray();

            int hmax = h.Max();
            int rhmax = (int)Math.Sqrt(hmax);

            for (int k = primes[primes.Count - 1] + 1; k <= rhmax; k++)
            {
                bool prime = true;
                foreach (int p in primes)
                    if (k % p == 0)
                    {
                        prime = false;
                        break;
                    }
                if (prime) primes.Add(k);
            }

            int[] a = new int[h.Length];
            for (int i = 0; i < h.Length; i++)
            {
                int rh = (int)Math.Sqrt(h[i]);
                foreach (int p in primes)
                {
                    if (p > rh) break;
                    while (h[i] % p == 0)
                    {
                        h[i] /= p;
                        a[i]++;
                    }
                }
                if (h[i] > 1) a[i]++;
            }

            int nimsum = 0;
            foreach (int x in a) nimsum ^= x;

            Console.WriteLine(nimsum == 0 ? "2" : "1");
        }
    }
}