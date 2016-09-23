using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
/// <summary>
/// https://www.hackerrank.com/contests/stryker-codesprint/challenges/julia-and-bst
/// </summary>
class Solution5
{
    static void Main(String[] args)
    {
        TextReader tIn = Console.In;
        TextWriter tOut = Console.Out;

        tIn = new StringReader(@"3
3 1 2
");

        int N = int.Parse(tIn.ReadLine());
        int[] A = tIn.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(p => int.Parse(p)).ToArray();

        long Fsum = 0;
        int[] D = new int[N + 1];
        int[] L = new int[N + 1];
        int[] R = new int[N + 1];

        for (int i = 1; i < N; i++)
        {
            int current = A[0];
            while (true)
            {
                if (A[i] < current)
                {
                    if (L[current] > 0)
                    {
                        current = L[current];
                        continue;
                    }
                    L[current] = A[i];
                    D[A[i]] = D[current] + 1;
                    Fsum += D[current] + 1;
                    break;
                }
                if (A[i] > current)
                {
                    if (R[current] > 0)
                    {
                        current = R[current];
                        continue;
                    }
                    R[current] = A[i];
                    D[A[i]] = D[current] + 1;
                    Fsum += D[current] + 1;
                    break;
                }
            }
        }

        tOut.WriteLine(Fsum);

        tIn.ReadLine();
    }
}
