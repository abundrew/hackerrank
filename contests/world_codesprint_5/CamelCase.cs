using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
/// <summary>
/// https://www.hackerrank.com/contests/world-codesprint-5/challenges/camelcase
/// </summary>
class Solution1
{
    static void Main(String[] args)
    {
        string s = Console.ReadLine();
        int k = 1;
        foreach (char c in s)
            if (char.IsUpper(c)) k++;
        Console.WriteLine(k);
    }
}
