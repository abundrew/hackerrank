using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
/// <summary>
/// https://www.hackerrank.com/contests/world-codesprint-6/challenges/hard-drive-disks
/// </summary>
class Solution8
{
    class DComparer : IComparer<long[]>
    {
        public int Compare(long[] x, long[] y)
        {
            return x[1].CompareTo(y[1]);
        }
    }
    class Computer
    {
        public long Place { get; set; }
        public List<long[]> Disks { get; set; }
        public Computer()
        {
            Place = 0;
            Disks = new List<long[]>();
        }
        public void Add(long[] pair)
        {
            Disks.Add(pair);
        }
        public long WireLength {
            get {
                long len = 0;
                foreach (long[] pair in Disks) len += Math.Abs(Place - pair[0]) + Math.Abs(Place - pair[1]);
                return len;
            }
        }
    }
    static void Main(String[] args)
    {
        TextReader tIn = Console.In;
        TextWriter tOut = Console.Out;

        tIn = new StringReader(@"5 2
6 7
-1 1
0 1
5 2
7 3
");

        int[] nk = tIn.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(p => int.Parse(p)).ToArray();
        int N = nk[0];
        int K = nk[1];

        long[][] D = new long[N][];

        for (int i = 0; i < N; i++)
        {
            long[] d = tIn.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(p => long.Parse(p)).ToArray();
            D[i] = new long[] { d.Min(), d.Max() };
        }

        DComparer dc = new DComparer();
        Array.Sort(D, dc);

        Computer[] computers = new Computer[K];
        for (int i = 0; i < K; i++)
            computers[i] = new Computer();

        int ic = 0;
        computers[ic].Place = D[0][1];
        computers[ic].Add(D[0]);
        for (int id = 1; id < D.Length; id++)
        {
            if (D[id][0] <= computers[ic].Place && computers[ic].Place <= D[id][1] || ic == K - 1)
            {
                computers[ic].Add(D[id]);
                continue;
            }
            ic++;
            computers[ic].Place = D[id][1];
            computers[ic].Add(D[id]);
        }

        long totalLength = 0;
        foreach (Computer computer in computers)
            totalLength += computer.WireLength;

        tOut.WriteLine(totalLength);

        tIn.ReadLine();
    }
}
