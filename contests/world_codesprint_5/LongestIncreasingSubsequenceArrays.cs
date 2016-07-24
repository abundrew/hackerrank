using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
/// <summary>
/// https://www.hackerrank.com/contests/world-codesprint-5/challenges/longest-increasing-subsequence-arrays
/// </summary>
class Solution4
{
    public static class TailRecursion
    {
        public static T Execute<T>(Func<RecursionResult<T>> func)
        {
            do
            {
                var recursionResult = func();
                if (recursionResult.IsFinalResult)
                    return recursionResult.Result;
                func = recursionResult.NextStep;
            } while (true);
        }

        public static RecursionResult<T> Return<T>(T result)
        {
            return new RecursionResult<T>(true, result, null);
        }

        public static RecursionResult<T> Next<T>(Func<RecursionResult<T>> nextStep)
        {
            return new RecursionResult<T>(false, default(T), nextStep);
        }

    }

    public class RecursionResult<T>
    {
        private readonly bool _isFinalResult;
        private readonly T _result;
        private readonly Func<RecursionResult<T>> _nextStep;
        internal RecursionResult(bool isFinalResult, T result, Func<RecursionResult<T>> nextStep)
        {
            _isFinalResult = isFinalResult;
            _result = result;
            _nextStep = nextStep;
        }

        public bool IsFinalResult { get { return _isFinalResult; } }
        public T Result { get { return _result; } }
        public Func<RecursionResult<T>> NextStep { get { return _nextStep; } }
    }

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

    static long N = 0;
    static long M = 0;
    const long R = 1000000007;

    static CustomDictionary<long, long> DP = null;

    static long PowR(long X, long Y)
    {
        long x = X % R;
        if (Y == 0) return 1;
        if (Y == 1) return x;
        long pow2 = PowR(x, Y / 2);
        long p = pow2 * pow2 * (Y % 2 != 0 ? x : 1);
        return p % R;
    }

    static RecursionResult<long> NN(int ix, int n0)
    {
        long key = ix * 10000000 + n0;
        if (DP.ContainsKey(key)) return TailRecursion.Return(DP[key]);

        if (n0 == N)
        {
            DP[key] = PowR(N, M - ix);
            return TailRecursion.Return(DP[key]);
        }
        if (M - ix < N - n0) return TailRecursion.Return((long)0);
        if (M - ix == N - n0) return TailRecursion.Return((long)1);

        long X1 = TailRecursion.Execute(() => NN(ix + 1, n0));
        long X2 = TailRecursion.Execute(() => NN(ix + 1, n0 + 1));

        long X = (N - 1) * X1 + X2;
        X %= R;
        DP[key] = X;
        return TailRecursion.Return(X);
    }

    static void Main(String[] args)
    {
        TextReader tIn = Console.In;
        TextWriter tOut = Console.Out;

        tIn = new StringReader(@"10
4 4
5 4
6 4
7 4
8 4
9 4
10 4
11 4
12 4
13 4
");

        int T = int.Parse(tIn.ReadLine());
        for (int t = 0; t < T; t++)
        {
            int[] mn = tIn.ReadLine().Split().Select(p => int.Parse(p)).ToArray();
            M = mn[0];
            N = mn[1];

            DP = new CustomDictionary<long, long>();
            DP.MaxItemsToHold = MAX_DICT_CAPACITY;

            long X = TailRecursion.Execute(() => NN(0, 0));

            tOut.WriteLine("{0}, {1}: {2}", M, N, X);
        }

         tIn.ReadLine();
    }
}
