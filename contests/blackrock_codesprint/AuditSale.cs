using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
/// <summary>
/// https://www.hackerrank.com/contests/blackrock-codesprint/challenges/audit-sale
/// </summary>
class Solution6
{
    const int MAX_DICT_CAPACITY = 500000;

    public class CustomDictionary<TKey, TValue> : Dictionary<TKey, TValue>
    {
        public int MaxItemsToHold { get; set; }

        private Queue<TKey> orderedKeys = new Queue<TKey>();

        public new void Add(TKey key, TValue value)
        {
            orderedKeys.Enqueue(key);
            if (this.MaxItemsToHold != 0 && this.Count >= MaxItemsToHold)
            {
                this.Remove(orderedKeys.Dequeue());
            }

            base.Add(key, value);
        }
    }

    static int N = 0;
    static int M = 0;
    static int K100 = 0;
    static int KP = 0;
    static long[][] securities = null;

    static int[] securityIndexes = null;

    static CustomDictionary<long, long> DP = new CustomDictionary<long, long>();

    static long MaxExpectation(int index, int k100, int kp)
    {
        if (k100 == 0 && kp == 0) return 0;
        if (index == securityIndexes.Length) return long.MinValue;
        if (k100 + kp > securityIndexes.Length - index) return long.MinValue;

        long key = ((long)index * (K100 + 1) + k100) * (KP + 1) + kp;
        if (DP.ContainsKey(key)) return DP[key];
        long maxexp = index >= M || kp == 0 ? MaxExpectation(index + 1, k100, kp) : long.MinValue;
        if (k100 > 0 && (kp == 0 || securities[securityIndexes[index]][1] < 100))
        {
            long exp1 = MaxExpectation(index + 1, k100 - 1, kp);
            if (exp1 > long.MinValue)
            {
                long exp = securities[securityIndexes[index]][3] + exp1;
                if (maxexp < exp) maxexp = exp;
            }
        }
        if (kp > 0)
        {
            long exp1 = MaxExpectation(index + 1, k100, kp - 1);
            if (exp1 > long.MinValue)
            {
                long exp = securities[securityIndexes[index]][2] + exp1;
                if (maxexp < exp) maxexp = exp;
            }
        }

        if (maxexp > long.MinValue)
        {
            DP[key] = maxexp;
        }
        return maxexp;
    }

    class SecurityKPComparer : IComparer<int>
    {
        public int Compare(int x, int y)
        {
            int cmp = securities[y][2].CompareTo(securities[x][2]);
            if (cmp == 0)
                cmp = securities[y][3].CompareTo(securities[x][3]);
            return cmp;
        }
    }

    class SecurityK100Comparer : IComparer<int>
    {
        public int Compare(int x, int y)
        {
            int cmp = securities[y][3].CompareTo(securities[x][3]);
            if (cmp == 0)
                cmp = securities[y][2].CompareTo(securities[x][2]);
            return cmp;
        }
    }

    static void Main(String[] args)
    {
        TextReader tIn = Console.In;
        TextWriter tOut = Console.Out;


        int[] nmk = tIn.ReadLine().Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries).Select(p => int.Parse(p)).ToArray();
        N = nmk[0];
        M = nmk[1];
        K100 = nmk[2];
        KP = M - K100;

        securities = new long[N][];

        for (int i = 0; i < N; i++)
        {
            long[] pc = tIn.ReadLine().Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries).Select(p => long.Parse(p)).ToArray();
            securities[i] = new long[4];
            securities[i][0] = pc[0];
            securities[i][1] = pc[1];
            securities[i][2] = pc[0] * pc[1];
            securities[i][3] = pc[0] * 100;
        }

        int[] indexesKP = new int[N];
        int[] indexesK100 = new int[N];
        for (int i = 0; i < N; i++)
        {
            indexesKP[i] = i;
            indexesK100[i] = i;
        }

        Array.Sort(indexesKP, new SecurityKPComparer());
        Array.Sort(indexesK100, new SecurityK100Comparer());

        long maxexp = 0;

        if (K100 == 0)
        {
            maxexp = indexesKP.Take(M).Select(p => securities[p][2]).Sum();
        }
        else if (KP == 0)
        {
            maxexp = indexesK100.Take(M).Select(p => securities[p][3]).Sum();
        }
        else
        {
            securityIndexes = indexesKP.Union(indexesK100).OrderByDescending(p => securities[p][2]).ToArray();
            DP.MaxItemsToHold = MAX_DICT_CAPACITY;
            maxexp = Math.Max(0, MaxExpectation(0, K100, KP));
        }
        tOut.WriteLine(maxexp);
    }
}
