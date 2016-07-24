using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
/// <summary>
/// https://www.hackerrank.com/contests/world-codesprint-5/challenges/string-construction
/// </summary>
class Solution2
{
    static void Main(String[] args)
    {
        int T = int.Parse(Console.ReadLine());
        for (int t = 0; t < T; t++)
        {
            string s = Console.ReadLine();
            int k = s.Distinct().Count();
            Console.WriteLine(k);
        }
    }
}
