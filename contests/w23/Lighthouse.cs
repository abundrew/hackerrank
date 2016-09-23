using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
/// <summary>
/// https://www.hackerrank.com/contests/w23/challenges/lighthouse
/// </summary>
class Solution2
{
    static void Main(String[] args)
    {
        TextReader tIn = Console.In;
        TextWriter tOut = Console.Out;

        tIn = new StringReader(@"9
*********
****.****
**.....**
**.....**
*.......*
**.....**
**.....**
****.****
*********
");

        int N = int.Parse(tIn.ReadLine());
        string[] G = new string[N];
        for (int i = 0; i < N; i++) G[i] = tIn.ReadLine();

        int maxR = 0;
        for (int r0 = 1; r0 < N - 1; r0++)
            for (int c0 = 1; c0 < N - 1; c0++)
                if (G[r0][c0] == '.')
                {
                    int span = Math.Min(Math.Min(r0, c0), Math.Min(N - 1 - r0, N - 1 - c0));
                    int R = span;
                    int R2 = R * R;
                    for (int r1 = r0 - span; R > 0 && r1 <= r0 + span; r1++)
                        for (int c1 = c0 - span; R > 0 && c1 <= c0 + span; c1++)
                            if (G[r1][c1] == '*')
                            {
                                int D2 = (r1 - r0) * (r1 - r0) + (c1 - c0) * (c1 - c0);
                                while (R > 0 && R2 >= D2)
                                {
                                    R -= 1;
                                    R2 = R * R;
                                }
                            }
                    if (R > maxR) maxR = R;
                }
        tOut.WriteLine(maxR);

        tIn.ReadLine();
    }
}
