using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
/// <summary>
/// https://www.hackerrank.com/contests/stryker-codesprint/challenges/kth-zero
/// </summary>
class Solution4
{
    static void Main(String[] args)
    {
        TextReader tIn = Console.In;
        TextWriter tOut = Console.Out;

        tIn = new StringReader(@"5 7    
1  0 3 0 5  
1  2   
2  1 5  
1  1       
2  3 10      
1  1   
2  4 0   
1  2   
");

        int[] nm = tIn.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(p => int.Parse(p)).ToArray();
        int N = nm[0];
        int M = nm[1];

        int[] A = tIn.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(p => int.Parse(p)).ToArray();
        List<int> list = new List<int>();
        for (int i = 0; i < A.Length; i++)
            if (A[i] == 0)
                list.Add(i);

        for (int q = 0; q < M; q++)
        {
            string[] Q = tIn.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToArray();
            if (Q[0] == "1")
            {
                int k = int.Parse(Q[1]);
                if (k <= list.Count)
                    tOut.WriteLine(list[k - 1]);
                else
                    tOut.WriteLine("NO");
                continue;
            }
            if (Q[0] == "2")
            {
                int p = int.Parse(Q[1]);
                int x = int.Parse(Q[2]);
                if (A[p] == 0 && x == 0 || A[p] != 0 && x != 0) continue;
                A[p] = x;
                int index = list.BinarySearch(p);
                if (x == 0)
                    list.Insert(~index, p);
                else
                    list.RemoveAt(index);
            }
        }

        tIn.ReadLine();
    }
}
