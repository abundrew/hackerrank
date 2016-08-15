using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
/// <summary>
/// https://www.hackerrank.com/contests/w22/challenges/sequential-prefix-function
/// </summary>
class Solution6
{
    static void Main(String[] args)
    {
        TextReader tIn = Console.In;
        TextWriter tOut = Console.Out;

        int Q = int.Parse(tIn.ReadLine());

        int[] S = new int[Q];
        int ix = 0;

        int[] LPS = new int[Q];
        int[] LEN = new int[Q];
        int len = 0;

        for (int q = 0; q < Q; q++)
        {
            string line = tIn.ReadLine();
            switch (line[0])
            {
                case '+':
                    S[ix] = int.Parse(line.Substring(2));

                    if (ix > 0)
                        while (true)
                        {
                            if (S[ix] == S[len])
                            {
                                len++;
                                LPS[ix] = len;
                                break;
                            }
                            else
                            {
                                if (len != 0)
                                {
                                    len = LPS[len - 1];
                                }
                                else
                                {
                                    LPS[ix] = 0;
                                    break;
                                }
                            }
                        }
                    LEN[ix] = len;
                    tOut.WriteLine(LPS[ix]);
                    ix++;
                    break;
                case '-':
                    ix--;
                    len = ix > 0 ? LEN[ix - 1] : 0;
                    tOut.WriteLine(len);
                    break;
            }
        }
    }
}
