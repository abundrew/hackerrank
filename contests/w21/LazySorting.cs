using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
/// <summary>
/// https://www.hackerrank.com/contests/w21/challenges/lazy-sorting
/// </summary>
class Solution3
{
    static void Main(String[] args)
    {
        TextReader tIn = Console.In;
        TextWriter tOut = Console.Out;

        List<int> primes = new List<int>();
        for (int i = 2; i < 101; i++)
        {
            bool prime = true;
            foreach (int p in primes)
                if (i % p == 0)
                {
                    prime = false;
                    break;
                }
            if (prime) primes.Add(i);
        }

        tIn = new StringReader(@"2
5 2
");
        int N = int.Parse(tIn.ReadLine());
        int[] P = tIn.ReadLine().Split().Select(p => int.Parse(p)).ToArray();
        Dictionary<int, int> counts = new Dictionary<int, int>();
        bool sorted = true;
        for (int i = 0; i < N; i++)
        {
            if (!counts.ContainsKey(P[i])) counts[P[i]] = 0;
            counts[P[i]]++;
            if (i > 0 && P[i] < P[i - 1]) sorted = false;
        }

        if (!sorted)
        {
            Dictionary<int, int> number = new Dictionary<int, int>();
            for (int i = 2; i <= N; i++)
            {
                int k = i;
                foreach (int p in primes)
                {
                    while (k % p == 0)
                    {
                        k /= p;
                        if (!number.ContainsKey(p)) number[p] = 0;
                        number[p]++;
                    }
                }
            }
            foreach (int c in counts.Keys)
            {
                for (int i = 2; i <= counts[c]; i++)
                {
                    int k = i;
                    foreach (int p in primes)
                    {
                        while (k % p == 0)
                        {
                            k /= p;
                            number[p]--;
                            if (number[p] == 0) number.Remove(p);
                        }
                    }
                }
            }
            List<int> digits = new List<int>();
            digits.Add(1);
            foreach (int n in number.Keys)
                for (int i = 0; i < number[n]; i++)
                {
                    for (int j = 0; j < digits.Count; j++) digits[j] *= n;
                    int carryon = 0;
                    for (int j = 0; j < digits.Count; j++)
                    {
                        digits[j] += carryon;
                        carryon = digits[j] / 10;
                        digits[j] %= 10;
                    }
                    while (carryon > 0)
                    {
                        digits.Add(carryon);
                        carryon = digits[digits.Count - 1] / 10;
                        digits[digits.Count - 1] %= 10;
                    }
                }
            digits.Reverse();
            tOut.WriteLine("{0}.000000", string.Join("", digits.Select(p => p.ToString()).ToArray()));
        } else tOut.WriteLine("0.000000");

        tIn.ReadLine();
    }
}
