using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
/// <summary>
/// https://www.hackerrank.com/contests/zalando-codesprint/challenges/give-me-the-order
/// </summary>
class Solution8
{
    static void Main(String[] args)
    {
        TextReader tIn = Console.In;
        TextWriter tOut = Console.Out;

//        tIn = new StringReader(@"6
//1 2 3 4 5 6
//3
//4 5
//3 4
//2 3
//");

        int N = int.Parse(tIn.ReadLine());
        int[] IDs = tIn.ReadLine().Split().Select(p => int.Parse(p)).ToArray();
        int M = int.Parse(tIn.ReadLine());

        int[] temp = new int[IDs.Length];

        const int INT_SIZE = 4;

        for (int m = 0; m < M; m++)
        {
            int[] q = tIn.ReadLine().Split().Select(p => int.Parse(p)).ToArray();
            int index = q[0] - 1;
            int length = q[1] - q[0] + 1;

            //Array.Copy(IDs, 0, temp, 0, index);
            //Array.Copy(IDs, index, IDs, 0, length);
            //Array.Copy(temp, 0, IDs, length, index);

            Buffer.BlockCopy(IDs, 0 * INT_SIZE, temp, 0 * INT_SIZE, index * INT_SIZE);
            Buffer.BlockCopy(IDs, index * INT_SIZE, IDs, 0 * INT_SIZE, length * INT_SIZE);
            Buffer.BlockCopy(temp, 0 * INT_SIZE, IDs, length * INT_SIZE, index * INT_SIZE);
        }

        tOut.WriteLine(string.Join(" ", IDs.Select(p => p.ToString()).ToArray()));

        //Console.ReadLine();
    }
}
