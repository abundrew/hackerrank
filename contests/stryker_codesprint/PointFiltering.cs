using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
/// <summary>
/// https://www.hackerrank.com/contests/stryker-codesprint/challenges/point-filtering
/// </summary>
class Solution2
{
    class Point
    {
        public int ID { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
    }
    static void Main(String[] args)
    {
        TextReader tIn = Console.In;
        TextWriter tOut = Console.Out;

        tIn = new StringReader(@"4 2
1 25.5 50.2 -60.5
2 12.2 60.2 -75.89
3 65.1 25.6 -55.9
4 22.6 12.6 -30.8
F 3
F 2
R 2
R 4
R 1
R 2
");

        int[] nb = tIn.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(p => int.Parse(p)).ToArray();
        int N = nb[0];
        int B = nb[1];

        List<Point> points = new List<Point>();
        Dictionary<int, Point> dic = new Dictionary<int, Point>();
        HashSet<int> bucket = new HashSet<int>();

        for (int i = 0; i < N; i++)
        {
            string[] pp = tIn.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToArray();
            Point p = new Point()
            {
                ID = int.Parse(pp[0]),
                X = double.Parse(pp[1]),
                Y = double.Parse(pp[2]),
                Z = double.Parse(pp[3])
            };
            points.Add(p);
            dic[p.ID] = p;
        }
        points.Sort((p1, p2) => p2.Z.CompareTo(p1.Z));
        int ip = 0;
        for (int i = 0; i < B; i++)
        {
            bucket.Add(points[ip].ID);
            ip++;
        }

        while (true)
        {
            string line = tIn.ReadLine();
            if (string.IsNullOrEmpty(line)) break;

            string[] pp = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToArray();
            int K = int.Parse(pp[1]);
            if (pp[0] == "f" || pp[0] == "F")
            {
                if (bucket.Contains(K)) {
                    Point p = dic[K];
                    tOut.WriteLine("{0} = ({1:0.000},{2:0.000},{3:0.000})", K, p.X, p.Y, p.Z);
                } else
                    tOut.WriteLine("Point doesn't exist in the bucket.");
                continue;
            }
            if (pp[0] == "r" || pp[0] == "R")
            {
                if (bucket.Contains(K))
                {
                    if (ip < N)
                    {
                        bucket.Remove(K);
                        tOut.WriteLine("Point id {0} removed.", K);
                        bucket.Add(points[ip].ID);
                        ip++;
                    }
                    else
                        tOut.WriteLine("No more points can be deleted.");
                }
                else
                    tOut.WriteLine("Point doesn't exist in the bucket.");
                continue;
            }
        }

        tIn.ReadLine();
    }
}
