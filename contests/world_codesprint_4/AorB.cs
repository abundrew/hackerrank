using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
/// <summary>
/// https://www.hackerrank.com/contests/june-world-codesprint/challenges/aorb
/// </summary>
class Solution3
{
    public static bool[] HexToBits(string hex)
    {
        bool[] bits = new bool[hex.Length * 4];
        int ix = 0;
        foreach (byte b in Enumerable.Range(0, hex.Length)
                         .Where(x => x % 2 == 0)
                         .Select(x => Convert.ToByte(hex.Substring(x, 2), 16)))
        {
            for (int j = 0; j < 8; j++)
                bits[ix++] = (b & (byte)(1 << (7 - j))) > 0;
        }
        return bits;
    }

    public static string BitsToHex(bool[] bits)
    {
        StringBuilder hex = new StringBuilder();
        int len = bits.Length / 8;
        for (int ix = 0; ix < len; ix++)
        {
            byte b = 0;
            for (int j = 0; j < 8; j++)
                if (bits[ix * 8 + j]) b |= (byte)(1 << (7 - j));
            hex.Append(b.ToString("X2"));
        }
        return hex.ToString();
    }

    static void Main(String[] args)
    {
        TextReader tIn = Console.In;
        TextWriter tOut = Console.Out;

        tIn = new StringReader(@"3
16
2B12
9F34
5856
5
B9
40
5A
2
91
BE
A8");


        int Q = int.Parse(tIn.ReadLine());
        for (int q = 0; q < Q; q++)
        {
            int K = int.Parse(tIn.ReadLine());
            string A = tIn.ReadLine().ToUpper().TrimStart('0');
            if (A.Length == 0) A = "0";
            string B = tIn.ReadLine().ToUpper().TrimStart('0');
            if (B.Length == 0) B = "0";
            string C = tIn.ReadLine().ToUpper().TrimStart('0');
            if (C.Length == 0) C = "0";

            int len = Math.Max(A.Length, Math.Max(B.Length, C.Length)) + 1;
            len -= len % 2;

            A = A.PadLeft(len, '0');
            B = B.PadLeft(len, '0');
            C = C.PadLeft(len, '0');

            bool[] a = HexToBits(A);
            bool[] b = HexToBits(B);
            bool[] c = HexToBits(C);

            int k = 0;
            for (int i = 0; i < a.Length; i++)
            {
                if (c[i] && !a[i] && !b[i])
                {
                    b[i] = true;
                    k++;
                } else
                if (!c[i] && (a[i] || b[i]))
                {
                    if (a[i])
                    {
                        a[i] = false;
                        k++;
                    }
                    if (b[i])
                    {
                        b[i] = false;
                        k++;
                    }
                }
            }
            if (k <= K)
            {
                for (int i = 0; i < a.Length; i++)
                {
                    if (k == K) break;

                    if (c[i] && a[i] && b[i])
                    {
                        a[i] = false;
                        k++;
                    }
                    else
                    if (c[i] && a[i] && !b[i] && (k + 1) < K)
                    {
                        a[i] = false;
                        b[i] = true;
                        k += 2;
                    }
                }

                string sa = BitsToHex(a).TrimStart('0');
                string sb = BitsToHex(b).TrimStart('0');

                if (sa.Length == 0) sa = "0";
                if (sb.Length == 0) sb = "0";

                tOut.WriteLine(sa);
                tOut.WriteLine(sb);
            }
            else tOut.WriteLine(-1);
        }

        tIn.ReadLine();
    }
}
