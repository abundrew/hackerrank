using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// https://www.hackerrank.com/contests/5-days-of-game-theory/challenges/day-2-poker-nim
/// </summary>

class Solution7
{
    static void Main(String[] args)
    {
        int T = int.Parse(Console.ReadLine());
        for (int t = 0; t < T; t++)
        {
            Console.ReadLine();
            int[] a = Console.ReadLine().Trim().Split(' ').Select(p => int.Parse(p)).ToArray();

            int nimsum = 0;
            foreach (int x in a) nimsum ^= x;

            Console.WriteLine(nimsum == 0 ? "Second" : "First");
        }
    }
}