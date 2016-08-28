using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
/// <summary>
/// https://www.hackerrank.com/contests/world-codesprint-6/challenges/abbr
/// </summary>
class Solution4
{
    class Point
    {
        public int Xa { get; set; }
        public int Xb { get; set; }
        public Point(int xa, int xb)
        {
            Xa = xa;
            Xb = xb;
        }
    }
    static void Main(String[] args)
    {
        TextReader tIn = Console.In;
        TextWriter tOut = Console.Out;

        tIn = new StringReader(@"1
daBcd
ABC
");

        int Q = int.Parse(tIn.ReadLine());
        for (int q = 0; q < Q; q++)
        {
            string A = tIn.ReadLine();
            string B = tIn.ReadLine();

            bool yes = false;
            Queue<Point> que = new Queue<Point>();
            que.Enqueue(new Point(0, 0));
            while (que.Count > 0)
            {
                Point p = que.Dequeue();
                int xa = p.Xa;
                int xb = p.Xb;

                if (xa == A.Length)
                {
                    if (xb == B.Length)
                    {
                        yes = true;
                        break;
                    };
                    continue;
                }

                if (xb == B.Length)
                {
                    yes = true;
                    for (int i = xa; i < A.Length; i++)
                        if (char.IsUpper(A[i]))
                        {
                            yes = false;
                            break;
                        }
                    if (yes) break;
                    continue;
                }

                while (xa < A.Length && char.IsLower(A[xa]) && char.ToUpper(A[xa]) != B[xb]) xa++;
                if (xa == A.Length) continue;
                if (char.IsUpper(A[xa]) && A[xa] != B[xb]) continue;

                que.Enqueue(new Point(xa + 1, xb + 1));
                if (char.IsLower(A[xa])) que.Enqueue(new Point(xa + 1, xb));
            }
            tOut.WriteLine(yes ? "YES" : "NO");
        }

        tIn.ReadLine();
    }
}
