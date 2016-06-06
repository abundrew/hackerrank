using System;
using System.Linq;
/// <summary>
/// https://www.hackerrank.com/contests/w20/challenges/divisible-sum-pairs
/// </summary>
class Solution1
{
    static void Main(String[] args)
    {
        int k = int.Parse(Console.ReadLine().Split(' ').Last());
        int[] a = Console.ReadLine().Split(' ').Select(p => int.Parse(p)).ToArray();
        int[] mk = new int[k];
        for (int i = 0; i < a.Length; i++) mk[a[i] % k]++;
        long x = 0;
        x = (mk[0] - 1) * mk[0] / 2;
        for (int i = 1; i < (k + 1) / 2; i++)
            x += mk[i] * mk[k - i];
        if (k % 2 == 0)
            x += (mk[k / 2] - 1) * mk[k / 2] / 2;
        Console.WriteLine(x);
    }
}
