using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
/// <summary>
/// https://www.hackerrank.com/contests/w20/challenges/catcation-rental
/// </summary>
class Solution6
{
    class Interval
    {
        public int L { get; set; }
        public int R { get; set; }
        public int E { get; set; }
        public Interval(int l, int r)
        {
            L = l;
            R = r;
            E = 0;
        }
        public bool Overlap(Interval other)
        {
            return L <= other.R && R >= other.L;
        }
        public bool Adjacent(Interval other)
        {
            return L == other.R + 1 || R + 1 == other.L;
        }
        public Interval Union(Interval other)
        {
            return new Interval(Math.Min(L, other.L), Math.Max(R, other.R)) { E = E + other.E + Math.Max(L, other.L) - Math.Min(R, other.R) - 1 };
        }
    }

    class IntervalComparer : IComparer<Interval>
    {
        public int Compare(Interval x, Interval y)
        {
            return x.L.CompareTo(y.L);
        }
    }

    static void Main(String[] args)
    {
        TextReader tIn = Console.In;
        TextWriter tOut = Console.Out;

//        tIn = new StringReader(@"2 5 2
//3 3
//1 4
//1
//3
//");

        int[] ndk = tIn.ReadLine().Split().Select(p => int.Parse(p)).ToArray();
        int N = ndk[0];
        int D = ndk[1];
        int K = ndk[2];

        List<Interval> booking = new List<Interval>();
        for (int i = 0; i < N; i++)
        {
            int[] lr = tIn.ReadLine().Split().Select(p => int.Parse(p)).ToArray();
            booking.Add(new Interval(lr[0], lr[1]));
        }

        IntervalComparer ic = new IntervalComparer();

        for (int k = 0; k < K; k++)
        {
            int d = int.Parse(tIn.ReadLine());
            List<Interval> rentals = new List<Interval>();

            foreach (Interval book in booking)
            {
                if (book.R - book.L + 1 < d) continue;

                if (rentals.Count == 0)
                {
                    rentals.Add(new Interval(book.L, book.R));
                    continue;
                }

                int index = rentals.BinarySearch(book, ic);
                if (index < 0)
                {
                    index = ~index;
                    if (index == rentals.Count) {
                        if (book.L > rentals[rentals.Count - 1].R)
                        {
                            if (book.L - rentals[rentals.Count - 1].R - 1 < d)
                            {
                                rentals[rentals.Count - 1] = rentals[rentals.Count - 1].Union(book);
                                continue;
                            }
                            rentals.Insert(index, new Interval(book.L, book.R));
                        }
                        continue;
                    }
                    if (index == 0) { 
                        if (book.R < rentals[index].L)
                        {
                            if (rentals[index].L - book.R - 1 < d)
                            {
                                rentals[index] = rentals[index].Union(book);
                                continue;
                            }
                            rentals.Insert(index, new Interval(book.L, book.R));
                        }
                        continue;
                    }

                    if (book.L > rentals[index - 1].R && book.R < rentals[index].L)
                    {
                        if (book.L - rentals[index - 1].R - 1 < d)
                        {
                            rentals[index - 1] = rentals[index - 1].Union(book);
                            if (rentals[index].L - rentals[index - 1].R - 1 < d)
                            {
                                rentals[index - 1] = rentals[index - 1].Union(rentals[index]);
                                rentals.RemoveAt(index);
                                continue;
                            }
                        }

                        if (rentals[index].L - book.R - 1 < d)
                        {
                            rentals[index] = rentals[index].Union(book);
                            continue;
                        }

                        rentals.Insert(index, new Interval(book.L, book.R));
                        continue;
                    }
                }
            }

            tOut.WriteLine(rentals.Select(p => p.R - p.L + 1 - p.E).Sum());
        }
    }
}
