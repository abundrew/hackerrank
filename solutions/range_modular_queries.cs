using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
/// <summary>
/// Range Modular Queries
/// https://www.hackerrank.com/contests/w30/challenges/range-modular-queries
/// </summary>
class Solution5
{
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
    static void Main(String[] args)
    {
        //----------------------------------------------------------------------
        FIO fio = new FIO(Input());
        DateTime started = DateTime.Now;
        //----------------------------------------------------------------------
        const int M = 40001;
        int MB = (int)Math.Sqrt(Math.Log(M, 2) * M) / 5;
        int N = fio.ReadInt();
        int Q = fio.ReadInt();
        int[] A = fio.ReadIntArr(N);
        int[] L = new int[Q];
        int[] R = new int[Q];
        int[] X = new int[Q];
        int[] Y = new int[Q];

        for (int q = 0; q < Q; q++)
        {
            L[q] = fio.ReadInt();
            R[q] = fio.ReadInt();
            X[q] = fio.ReadInt();
            Y[q] = fio.ReadInt();
        }

        int[] NA = new int[M];
        for (int i = 0; i < N; i++)
            NA[A[i]]++;
        int[][] AA = new int[M][];
        for (int i = 0; i < M; i++)
            AA[i] = new int[NA[i]];
        for (int i = 0; i < M; i++)
            NA[i] = 0;
        for (int i = 0; i < N; i++)
            AA[A[i]][NA[A[i]]++] = i;

        int[,] NB = new int[MB, MB - 1];
        for (int i = 0; i < N; i++)
            for (int j = 2; j < MB; j++)
                NB[j, A[i] % j]++;
        int[,][] BB = new int[MB, MB - 1][];
        for (int i = 0; i < MB; i++)
            for (int j = 0; j < MB - 1; j++)
                BB[i, j] = new int[NB[i, j]];
        for (int i = 0; i < MB; i++)
            for (int j = 0; j < MB - 1; j++)
                NB[i, j] = 0;
        for (int i = 0; i < N; i++)
            for (int j = 2; j < MB; j++)
            {
                int x = A[i] % j;
                BB[j, x][NB[j, x]++] = i;
            }

        for (int q = 0; q < Q; q++)
        {
            int K = 0;
            if (X[q] == 1)
            {
                K = R[q] - L[q] + 1;
            }
            else
            if (X[q] < MB)
            {
                if (BB[X[q], Y[q]].Length > 0)
                {
                    int l = Array.BinarySearch(BB[X[q], Y[q]], L[q]);
                    if (l < 0) l = ~l;
                    int r = Array.BinarySearch(BB[X[q], Y[q]], R[q]);
                    if (r < 0) r = ~r - 1;
                    int k = r - l + 1;
                    if (k > 0) K += k;
                }
            }
            else
            {
                int y = Y[q];
                while (y < M)
                {
                    if (AA[y].Length > 0)
                    {
                        int l = Array.BinarySearch(AA[y], L[q]);
                        if (l < 0) l = ~l;
                        int r = Array.BinarySearch(AA[y], R[q]);
                        if (r < 0) r = ~r - 1;
                        int k = r - l + 1;
                        if (k > 0) K += k;
                    }
                    y += X[q];
                }
            }
            fio.WriteLine(K.ToString());
        }
        //----------------------------------------------------------------------
        fio.Close();
        //----------------------------------------------------------------------
        Console.WriteLine();
        Console.WriteLine("Elapsed: {0} ms", (int)DateTime.Now.Subtract(started).TotalMilliseconds);
        Console.ReadLine();
        //----------------------------------------------------------------------
    }
    static Stream Input()
    {
        //----------------------------------------------------------------------
        //        string s = @"5 3
        //250 501 5000 5 4
        //0 4 5 0
        //0 4 10 0
        //0 4 3 2
        //";
        //----------------------------------------------------------------------
        MemoryStream stream = new MemoryStream();
        StreamWriter writer = new StreamWriter(stream);

        int N = 40000;
        int Q = 40000;
        Random random = new Random(100);
        writer.Write(string.Format("{0} {1}\r\n", N, Q));
        for (int i = 0; i < N - 1; i++)
            writer.Write(string.Format("{0} ", random.Next(40001)));
        writer.Write(string.Format("{0}\r\n", random.Next(40001)));
        for (int i = 0; i < Q; i++)
        {
            int L = random.Next(N - 10);
            int R = random.Next(N - L) + L;
            int X = random.Next(500) + 1;
            int Y = random.Next(X);
            writer.Write(string.Format("{0} {1} {2} {3}\r\n", L, R, X, Y));
        }

        //writer.Write(s);

        writer.Flush();
        stream.Position = 0;
        return stream;
        //----------------------------------------------------------------------
        //return File.OpenRead(@"c:\temp\test30.txt");
        //----------------------------------------------------------------------
    }
}
