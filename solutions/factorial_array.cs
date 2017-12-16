using System;
using System.IO;
using System.Text;
/// <summary>
/// Factorial Array
/// https://www.hackerrank.com/contests/world-codesprint-12/challenges/factorial-array
/// </summary>
class FactorialArray
{
    static void Main(String[] args)
    {
        //----------------------------------------------------------------------
        StreamReader sr = new StreamReader(Console.OpenStandardInput());
        StreamWriter sw = new StreamWriter(Console.OpenStandardOutput());
        //----------------------------------------------------------------------
        int R = 1000000000;
        long[] F = new long[41];
        F[1] = 1;
        for (int i = 2; i < 41; i++)
            F[i] = (F[i - 1] * i) % R;
        int[] nm = Array.ConvertAll(sr.ReadLine().Split((char[])null, StringSplitOptions.RemoveEmptyEntries), int.Parse);
        int N = nm[0];
        int M = nm[1];
        long[] A = Array.ConvertAll(sr.ReadLine().Split((char[])null, StringSplitOptions.RemoveEmptyEntries), long.Parse);

        int zB = (int)Math.Sqrt(N);
        int kB = (N + zB - 1) / zB;
        int[][] B = new int[kB][];
        for (int i = 0; i < kB; i++)
            B[i] = new int[41];
        int[] D = new int[kB];

        for (int i = 0; i < N; i++)
        {
            int k = i / zB;
            if (A[i] < 41) B[k][A[i]]++;
        }

        for (int ixm = 0; ixm < M; ixm++)
        {
            int[] qry = Array.ConvertAll(sr.ReadLine().Split((char[])null, StringSplitOptions.RemoveEmptyEntries), int.Parse);
            if (qry[0] == 1)
            {
                int a = qry[1] - 1;
                int b = qry[2] - 1;

                int ka = a / zB;
                int kb = b / zB;

                for (int i = 0; i < 41; i++) B[ka][i] = 0;
                for (int i = zB * ka; i < Math.Min(N, zB * (ka + 1)); i++)
                {
                    A[i] += D[ka];
                    if (i >= a && i <= b) A[i]++;
                    if (A[i] < 41) B[ka][A[i]]++;
                }
                D[ka] = 0;

                if (kb > ka)
                {
                    for (int i = 0; i < 41; i++) B[kb][i] = 0;
                    for (int i = zB * kb; i < Math.Min(N, zB * (kb + 1)); i++)
                    {
                        A[i] += D[kb];
                        if (i >= a && i <= b) A[i]++;
                        if (A[i] < 41) B[kb][A[i]]++;
                    }
                    D[kb] = 0;
                }

                for (int k = ka + 1; k < kb; k++) D[k]++;
            }
            if (qry[0] == 2)
            {
                int a = qry[1] - 1;
                int b = qry[2] - 1;

                int[] count = new int[41];

                int ka = a / zB;
                int kb = b / zB;

                for (int i = 0; i < 41; i++) B[ka][i] = 0;
                for (int i = zB * ka; i < Math.Min(N, zB * (ka + 1)); i++)
                {
                    A[i] += D[ka];
                    if (i >= a && i <= b)
                    {
                        if (A[i] < 41) count[A[i]]++;
                    }
                    if (A[i] < 41) B[ka][A[i]]++;
                }
                D[ka] = 0;

                if (kb > ka)
                {
                    for (int i = 0; i < 41; i++) B[kb][i] = 0;
                    for (int i = zB * kb; i < Math.Min(N, zB * (kb + 1)); i++)
                    {
                        A[i] += D[kb];
                        if (i >= a && i <= b)
                        {
                            if (A[i] < 41) count[A[i]]++;
                        }
                        if (A[i] < 41) B[kb][A[i]]++;
                    }
                    D[kb] = 0;
                }

                for (int k = ka + 1; k < kb; k++)
                    for (int i = 0; i < 41; i++)
                    {
                        if (D[k] + i < 41)
                            count[D[k] + i] += B[k][i];
                    }

                long sum = 0;
                for (int i = 1; i < 41; i++)
                    sum = (sum + (F[i] * count[i]) % R) % R;
                sw.WriteLine(sum);
            }
            if (qry[0] == 3)
            {
                int ix = qry[1] - 1;
                int v = qry[2];

                int k = ix / zB;
                for (int i = 0; i < 41; i++) B[k][i] = 0;
                for (int i = zB * k; i < Math.Min(N, zB * (k + 1)); i++)
                {
                    A[i] += D[k];
                    if (i == ix) A[i] = v;
                    if (A[i] < 41) B[k][A[i]]++;
                }
                D[k] = 0;
            }
        }
        //----------------------------------------------------------------------
        sr.Dispose();
        sw.Dispose();
        //----------------------------------------------------------------------
    }
}
