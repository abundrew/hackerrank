using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
/// <summary>
/// https://www.hackerrank.com/contests/booking-passions-hacked-backend/challenges/a-visit-from-the-north
/// </summary>
class Solution6
{
    class Trip
    {
        public int City { get; set; }
        public int Days { get; set; }
        public int Hours { get; set; }
        public bool[] Visited { get; set; }
        public int Score { get; set; }
        public string Route { get; set; }
    }
    static void Main(String[] args)
    {
        TextReader tIn = Console.In;
        TextWriter tOut = Console.Out;

//        tIn = new StringReader(@"4
//Roma,38
//Florence,10
//Perugia,12
//Ancona,12
//4
//Bevagna,Roma,14
//Bevagna,Perugia,1
//Roma,Florence,4
//Roma,Perugia,2
//");

        int N = int.Parse(tIn.ReadLine()) + 1;
        string[] C = new string[N];
        int[] V = new int[N];

        C[0] = "Bevagna";

        for (int i = 1; i < N; i++)
        {
            string[] ss = tIn.ReadLine().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToArray();
            C[i] = ss[0];
            V[i] = int.Parse(ss[1]);
        }

        Dictionary<int, int>[] E = new Dictionary<int, int>[N];
        for (int i = 0; i < N; i++) E[i] = new Dictionary<int, int>();

        int Ne = int.Parse(tIn.ReadLine());
        for (int i = 0; i < Ne; i++)
        {
            string[] ss = tIn.ReadLine().Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToArray();
            int ix1 = Array.FindIndex(C, p => p.CompareTo(ss[0]) == 0);
            int ix2 = Array.FindIndex(C, p => p.CompareTo(ss[1]) == 0);
            E[ix1][ix2] = int.Parse(ss[2]);
            E[ix2][ix1] = int.Parse(ss[2]);
        }

        int maxScore = 0;
        string bestRoute = "";

        int start = 0;
        bool[] visited = new bool[N];
        visited[start] = true;

        Queue<Trip> que = new Queue<Trip>();
        que.Enqueue(new Trip() { City = start, Days = 6, Hours = 0, Score = 0, Visited = visited, Route = "" });

        while (que.Count > 0)
        {
            Trip trip = que.Dequeue();
            if (trip.Score > maxScore)
            {
                maxScore = trip.Score;
                bestRoute = trip.Route;
            }

            foreach (int e in E[trip.City].Keys)
            {
                if (trip.Visited[e]) continue;
                int h = E[trip.City][e];
                int v = V[e];
                if (h > 16) continue;
                int days = trip.Days;
                int hours = trip.Hours;
                if (h > hours)
                {
                    days--;
                    hours = 16;
                }
                hours -= h;
                while (v > hours)
                {
                    days--;
                    hours += 16;
                }
                hours -= v;
                if (days < 0 || hours < 0) continue;
                bool[] vcopy = new bool[N];
                Array.Copy(trip.Visited, vcopy, N);
                vcopy[e] = true;

                string route = trip.Route + (!string.IsNullOrEmpty(trip.Route) ? "," : "") + C[e];

                que.Enqueue(new Trip() { City = e, Days = days, Hours = hours, Score = trip.Score + 1, Visited = vcopy, Route = route });
            }

        }

        if (maxScore > 0)
        {
            foreach (string route in bestRoute.Split(',')) tOut.WriteLine(route);
        }
        else
        {
            tOut.WriteLine("NONE");
        }

        //tIn.ReadLine();
    }
}
