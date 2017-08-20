using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
/// <summary>
/// Currencies
/// https://www.hackerrank.com/contests/gs-codesprint/challenges/currencies
/// </summary>
class Currencies
{
    // -------------------------------------------------------------------------
    const int R = 1000000007;
    // -------------------------------------------------------------------------
    public static ulong highestOneBit(ulong i)
    {
        i |= (i >> 1);
        i |= (i >> 2);
        i |= (i >> 4);
        i |= (i >> 8);
        i |= (i >> 16);
        i |= (i >> 32);
        return i - (i >> 1);
    }
    // -------------------------------------------------------------------------
    static void Main(String[] args)
    {
        //----------------------------------------------------------------------
        FIO fio = new FIO();
        //----------------------------------------------------------------------
        int N = fio.ReadInt();
        int X = fio.ReadInt();
        int S = fio.ReadInt();
        int F = fio.ReadInt();
        int M = fio.ReadInt();
        int[][] A = new int[N][];
        for (int i = 0; i < N; i++)
            A[i] = fio.ReadIntArr(N);

        int M2 = (int)Math.Log(1000000000, 2) + 1;

        long[,,] A2 = new long[M2 + 1, N, N];
        double[,,] L2 = new double[M2 + 1, N, N];

        for (int s = 0; s < N; s++)
            for (int f = 0; f < N; f++)
                if (s != f)
                {
                    A2[0, s, f] = A[s][f];
                    L2[0, s, f] = Math.Log(A[s][f]);
                }

        for (int s = 0; s < N; s++)
            for (int f = 0; f < N; f++)
                for (int x = 0; x < N; x++)
                {
                    if (x == s || x == f) continue;
                    double l2 = L2[0, s, x] + L2[0, x, f];
                    if (L2[1, s, f] < l2)
                    {
                        L2[1, s, f] = l2;
                        A2[1, s, f] = (A2[0, s, x] * A2[0, x, f]) % R;
                    }
                }

        for (int m = 2; m <= M2; m++)
            for (int s = 0; s < N; s++)
                for (int f = 0; f < N; f++)
                    for (int x = 0; x < N; x++)
                    {
                        double l2 = L2[m - 1, s, x] + L2[m - 1, x, f];
                        if (L2[m, s, f] < l2)
                        {
                            L2[m, s, f] = l2;
                            A2[m, s, f] = (A2[m - 1, s, x] * A2[m - 1, x, f]) % R;
                        }
                    }

        long[,,] AX = new long[M2 + 1, N, N];
        double[,,] LX = new double[M2 + 1, N, N];

        {
            int H = (int)highestOneBit((ulong)M);
            int h = (int)Math.Log(H, 2);
            for (int s = 0; s < N; s++)
                for (int f = 0; f < N; f++)
                {
                    AX[0, s, f] = A2[h, s, f];
                    LX[0, s, f] = L2[h, s, f];
                }
            M -= H;
        }

        int mx = 0;
        while (M > 0)
        {
            int H = (int)highestOneBit((ulong)M);
            int h = (int)Math.Log(H, 2);
            for (int s = 0; s < N; s++)
                for (int f = 0; f < N; f++)
                    for (int x = 0; x < N; x++)
                    {
                        if (h == 0 && x == f) continue;
                        double lx = LX[mx, s, x] + L2[h, x, f];
                        if (LX[mx + 1, s, f] < lx)
                        {
                            LX[mx + 1, s, f] = lx;
                            AX[mx + 1, s, f] = (AX[mx, s, x] * A2[h, x, f]) % R;
                        }
                    }
            M -= H;
            mx++;
        }

        X = (int)((X * AX[mx, S, F]) % R);
        fio.WriteLine(X.ToString());
        //----------------------------------------------------------------------
        fio.Close();
        //----------------------------------------------------------------------
    }
    // -------------------------------------------------------------------------
    public class FIO : IDisposable
    {
        const int BUFFER_SIZE = 4096;
        byte[] byteBuffer;
        char[] charBuffer;
        int byteBufferSize;
        int charBufferSize;
        int charPos;
        int charLen;
        int byteLen;
        Stream stm = null;
        StreamWriter sw = null;
        public FIO()
        {
            stm = Console.OpenStandardInput();
            sw = new StreamWriter(Console.OpenStandardOutput());
            Init();
        }
        public FIO(Stream stdin)
        {
            stm = stdin;
            sw = new StreamWriter(Console.OpenStandardOutput());
            Init();
        }
        public void Close()
        {
            Dispose();
        }
        public void Dispose()
        {
            try
            {
                stm.Close();
                sw.Dispose();
            }
            finally
            {
                if (stm != null)
                {
                    stm = null;
                    byteBuffer = null;
                    charBuffer = null;
                    charPos = 0;
                    charLen = 0;
                }
            }
        }
        void Init()
        {
            byteBufferSize = BUFFER_SIZE;
            byteBuffer = new byte[byteBufferSize];
            charBufferSize = Encoding.UTF8.GetMaxCharCount(byteBufferSize);
            charBuffer = new char[charBufferSize];
            charPos = 0;
            charLen = 0;
            byteLen = 0;
        }
        int ReadBuffer()
        {
            charLen = 0;
            charPos = 0;
            byteLen = 0;
            do
            {
                byteLen = stm.Read(byteBuffer, 0, byteBuffer.Length);
                if (byteLen == 0) return charLen;
                charLen += Encoding.UTF8.GetChars(byteBuffer, 0, byteLen, charBuffer, charLen);
            } while (charLen == 0);
            return charLen;
        }
        public string ReadToEnd()
        {
            StringBuilder sb = new StringBuilder(charLen - charPos);
            do
            {
                sb.Append(charBuffer, charPos, charLen - charPos);
                charPos = charLen;
                ReadBuffer();
            } while (charLen > 0);
            return sb.ToString();
        }
        public string ReadLine()
        {
            if (charPos == charLen)
            {
                if (ReadBuffer() == 0) return null;
            }
            StringBuilder sb = null;
            do
            {
                int i = charPos;
                do
                {
                    char ch = charBuffer[i];
                    if (ch == '\r' || ch == '\n')
                    {
                        string s;
                        if (sb != null)
                        {
                            sb.Append(charBuffer, charPos, i - charPos);
                            s = sb.ToString();
                        }
                        else
                        {
                            s = new string(charBuffer, charPos, i - charPos);
                        }
                        charPos = i + 1;
                        if (ch == '\r' && (charPos < charLen || ReadBuffer() > 0))
                        {
                            if (charBuffer[charPos] == '\n') charPos++;
                        }
                        return s;
                    }
                    i++;
                } while (i < charLen);
                i = charLen - charPos;
                if (sb == null) sb = new StringBuilder(i + 80);
                sb.Append(charBuffer, charPos, i);
            } while (ReadBuffer() > 0);
            return sb.ToString();
        }
        public string ReadToken()
        {
            if (charPos == charLen)
            {
                if (ReadBuffer() == 0) return null;
            }
            StringBuilder sb = null;
            do
            {
                int i = charPos;
                do
                {
                    char ch = charBuffer[i];
                    if (ch == '\r' || ch == '\n' || ch == ' ')
                    {
                        string s;
                        if (sb != null)
                        {
                            sb.Append(charBuffer, charPos, i - charPos);
                            s = sb.ToString();
                        }
                        else
                        {
                            s = new string(charBuffer, charPos, i - charPos);
                        }
                        charPos = i + 1;
                        if (ch == '\r' && (charPos < charLen || ReadBuffer() > 0))
                        {
                            if (charBuffer[charPos] == '\n' || charBuffer[charPos] == ' ') charPos++;
                        }
                        if (!string.IsNullOrEmpty(s)) return s;
                        i = charPos;
                    }
                    i++;
                } while (i < charLen);
                i = charLen - charPos;
                if (sb == null) sb = new StringBuilder(i + 80);
                sb.Append(charBuffer, charPos, i);
            } while (ReadBuffer() > 0);
            return sb.ToString();
        }
        long ReadLongToken()
        {
            long y = 0;
            bool none = true;
            if (charPos == charLen)
            {
                if (ReadBuffer() == 0) return 0;
            }
            do
            {
                int i = charPos;
                do
                {
                    char ch = charBuffer[i];
                    if (ch == '\r' || ch == '\n' || ch == ' ')
                    {
                        for (int j = 0; j < (i - charPos); j++)
                        {
                            y = y * 10 + (charBuffer[j + charPos] - '0');
                            none = false;
                        }
                        charPos = i + 1;
                        if (ch == '\r' && (charPos < charLen || ReadBuffer() > 0))
                        {
                            if (charBuffer[charPos] == '\n' || charBuffer[charPos] == ' ') charPos++;
                        }
                        if (!none) return y;
                        i = charPos;
                    }
                    i++;
                } while (i < charLen);
                i = charLen - charPos;
                for (int j = 0; j < i; j++)
                {
                    y = y * 10 + (charBuffer[j + charPos] - '0');
                    none = false;
                }
            } while (ReadBuffer() > 0);
            return y;
        }
        public int ReadInt()
        {
            return (int)ReadLongToken();
        }
        public long ReadLong()
        {
            return ReadLongToken();
        }
        public decimal ReadDecimal()
        {
            string s = ReadToken();
            long n = 0;
            int decimalPosition = s.Length;
            for (int k = 0; k < s.Length; k++)
            {
                char c = s[k];
                if (c == '.')
                    decimalPosition = k + 1;
                else
                    n = (n * 10) + (c - '0');
            }
            return new decimal((int)n, (int)(n >> 32), 0, false, (byte)(s.Length - decimalPosition));
        }
        public double ReadDouble()
        {
            return (double)ReadDecimal();
        }
        public int[] ReadIntArr(int n)
        {
            int[] arr = new int[n];
            for (int i = 0; i < n; i++) arr[i] = (int)ReadLongToken();
            return arr;
        }
        public long[] ReadLongArr(int n)
        {
            long[] arr = new long[n];
            for (int i = 0; i < n; i++) arr[i] = ReadLongToken();
            return arr;
        }
        public decimal[] ReadDecimalArr(int n)
        {
            decimal[] arr = new decimal[n];
            for (int i = 0; i < n; i++) arr[i] = ReadDecimal();
            return arr;
        }
        public double[] ReadDoubleArr(int n)
        {
            double[] arr = new double[n];
            for (int i = 0; i < n; i++) arr[i] = ReadDouble();
            return arr;
        }
        public void Write(string value)
        {
            sw.Write(value);
        }
        public void WriteLine(string value)
        {
            sw.WriteLine(value);
        }
    }
    // -------------------------------------------------------------------------
}
