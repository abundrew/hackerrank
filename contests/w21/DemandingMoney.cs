using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
/// <summary>
/// https://www.hackerrank.com/contests/w21/challenges/borrowing-money
/// </summary>
class Solution4
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
    static int[] C = null;
    static bool[,] R = null;

    static CustomDictionary<long, Tuple<int, long>> DP = new CustomDictionary<long, Tuple<int, long>>();

    static int Money(bool[]toSelect, out long ways)
    {
        long key = 0;
        for (int i = 0; i < N; i++)
        {
            if (toSelect[i]) key = key | (((long)1) << (N - 1 - i));
        }

        if (DP.ContainsKey(key))
        {
            Tuple<int, long> result = DP[key];
            ways = result.Item2;
            return result.Item1;
        }

        int ix = 0;
        bool done = true;
        for (int i = 0; i < N; i++)
            if (toSelect[i])
            {
                ix = i;
                done = false;
                break;
            }
        if (done)
        {
            ways = 1;
            return 0;
        }

        bool[] toSelect2 = new bool[N];
        Array.Copy(toSelect, toSelect2, N);
        toSelect2[ix] = false;

        for (int i = 0; i < N; i++)
            if (ix != i && R[ix, i]) toSelect2[i] = false;

        long w1 = 0;
        int money1 = C[ix] + Money(toSelect2, out w1);

        bool[] toSelect3 = new bool[N];
        Array.Copy(toSelect, toSelect3, N);
        toSelect3[ix] = false;

        long w2 = 0;
        int money2 = Money(toSelect3, out w2);

        if (money1 > money2)
        {
            ways = w1;
            DP[key] = new Tuple<int, long>(money1, ways);
            return money1;
        }
        if (money1 < money2)
        {
            ways = w2;
            DP[key] = new Tuple<int, long>(money2, ways);
            return money2;
        }
        ways = w1 + w2;
        DP[key] = new Tuple<int, long>(money1, ways);
        return money1;
    }

    static void Main(String[] args)
    {
        TextReader tIn = Console.In;
        TextWriter tOut = Console.Out;

//        tIn = new StringReader(@"34 2
//100 100 100 100 100 100 100 100 100 100 100 100 100 100 100 100 100 100 100 100 100 100 100 100 100 100 100 100 100 100 100 100 100 100
//3 8
//15 20
//");

        tIn = new StringReader(@"34 0
0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0 0
");



        //        tIn = new StringReader(@"34 34
        //100 100 100 100 100 100 100 100 100 100 100 100 100 100 100 100 100 100 100 100 100 100 100 100 100 100 100 100 100 100 100 100 100 100
        //1 2
        //2 3
        //3 4
        //4 5
        //5 6
        //6 7
        //7 8
        //8 9
        //9 10
        //10 11
        //11 12
        //12 13
        //13 14
        //14 15
        //15 16
        //16 17
        //17 18
        //18 19
        //19 20
        //20 21
        //21 22
        //22 23
        //23 24
        //24 25
        //25 26
        //26 27
        //27 28
        //28 29
        //29 30
        //30 31
        //31 32
        //32 33
        //33 34
        //34 1
        //");

        //        tIn = new StringReader(@"3 2
        //6 8 2
        //1 2
        //3 2");

        int[] nm = tIn.ReadLine().Split().Select(p => int.Parse(p)).ToArray();
        N = nm[0];
        M = nm[1];
        C = tIn.ReadLine().Split().Select(p => int.Parse(p)).ToArray();
        R = new bool[N, N];
        for (int i = 0; i < M; i++)
        {
            int[] ab = tIn.ReadLine().Split().Select(p => int.Parse(p) - 1).ToArray();
            R[ab[0], ab[1]] = true;
            R[ab[1], ab[0]] = true;
        }

        bool[] toSelect = new bool[N];
        for (int i = 0; i < N; i++) toSelect[i] = true;
        long ways = 0;

        DP.MaxItemsToHold = MAX_DICT_CAPACITY;

        int money = Money(toSelect, out ways);

        tOut.WriteLine("{0} {1}", money, ways);

        tIn.ReadLine();
    }
}
