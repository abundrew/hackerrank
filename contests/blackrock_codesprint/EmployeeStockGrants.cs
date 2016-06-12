using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
/// <summary>
/// https://www.hackerrank.com/contests/blackrock-codesprint/challenges/employee-stock-grants
/// </summary>
class Solution5
{
    static void Main(String[] args)
    {
        TextReader tIn = Console.In;
        TextWriter tOut = Console.Out;

        tIn = new StringReader(@"12
6 1 1 1 2 2 2 3 3 3 4 5
2 1 2 3 2 1 2 3 2 1 2 3");

        tIn.ReadLine();
        int[] R = tIn.ReadLine().Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries).Select(p => int.Parse(p)).ToArray();
        long[] M = tIn.ReadLine().Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries).Select(p => long.Parse(p)).ToArray();

        bool odd = true;
        while (true)
        {
            bool changes = false;
            for (int i = 0; i < M.Length; i++)
            {
                int ix = odd ? i : M.Length - i - 1;
                while (true)
                {
                    bool changes2 = false;
                    for (int j = ix - 10; j < ix + 10 + 1; j++)
                        if (j != ix && j >= 0 && j < M.Length)
                        {
                            if (R[ix] < R[j] && M[ix] >= M[j])
                            {
                                M[j] = M[ix] + 1;
                                changes = true;
                                changes2 = true;
                            }
                            if (R[ix] > R[j] && M[ix] <= M[j])
                            {
                                M[ix] = M[j] + 1;
                                changes = true;
                                changes2 = true;
                            }
                        }
                    if (!changes2) break;
                }
            }
            if (!changes) break;
            odd = !odd;
        }

        tOut.WriteLine(M.Sum());

        Console.ReadLine();
    }
}
