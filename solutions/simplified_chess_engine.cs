using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
/// <summary>
/// https://www.hackerrank.com/contests/w24/challenges/simplified-chess-engine
/// </summary>
class Solution3
{
    const int SZ = 4;
    static int[][,] position = null;
    static void Copy(int m)
    {
        Buffer.BlockCopy(position[m], 0, position[m + 1], 0, SZ * SZ * 4);
    }
    static readonly Dictionary<char, int> pdic = new Dictionary<char, int>() { { 'N', 1 }, { 'B', 2 },{ 'R', 3 },{ 'Q', 4 } };
    static readonly int[][] pmoves = new int[][] {
        new int[] { 1, 2, 1, -2, 2, 1, 2, -1, -1, 2, -1, -2, -2, 1, -2, -1 },
        new int[] { -1, -1, -1, 1, 1, -1, 1, 1 },
        new int[] { 0, 1, 0, -1, 1, 0, -1, 0 },
        new int[] { 0, 1, 0, -1, 1, 0, -1, 0, -1, -1, -1, 1, 1, -1, 1, 1 }
    };
    static int M = 0;
    static bool OnBoard(int r, int c)
    {
        return (r >= 0 && c >= 0 && r < SZ && c < SZ);
    }
    static IEnumerable<int> Moves(int m, int r0, int c0)
    {
        int sign = (m % 2 == 0) ? 1 : -1;
        int piece = position[m][r0, c0] * sign;
        switch (piece)
        {
            case 1:
                for (int i = 0; i < pmoves[0].Length / 2; i++)
                {
                    int r = r0;
                    int c = c0;
                    r += pmoves[0][i * 2];
                    c += pmoves[0][i * 2 + 1];
                    if (!OnBoard(r, c) || (position[m][r, c] * sign) > 0) continue;
                    yield return r * SZ + c;
                }
                break;
            case 2:
            case 3:
            case 4:
                for (int i = 0; i < pmoves[piece - 1].Length / 2; i++)
                {
                    int r = r0;
                    int c = c0;
                    while (true)
                    {
                        r += pmoves[piece - 1][i * 2];
                        c += pmoves[piece - 1][i * 2 + 1];
                        if (!OnBoard(r, c) || (position[m][r, c] * sign) > 0) break;
                        yield return r * SZ + c;
                        if (position[m][r, c] != 0) break;
                    }
                }
                break;
        }
    }
    static int Game(int m)
    {
        if (m == M) return 0;
        int result = -1;

        int sign = (m % 2 == 0) ? 1 : -1;

        for (int r0 = 0; r0 < SZ; r0++)
            for (int c0 = 0; c0 < SZ; c0++)
                if ((position[m][r0, c0] * sign) > 0)
                {
                    foreach (int move in Moves(m, r0, c0))
                    {
                        int r = move / SZ;
                        int c = move % SZ;
                        if (position[m][r, c] * sign == -4) return 1;
                        if (m + 1 < M)
                        {
                            Copy(m);
                            position[m + 1][r, c] = position[m + 1][r0, c0];
                            position[m + 1][r0, c0] = 0;
                            result = Math.Max(result, -Game(m + 1));
                        } else
                            result = 0;
                    }
                }
        return result;
    }
    static void Main(String[] args)
    {
        TextReader tIn = Console.In;
        TextWriter tOut = Console.Out;

//        tIn = new StringReader(@"1
//2 1 6
//Q D 1
//B C 2
//Q B 4
//");

        tIn = new StringReader(File.ReadAllText(@"c:\temp\test1.txt"));

        int G = int.Parse(tIn.ReadLine());
        for (int g = 0; g < G; g++)
        {
            int[] wbm = tIn.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(p => int.Parse(p)).ToArray();
            int W = wbm[0];
            int B = wbm[1];
            M = wbm[2];

            if (M % 2 == 0) M--;

            position = new int[M + 1][,];
            for (int i = 0; i < M + 1; i++) position[i] = new int[SZ, SZ];

            for (int i = 0; i < W; i++)
            {
                string[] xx = tIn.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToArray();
                position[0][int.Parse(xx[2]) - 1, xx[1].ToUpper()[0] - 'A'] = pdic[xx[0][0]];
            }
            for (int i = 0; i < B; i++)
            {
                string[] xx = tIn.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToArray();
                position[0][int.Parse(xx[2]) - 1, xx[1].ToUpper()[0] - 'A'] = -pdic[xx[0][0]];
            }

            tOut.WriteLine(Game(0) > 0 ? "YES" : "NO");
        }

        tIn.ReadLine();
    }
}
