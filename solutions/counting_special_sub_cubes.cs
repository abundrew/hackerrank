using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
/// <summary>
/// https://www.hackerrank.com/contests/openbracket/challenges/counting-special-sub-cubes
/// </summary>
class Solution3
{
    static void Main(String[] args)
    {
        TextReader tIn = Console.In;
        TextWriter tOut = Console.Out;

        tIn = new StringReader(@"3
        3
        1 1 1 1 1 3 1 1 3 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 2 1 2
        3
        1 1 1 1 1 1 1 1 2 1 1 1 1 3 1 2 2 1 1 1 1 1 1 1 1 1 1
        3
        1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 2 1 1 1 3 1 1 1 3 1 2 2
        ");

        //tIn = new StringReader(File.ReadAllText(@"c:\temp\test2.txt"));

        int Q = int.Parse(tIn.ReadLine());
        for (int q = 0; q < Q; q++)
        {
            int N = int.Parse(tIn.ReadLine());
            int[] A = tIn.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(p => int.Parse(p)).ToArray();
            int[,,] F = new int[N, N, N];
            for (int x = 0; x < N; x++)
                for (int y = 0; y < N; y++)
                    for (int z = 0; z < N; z++)
                        F[x, y, z] = A[x * N * N + y * N + z];

            int[,,,] C = new int[N, N, N, N];
            for (int x0 = N - 1; x0 >= 0; x0--)
                for (int y0 = N - 1; y0 >= 0; y0--)
                    for (int z0 = N - 1; z0 >= 0; z0--) {
                        C[x0, y0, z0, 0] = F[x0, y0, z0];
                        int nn = Math.Min(Math.Min(N - x0, N - y0), N - z0);
                        for (int k0 = 1; k0 < nn; k0++)
                        {
                            C[x0, y0, z0, k0] = C[x0, y0, z0, k0 - 1];
                            if (x0 < N - 1 && C[x0, y0, z0, k0] < C[x0 + 1, y0, z0, k0 - 1]) C[x0, y0, z0, k0] = C[x0 + 1, y0, z0, k0 - 1];
                            if (y0 < N - 1 && C[x0, y0, z0, k0] < C[x0, y0 + 1, z0, k0 - 1]) C[x0, y0, z0, k0] = C[x0, y0 + 1, z0, k0 - 1];
                            if (z0 < N - 1 && C[x0, y0, z0, k0] < C[x0, y0, z0 + 1, k0 - 1]) C[x0, y0, z0, k0] = C[x0, y0, z0 + 1, k0 - 1];

                            if (x0 < N - 1 && y0 < N - 1 && C[x0, y0, z0, k0] < C[x0 + 1, y0 + 1, z0, k0 - 1]) C[x0, y0, z0, k0] = C[x0 + 1, y0 + 1, z0, k0 - 1];
                            if (y0 < N - 1 && z0 < N - 1 && C[x0, y0, z0, k0] < C[x0, y0 + 1, z0 + 1, k0 - 1]) C[x0, y0, z0, k0] = C[x0, y0 + 1, z0 + 1, k0 - 1];
                            if (x0 < N - 1 && z0 < N - 1 && C[x0, y0, z0, k0] < C[x0 + 1, y0, z0 + 1, k0 - 1]) C[x0, y0, z0, k0] = C[x0 + 1, y0, z0 + 1, k0 - 1];

                            if (x0 < N - 1 && y0 < N - 1 && z0 < N - 1 && C[x0, y0, z0, k0] < C[x0 + 1, y0 + 1, z0 + 1, k0 - 1]) C[x0, y0, z0, k0] = C[x0 + 1, y0 + 1, z0 + 1, k0 - 1];
                        }
                    }

            int[] X = new int[N];
            for (int x = 0; x < N; x++)
                for (int y = 0; y < N; y++)
                    for (int z = 0; z < N; z++)
                        for (int k = 0; k < N; k++)
                            if (x + k < N && y + k < N && z + k < N && C[x, y, z, k] == k + 1) X[k]++;

            tOut.WriteLine(string.Join(" ", X.Select(p => p.ToString()).ToArray()));
        }

        tIn.ReadLine();
    }
}
