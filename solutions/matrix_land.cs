using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
/// <summary>
/// Matrix Land
/// https://www.hackerrank.com/contests/w35/challenges/matrix-land
/// </summary>
class w35_4_MatrixLand
{
    static void Main(String[] args)
    {
        //----------------------------------------------------------------------
        string[] ss = Console.ReadLine().Split();
        int N = int.Parse(ss[0]);
        int M = int.Parse(ss[1]);
        int[][] A = new int[N][];
        for (int i = 0; i < N; i++) {
            A[i] = Console.ReadLine().Split().Select(p => int.Parse(p)).ToArray();
        }

        int[] top = new int[M];
        int[] left = new int[M];
        int[] top_left = new int[M];
        int[] right = new int[M];
        int[] top_right = new int[M];

        top_left[0] = -1000000001;
        top_right[M - 1] = -1000000001;

        for (int i = 0; i < N; i++) {
            for (int j = 1; j < M; j++) {
                left[j] = Math.Max(0, left[j - 1] + A[i][j - 1]);
                top_left[j] = Math.Max(top_left[j - 1], left[j - 1] + top[j - 1]) + A[i][j - 1];
                int k = M - j - 1;
                right[k] = Math.Max(0, right[k + 1] + A[i][k + 1]);
                top_right[k] = Math.Max(top_right[k + 1], right[k + 1] + top[k + 1]) + A[i][k + 1];
            }
            for (int j = 0; j < M; j++)
                top[j] = A[i][j] + Math.Max(top[j] + left[j] + right[j], Math.Max(top_left[j] + right[j], left[j] + top_right[j]));
        }

        int res = -1000000001;
        for (int j = 0; j < M; j++)
            if (res < top[j]) res = top[j];

        Console.WriteLine(res.ToString());
        //----------------------------------------------------------------------
    }
}
