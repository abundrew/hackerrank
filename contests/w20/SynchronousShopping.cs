using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
/// <summary>
/// https://www.hackerrank.com/contests/w20/challenges/synchronous-shopping
/// </summary>
class Solution3
{
    struct Score
    {
        public int Fish { get; set; }
        public int Time { get; set; }
        public Score(int fish, int time)
        {
            Fish = fish;
            Time = time;
        }
    }

    struct Position
    {
        public int Center { get; set; }
        public Score Score { get; set; }
        public Position(int center, Score score)
        {
            Center = center;
            Score = score;
        }
    }

    static void Main(String[] args)
    {
        TextReader tIn = Console.In;
        TextWriter tOut = Console.Out;

//        tIn = new StringReader(@"5 5 5
//1 1
//1 2
//1 3
//1 4
//1 5
//1 2 10
//1 3 10
//2 4 10
//3 5 10
//4 5 10
//");

        int[] ATYPE = new int[] { 0x0001, 0x0002, 0x0004, 0x0008, 0x0010, 0x0020, 0x0040, 0x0080, 0x0100, 0x0200 };

        int[] nmk = tIn.ReadLine().Split().Select(p => int.Parse(p)).ToArray();
        int N = nmk[0];
        int M = nmk[1];
        int K = nmk[2];

        int ALL = ATYPE.Where((p, i) => i < K).Sum();

        int[] A = new int[N];
        for (int i = 0; i < N; i++)
        {
            int[] ak = tIn.ReadLine().Split().Select(p => int.Parse(p)).ToArray();
            for (int j = 1; j < ak.Length; j++) A[i] += ATYPE[ak[j] - 1];
        }

        Dictionary<int, int>[] R = new Dictionary<int, int>[N];
        for (int i = 0; i < N; i++) R[i] = new Dictionary<int, int>();

        for (int i = 0; i < M; i++)
        {
            int[] xyz = tIn.ReadLine().Split().Select(p => int.Parse(p)).ToArray();
            R[xyz[0] - 1][xyz[1] - 1] = xyz[2];
            R[xyz[1] - 1][xyz[0] - 1] = xyz[2];
        }

        List<Score>[] DP = new List<Score>[N];
        for (int i = 0; i < N; i++) DP[i] = new List<Score>();

        int NFish = 0;
        int minAmount = int.MaxValue;

        Queue<Position> Q = new Queue<Position>();
        Q.Enqueue(new Position(0, new Score(A[0], 0)));

        while (Q.Count > 0)
        {
            Position p = Q.Dequeue();
            bool bad = false;
            for (int i = DP[p.Center].Count - 1; i >= 0; i--)
            {
                if ((p.Score.Fish & DP[p.Center][i].Fish) == p.Score.Fish && p.Score.Time >= DP[p.Center][i].Time)
                {
                    bad = true;
                    break;
                }
                if ((p.Score.Fish & DP[p.Center][i].Fish) == DP[p.Center][i].Fish && p.Score.Time < DP[p.Center][i].Time)
                {
                    DP[p.Center].RemoveAt(i);
                }
            }
            if (bad) continue;
            DP[p.Center].Add(new Score(p.Score.Fish, p.Score.Time));

            if (p.Center == N - 1)
            {
                NFish |= p.Score.Fish;
                if (NFish == ALL)
                {
                    for (int i = 0; i < DP[N - 1].Count; i++)
                        for (int j = i; j < DP[N - 1].Count; j++)
                            if ((DP[N - 1][i].Fish | DP[N - 1][j].Fish) == ALL)
                            {
                                int amount = Math.Max(DP[N - 1][i].Time, DP[N - 1][j].Time);
                                if (amount < minAmount) minAmount = amount;
                            }
                }
            }

            if (p.Score.Time < minAmount || p.Center != N - 1 || p.Score.Fish != ALL)
                foreach (int c in R[p.Center].Keys)
                    Q.Enqueue(new Position(c, new Score(p.Score.Fish | A[c], p.Score.Time + R[p.Center][c])));
        }

        for (int i = 0; i < DP[N - 1].Count; i++)
            for (int j = i; j < DP[N - 1].Count; j++)
                if ((DP[N - 1][i].Fish | DP[N - 1][j].Fish) == ALL) {
                    int amount = Math.Max(DP[N - 1][i].Time, DP[N - 1][j].Time);
                    if (amount < minAmount) minAmount = amount;
                }
        Console.WriteLine(minAmount);
    }
}
