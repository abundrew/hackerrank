using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
/// <summary>
/// https://www.hackerrank.com/contests/zalando-codesprint/challenges/the-inquiring-manager
/// </summary>
class Solution2
{
    static void Main(String[] args)
    {
        TextReader tIn = Console.In;
        TextWriter tOut = Console.Out;

//        tIn = new StringReader(@"11
//1 150 0
//1 3 10
//2 40
//1 143 59
//2 59
//1 100 60
//2 60
//1 159 61
//2 61
//2 120
//2 121
//");

        const int INTERVAL = 60;
        long[,] orders = new long[INTERVAL, 2];
        for (int i = 0; i < INTERVAL; i++) orders[i, 1] = -1;

        int N = int.Parse(tIn.ReadLine());
        for (int i = 0; i < N; i++)
        {
            int[] line = tIn.ReadLine().Split().Select(p => int.Parse(p)).ToArray();
            if (line[0] == 1)
            {
                int t1 = line[2] % INTERVAL;
                int t2 = line[2] / INTERVAL;
                if (orders[t1, 0] < t2 || orders[t1, 1] < line[1])
                {
                    orders[t1, 0] = t2;
                    orders[t1, 1] = line[1];
                }
            }
            else
            {
                int t1 = line[1] % INTERVAL;
                int t2 = line[1] / INTERVAL;
                long mx = -1;
                for (int t = 0; t < INTERVAL; t++)
                {
                    if (t <= t1 && orders[t, 0] == t2 || t > t1 && orders[t, 0] == t2 - 1)
                        if (mx < orders[t, 1]) mx = orders[t, 1];
                }
                tOut.WriteLine(mx);
            }
        }

//        Console.ReadLine();
    }
}
