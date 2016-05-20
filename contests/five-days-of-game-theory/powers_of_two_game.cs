using System;
using System.Collections.Generic;
using System.IO;

/// <summary>
/// https://www.hackerrank.com/contests/5-days-of-game-theory/challenges/powers-of-two-game
/// </summary>
/// 
class Solution
{
    static void Main(String[] args)
    {
        int T = int.Parse(Console.ReadLine());
        for (int t = 0; t < T; t++)
        {
            int N = int.Parse(Console.ReadLine());
            Console.WriteLine(N % 8 == 0 ? "Second" : "First");
        }
    }
}