using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
/// <summary>
/// https://www.hackerrank.com/contests/w22/challenges/submask-queries-
/// </summary>
class Solution5
{
    static void Main(String[] args)
    {
        TextReader tIn = Console.In;
        TextWriter tOut = Console.Out;

        //tIn = new StringReader(File.ReadAllText(@"c:\temp\3.txt"));
        //DateTime started = DateTime.Now;

        int[] nm = tIn.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(p => int.Parse(p)).ToArray();
        int M = nm[1];

        int[,] queries = new int[M, 3];
        for (int m = 0; m < M; m++)
        {
            string[] line = tIn.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).ToArray();
            switch (line[0])
            {
                case "1":
                    queries[m, 0] = 1;
                    queries[m, 1] = int.Parse(line[1]);
                    queries[m, 2] = Convert.ToInt32(line[2], 2);
                    break;
                case "2":
                    queries[m, 0] = 2;
                    queries[m, 1] = int.Parse(line[1]);
                    queries[m, 2] = Convert.ToInt32(line[2], 2);
                    break;
                case "3":
                    queries[m, 0] = 3;
                    queries[m, 2] = Convert.ToInt32(line[1], 2);
                    break;
            }
        }

        HashSet<int> values = new HashSet<int>();

        for (int m = M - 1; m >= 0; m--)
        {
            switch (queries[m, 0])
            {
                case 1:
                    List<int> toRemove = new List<int>();
                    foreach (int value in values)
                        if ((queries[m, 2] & queries[value, 2]) == queries[value, 2])
                        {
                            if (queries[value, 1] == -1)
                                queries[value, 1] = queries[m, 1];
                            else
                                queries[value, 1] ^= queries[m, 1];
                            toRemove.Add(value);
                        }
                    foreach (int value in toRemove) values.Remove(value);
                    break;
                case 2:
                    foreach (int value in values)
                        if ((queries[m, 2] & queries[value, 2]) == queries[value, 2])
                        {
                            if (queries[value, 1] == -1)
                                queries[value, 1] = queries[m, 1];
                            else
                                queries[value, 1] ^= queries[m, 1];
                        }
                    break;
                case 3:
                    queries[m, 1] = -1;
                    values.Add(m);
                    break;
            }
        }

        for (int m = 0; m < M; m++)
            if (queries[m, 0] == 3)
            {
                if (values.Contains(m))
                {
                    if (queries[m, 1] == -1)
                        queries[m, 1] = 0;
                    else
                        queries[m, 1] ^= 0;
                }
                tOut.WriteLine(queries[m, 1]);
            }

        //tOut.WriteLine("{0} sec.", DateTime.Now.Subtract(started).TotalSeconds.ToString("0.0"));
        //tIn.ReadLine();
    }
}
