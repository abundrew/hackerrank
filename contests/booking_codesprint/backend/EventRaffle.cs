using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
/// <summary>
/// https://www.hackerrank.com/contests/booking-passions-hacked-backend/challenges/lottery-ii
/// </summary>
class Solution5
{
    static void Main(String[] args)
    {
        TextReader tIn = Console.In;
        TextWriter tOut = Console.Out;

//        tIn = new StringReader(@"3
//1 3 museum wine_tasting biking
//3 2 museum flower_parade
//2 2 hiking biking
//1 flower_parade
//1 museum
//2 biking wine_tasting
//");

        int N = int.Parse(tIn.ReadLine());
        int[] IDs = new int[N];
        HashSet<string>[] P = new HashSet<string>[N];

        for (int i = 0; i < N; i++)
        {
            string[] ss = tIn.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            IDs[i] = int.Parse(ss[0]) - 1;
            int k = int.Parse(ss[1]);
            P[i] = new HashSet<string>();
            for (int j = 0; j < k; j++) P[i].Add(ss[j + 2]);
        }

        HashSet<string>[] E = new HashSet<string>[N];
        for (int i = 0; i < N; i++)
        {
            string[] ss = tIn.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            int k = int.Parse(ss[0]);
            E[i] = new HashSet<string>();
            for (int j = 0; j < k; j++) E[i].Add(ss[j + 1]);
        }

        int[,] M = new int[N, N];
        for (int e = 0; e < N; e++)
            for (int p = 0; p < N; p++)
                if (E[IDs[e]].Intersect(P[p]).Any()) M[e, p] = 1;

        bool[] et = new bool[N];
        bool[] pt = new bool[N];

        int X = 0;
        while (true)
        {
            bool found = false;
            int ex = -1;
            int psumx = 0;
            for (int e = 0; e < N; e++)
                if (!et[e])
                {
                    int psum = 0;
                    for (int p = 0; p < N; p++)
                        if (!pt[p]) psum += M[e, p];
                    if (psum > 0 && (ex == -1 || psum < psumx))
                    {
                        ex = e;
                        psumx = psum;
                        found = true;
                    }
                }
            if (found)
            {
                int px = 0;
                int esumx = 0;
                for (int p = 0; p < N; p++)
                    if (!pt[p] && M[ex, p] > 0)
                    {
                        int esum = 0;
                        for (int e = 0; e < N; e++)
                            if (!et[e]) esum += M[e, p];
                        if (esum > 0 && (px == -1 || esum < esumx))
                        {
                            px = p;
                            esumx = esum;
                        }
                    }

                X++;
                et[ex] = true;
                pt[px] = true;
            } else break;
        }

        tOut.WriteLine(X);

        //tIn.ReadLine();
    }
}
