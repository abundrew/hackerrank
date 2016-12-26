using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
/// <summary>
/// Simplified Chess Engine II
/// https://www.hackerrank.com/contests/w27/challenges/simplified-chess-engine-ii
/// </summary>
class Solution5
{
    const int SZ = 4;
    const int KNIGHT = 1;
    const int BISHOP = 2;
    const int ROOK = 3;
    const int QUEEN = 4;
    const int PAWN = 5;
    static readonly Dictionary<char, int> pdic = new Dictionary<char, int>() { { 'N', KNIGHT }, { 'B', BISHOP }, { 'R', ROOK }, { 'Q', QUEEN }, { 'P', PAWN } };
    static readonly int[][] pmoves = new int[][] {
        new int[] { 1, 2, 1, -2, 2, 1, 2, -1, -1, 2, -1, -2, -2, 1, -2, -1 },
        new int[] { -1, -1, -1, 1, 1, -1, 1, 1 },
        new int[] { 0, 1, 0, -1, 1, 0, -1, 0 },
        new int[] { 0, 1, 0, -1, 1, 0, -1, 0, -1, -1, -1, 1, 1, -1, 1, 1 }
    };
    static int[][,] board = null;
    static int[,] temp = null;
    static bool Valid(int r, int c) { return (r >= 0 && c >= 0 && r < SZ && c < SZ); }
    static int Whose(int[,] board, int r, int c) { if (board[r, c] == 0) return 0; if (board[r, c] > 5) return -1; else return 1; }
    static void Copy(int[,] src, int[,] dst) { Buffer.BlockCopy(src, 0, dst, 0, SZ * SZ * 4); }
    static void Switch(int[,] src, int[,] dst)
    {
        for (int r = 0; r < SZ; r++)
            for (int c = 0; c < SZ; c++)
            {
                dst[r, c] = src[SZ - 1 - r, c];
                if (dst[r, c] > 0)
                    dst[r, c] = (dst[r, c] - 1 + 5) % 10 + 1;
            }
    }
    static long Key(int[,] board, int m)
    {
        int key = 0;
        for (int r = 0; r < SZ; r++)
            for (int c = 0; c < SZ; c++)
                key = key * 11 + board[r, c];
        return key * 10 + m;
    }
    static IEnumerable<int> Moves(int[,] board, int r0, int c0)
    {
        int piece = board[r0, c0];
        switch (piece)
        {
            case KNIGHT:
                for (int i = 0; i < pmoves[piece - 1].Length / 2; i++)
                {
                    int r = r0 + pmoves[0][i * 2];
                    int c = c0 + pmoves[0][i * 2 + 1];
                    if (!Valid(r, c) || Whose(board, r, c) > 0) continue;
                    yield return (r * SZ + c) * 11 + piece;
                }
                break;
            case BISHOP:
            case ROOK:
            case QUEEN:
                for (int i = 0; i < pmoves[piece - 1].Length / 2; i++)
                {
                    int r = r0;
                    int c = c0;
                    while (true)
                    {
                        r += pmoves[piece - 1][i * 2];
                        c += pmoves[piece - 1][i * 2 + 1];
                        if (!Valid(r, c) || Whose(board, r, c) > 0) break;
                        yield return (r * SZ + c) * 11 + piece;
                        if (board[r, c] != 0) break;
                    }
                }
                break;
            case PAWN:
                for (int i = 0; i < 3; i++)
                {
                    int r = r0 - 1;
                    int c = c0 - 1 + i;
                    if (c == c0 && board[r, c] == 0 || c != c0 && Valid(r, c) && Whose(board, r, c) < 0)
                    {
                        yield return (r * SZ + c) * 11 + (r == 0 ? KNIGHT : PAWN);
                        yield return (r * SZ + c) * 11 + (r == 0 ? BISHOP : PAWN);
                        yield return (r * SZ + c) * 11 + (r == 0 ? ROOK : PAWN);
                    }
                }
                break;
        }
    }
    static int M = 0;
    static Dictionary<long, int> dp = null;
    static int Game(int m)
    {
        if (m == M) return 0;
        long key = Key(board[m], m);
        if (dp.ContainsKey(key)) return dp[key];

        int result = -1;

        for (int r0 = 0; r0 < SZ; r0++)
            for (int c0 = 0; c0 < SZ; c0++)
                if (Whose(board[m], r0, c0) > 0)
                {
                    int[] moves = Moves(board[m], r0, c0).ToArray();
                    foreach (int move in moves)
                        if (board[m][(move / 11) / SZ, (move / 11) % SZ] == QUEEN + 5)
                        {
                            dp[key] = 1;
                            return 1;
                        }

                    foreach (int move in moves)
                    {
                        int p = move % 11;
                        int r = (move / 11) / SZ;
                        int c = (move / 11) % SZ;
                        if (m + 1 < M)
                        {
                            Copy(board[m], temp);
                            temp[r, c] = p;
                            temp[r0, c0] = 0;
                            Switch(temp, board[m + 1]);
                            result = Math.Max(result, -Game(m + 1));
                        }
                        else
                            result = 0;

                        if (m % 2 == 1 && result >= 0)
                        {
                            dp[key] = result;
                            return result;
                        }
                    }
                }

        dp[key] = result;
        return result;
    }
    static void Print(int[][,] board)
    {
        string[] sb = new string[SZ];
        for (int r = 0; r < SZ; r++)
        {
            sb[r] = "";
            for (int m = 0; m < board.Length; m++)
            {
                for (int c = 0; c < SZ; c++)
                {
                    switch(board[m][r,c])
                    {
                        case 0:
                            sb[r] += "..";
                            break;
                        case KNIGHT:
                            sb[r] += "WN";
                            break;
                        case BISHOP:
                            sb[r] += "WB";
                            break;
                        case ROOK:
                            sb[r] += "WR";
                            break;
                        case QUEEN:
                            sb[r] += "WQ";
                            break;
                        case PAWN:
                            sb[r] += "WP";
                            break;
                        case KNIGHT + 5:
                            sb[r] += "BN";
                            break;
                        case BISHOP + 5:
                            sb[r] += "BB";
                            break;
                        case ROOK + 5:
                            sb[r] += "BR";
                            break;
                        case QUEEN + 5:
                            sb[r] += "BQ";
                            break;
                        case PAWN + 5:
                            sb[r] += "BP";
                            break;
                    }
                    sb[r] += " ";
                }
                sb[r] += " ";
            }
        }

        foreach (string s in sb)
            Console.WriteLine(s);
        Console.WriteLine();
    }
    static void Main(String[] args)
    {
        TextReader tIn = Console.In;
        TextWriter tOut = Console.Out;

        tIn = new StringReader(@"1
        5 5 6
        Q A 1
        B B 1
        R C 1
        R D 1
        N A 2
        N A 3
        Q A 4
        B B 4
        R C 4
        R D 4");

        dp = new Dictionary<long, int>();

        int G = int.Parse(tIn.ReadLine());
        for (int g = 0; g < G; g++)
        {
            int[] wbm = tIn.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(p => int.Parse(p)).ToArray();
            int W = wbm[0];
            int B = wbm[1];
            M = wbm[2];

            if (M % 2 == 0) M--;

            board = new int[M + 1][,];
            for (int i = 0; i < M + 1; i++) board[i] = new int[SZ, SZ];
            temp = new int[SZ, SZ];

            for (int i = 0; i < W; i++)
            {
                string[] xx = tIn.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToArray();
                board[0][SZ - int.Parse(xx[2]), xx[1].ToUpper()[0] - 'A'] = pdic[xx[0][0]];
            }
            for (int i = 0; i < B; i++)
            {
                string[] xx = tIn.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToArray();
                board[0][SZ - int.Parse(xx[2]), xx[1].ToUpper()[0] - 'A'] = pdic[xx[0][0]] + 5;
            }

            dp.Clear();
            tOut.WriteLine(Game(0) > 0 ? "YES" : "NO");
        }

        tIn.ReadLine();
    }
}
