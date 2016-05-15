using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// https://www.hackerrank.com/contests/5-days-of-game-theory/challenges/nimble
/// </summary>

class Solution6
{
    static void Main(String[] args)
    {
        int T = int.Parse(Console.ReadLine());
        for (int t = 0; t < T; t++)
        {
            Console.ReadLine();
            int[] c = Console.ReadLine().Trim().Split(' ').Select(p => int.Parse(p)).ToArray();

            int nimsum = 0;
            for (int i = 1; i < c.Length; i++)
                if (c[i] % 2 == 1)
                    nimsum ^= i;

            Console.WriteLine(nimsum == 0 ? "Second" : "First");
        }
    }
}