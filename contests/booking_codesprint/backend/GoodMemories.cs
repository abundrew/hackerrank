using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
/// <summary>
/// https://www.hackerrank.com/contests/booking-passions-hacked-backend/challenges/good-memories
/// </summary>
class Solution4
{
    static void Main(String[] args)
    {
        TextReader tIn = Console.In;
        TextWriter tOut = Console.Out;

//        tIn = new StringReader(@"2
//3
//Red square,Colosseum
//Louvre,Red square
//Louvre
//3
//Sacre Coeur,The Hermitage
//Stonehenge,Versailles,Louvre
//Louvre,Stonehenge
//");

        int X = int.Parse(tIn.ReadLine());
        for (int x = 0; x < X; x++)
        {
            int N = int.Parse(tIn.ReadLine());
            string[][] S = new string[N][];
            HashSet<string> H = new HashSet<string>();
            for (int i = 0; i < N; i++) {
                S[i] = tIn.ReadLine().ToLower().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToArray();
                foreach (string a in S[i]) H.Add(a);
            }
            Dictionary<string, int> A = new Dictionary<string, int>();
            int K = 0;
            foreach (string a in H)
            {
                A[a] = K;
                K++;
            }
            int[,] M = new int[K, K];
            bool ok = true;
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < S[i].Length - 1; j++)
                {
                    for (int k = j + 1; k < S[i].Length; k++)
                    {
                        int aj = A[S[i][j]];
                        int ak = A[S[i][k]];
                        if (M[aj, ak] == 1 || M[ak, aj] == -1)
                        {
                            ok = false;
                            break;
                        }
                        M[aj, ak] = -1;
                        M[ak, aj] = 1;
                    }

                    if (!ok) break;
                }
                if (!ok) break;
            }

            tOut.WriteLine(ok ? "ORDER EXISTS" : "ORDER VIOLATION");
        }

        //tIn.ReadLine();
    }
}
