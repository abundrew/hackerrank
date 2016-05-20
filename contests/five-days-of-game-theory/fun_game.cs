using System;
using System.Linq;

/// <summary>
/// https://www.hackerrank.com/contests/5-days-of-game-theory/challenges/fun-game
/// </summary>
/// 
class Solution9
{
    static void Main(String[] args)
    {
        int T = int.Parse(Console.ReadLine());
        for (int t = 0; t < T; t++)
        {
            Console.ReadLine();
            int[] A = Console.ReadLine().Split(' ').Select(p => int.Parse(p)).ToArray();
            int[] B = Console.ReadLine().Split(' ').Select(p => int.Parse(p)).ToArray();
            int[] X = new int[A.Length];
            for (int i = 0; i < A.Length; i++) X[i] = i;
            Array.Sort(X, (i1, i2) => (A[i2] + B[i2]).CompareTo(A[i1] + B[i1]));
            int V1 = 0;
            int V2 = 0;
            for (int i = 0; i < X.Length; i++)
                if (i % 2 == 0)
                    V1 += A[X[i]];
                else
                    V2 += B[X[i]];

            Console.WriteLine(V1 > V2 ? "First" : (V1 < V2 ? "Second" : "Tie"));
        }
    }
}