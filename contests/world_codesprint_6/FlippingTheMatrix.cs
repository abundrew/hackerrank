using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
/// <summary>
/// https://www.hackerrank.com/contests/world-codesprint-6/challenges/flipping-the-matrix
/// </summary>
class Solution3
{
    static void Main(String[] args)
    {
        TextReader tIn = Console.In;
        TextWriter tOut = Console.Out;

        tIn = new StringReader(@"1
2
112 42 83 119
56 125 56 49
15 78 101 43
62 98 114 108
");

        int Q = int.Parse(tIn.ReadLine());
        for (int q = 0; q < Q; q++)
        {
            int N = int.Parse(tIn.ReadLine());
            int[][] matrix = new int[N * 2][];
            for (int i = 0; i < N * 2; i++)
                matrix[i] = tIn.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(p => int.Parse(p)).ToArray();

            int sum = 0;
            int[] m4 = new int[4];
            for (int r = 0; r < N; r++)
                for (int c = 0; c < N; c++)
                {
                    m4[0] = matrix[r][c];
                    m4[1] = matrix[r][N * 2 - 1 - c];
                    m4[2] = matrix[N * 2 - 1 - r][c];
                    m4[3] = matrix[N * 2 - 1 - r][N * 2 - 1 - c];
                    sum += m4.Max();
                }

            tOut.WriteLine(sum);
        }

        tIn.ReadLine();
    }
}
