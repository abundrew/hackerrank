using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
/// <summary>
/// https://www.hackerrank.com/contests/zalando-codesprint/challenges/does-it-fit
/// </summary>
class Solution4
{
    static void Main(String[] args)
    {
        TextReader tIn = Console.In;
        TextWriter tOut = Console.Out;

//        tIn = new StringReader(@"4 5
//3
//R 1 2
//R 5 5
//C 2
//");

        int[] wh = tIn.ReadLine().Split().Select(p => int.Parse(p)).ToArray();
        int W = wh[0];
        int H = wh[1];
        int B = Math.Min(W, H);
        int A = Math.Max(W, H);
        int maxR = B / 2;
        long N = int.Parse(tIn.ReadLine());

        for (int i = 0; i < N; i++)
        {
            string[] line = tIn.ReadLine().Split().ToArray();
            if (line[0] == "C")
            {
                tOut.WriteLine(int.Parse(line[1]) <= maxR ? "YES" : "NO");
            }
            else
            {
                int w = int.Parse(line[1]);
                int h = int.Parse(line[2]);
                int q = Math.Min(w, h);
                int p = Math.Max(w, h);

                string answer = "NO";

                if (p <= A && q <= B)
                {
                    answer = "YES";
                }
                else
                //http://stackoverflow.com/questions/13784274/detect-if-one-rect-can-be-put-into-another-rect
                if (p > A && q <= B)
                {
                    double x = (double)(A + B) * (A + B) / ((p + q) * (p + q)) + (double)(A - B) * (A - B) / ((p - q) * (p - q));
                    if (x >= 2) answer = "YES";
                }

                tOut.WriteLine(answer);
            }
        }

        //Console.ReadLine();
    }
}
