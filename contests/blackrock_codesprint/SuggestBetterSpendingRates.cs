using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
/// <summary>
/// https://www.hackerrank.com/contests/blackrock-codesprint/challenges/suggest-better-spending-rates
/// </summary>
class Solution3
{
    static int[] S0 = null;
    static int TH = 0;
    static int[] BALANCE_LIMIT_UP = null;
    static int[] BALANCE_LIMIT_DOWN = null;

    public static IEnumerable<int[]> GetCombinations(int index, int balance)
    {
        if (index == S0.Length)
        {
            yield return new int[] { };
        }
        else
        if (index == S0.Length - 1)
        {
            yield return new int[] { S0[index] - balance };
        }
        else
        {
            for (int x = -TH; x < TH + 1; x++)
            {
                int sx = S0[index] + x;
                if (sx < 0 || sx > 99) continue;

                //if (Math.Abs(x + balance) > TH * (S0.Length - index - 1)) continue;

                if (x + balance > BALANCE_LIMIT_DOWN[index + 1] || x + balance < -BALANCE_LIMIT_UP[index + 1]) continue;

                IEnumerable<int[]> tails = GetCombinations(index + 1, x + balance);
                foreach (int[] tail in tails)
                {
                    int[] a = new int[S0.Length - index];
                    a[0] = sx;
                    Buffer.BlockCopy(tail, 0 * 4, a, 1 * 4, tail.Length * 4);
                    yield return a;
                }
            }
        }
    }

    static double Income(double[] cf, int[] s)
    {
        double income = 0;
        double[] ds = new double[s.Length];
        for (int i = 0; i < s.Length; i++) ds[i] = (double)s[i] / 100;
        for (int t = 0; t < cf.Length; t++)
        {
            double income_t = cf[t] * ds[t];
            for (int i = 0; i < t; i++) income_t *= 1 - ds[i];
            income += income_t;
        }
        return income;
    }

    static void Main(String[] args)
    {
        TextReader tIn = Console.In;
        TextWriter tOut = Console.Out;

//        tIn = new StringReader(@"5000 0.0 2 5
//55 33
//");
        string[] ss = tIn.ReadLine().Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries).ToArray();

        int P = int.Parse(ss[0]);
        double R = double.Parse(ss[1]) / 100;
        int T = int.Parse(ss[2]);
        TH = int.Parse(ss[3]);

        double[] cf = new double[T];
        for (int t = 0; t < T; t++) cf[t] = Math.Pow(1.0 + R, t + 1) * P;

        S0 = tIn.ReadLine().Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries).Select(p => int.Parse(p)).ToArray();

        BALANCE_LIMIT_UP = new int[S0.Length];
        BALANCE_LIMIT_DOWN = new int[S0.Length];

        for (int i = 0; i < S0.Length; i++)
        {
            int index = S0.Length - i - 1;
            BALANCE_LIMIT_UP[index] = Math.Min(TH, 99 - S0[index]) + (index < S0.Length - 1 ? BALANCE_LIMIT_UP[index + 1] : 0);
            BALANCE_LIMIT_DOWN[index] = Math.Min(TH, S0[index] - 1) + (index < S0.Length - 1 ? BALANCE_LIMIT_DOWN[index + 1] : 0);
        }

        double income0 = Income(cf, S0);

        double[] bestIncomes = new double[4];
        string[] bestCombinations = new string[4] { "", "", "", "" };

        IEnumerable<IEnumerable<int>> combinations = GetCombinations(0, 0);
        foreach (IEnumerable<int> combination in combinations)
        {
            bestIncomes[3] = Income(cf, combination.ToArray());
            bestCombinations[3] = string.Join(" ", combination.Select(p => p.ToString()).ToArray());

            for (int i = 0; i < 3; i++)
                if (bestIncomes[2 - i] < bestIncomes[3 - i])
                {
                    double income_t = bestIncomes[2 - i];
                    bestIncomes[2 - i] = bestIncomes[3 - i];
                    bestIncomes[3 - i] = income_t;
                    string combination_t = bestCombinations[2 - i];
                    bestCombinations[2 - i] = bestCombinations[3 - i];
                    bestCombinations[3 - i] = combination_t;
                }
                else break;
        }

        tOut.WriteLine(income0.ToString("0.000"));
        for (int i = 0; i < 3; i++)
            tOut.WriteLine(string.Format("{0} - {1}", bestIncomes[i].ToString("0.000"), bestCombinations[i]));

//        Console.ReadLine();
    }
}
