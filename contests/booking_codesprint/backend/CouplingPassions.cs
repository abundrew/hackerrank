using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
/// <summary>
/// https://www.hackerrank.com/contests/booking-passions-hacked-backend/challenges/a-couple-and-their-passions
/// </summary>
class Solution2
{
    static double distance_between(double lat1, double long1, double lat2, double long2)
    {
        double EARTH_RADIUS = 6371;
        double PI = 3.14159265359;

        double point1_lat_in_radians = lat1 * PI / 180;
        double point2_lat_in_radians = lat2 * PI / 180;
        double point1_long_in_radians = long1 * PI / 180;
        double point2_long_in_radians = long2 * PI / 180;

        return Math.Acos(Math.Sin(point1_lat_in_radians) * Math.Sin(point2_lat_in_radians) +
                     Math.Cos(point1_lat_in_radians) * Math.Cos(point2_lat_in_radians) *
                     Math.Cos(point2_long_in_radians - point1_long_in_radians)) * EARTH_RADIUS;
    }
    class Destination
    {
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string[] Covered { get; set; }
    }
    static void Main(String[] args)
    {
        TextReader tIn = Console.In;
        TextWriter tOut = Console.Out;

//        tIn = new StringReader(@"2
//3 surfing yoga walking
//3 wine relaxation beach
//3
//amsterdam 52.374030 4.889690 4 museums canals nightlife walking
//sagres 37.129665 -8.669586 3 beach surfing relaxation
//biarritz 43.480120 -1.555580 6 surfing nightlife beach food wine walking");

        Dictionary<string, int> groupPassions = new Dictionary<string, int>();

        int N = int.Parse(tIn.ReadLine());
        for (int i = 0; i < N; i++)
        {
            string[] ss = tIn.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            int m = int.Parse(ss[0]);
            for (int j = 0; j < m; j++)
            {
                string passion = ss[j + 1].ToLower();
                if (!groupPassions.ContainsKey(passion)) groupPassions[passion] = 0;
                groupPassions[passion]++;
            }
        }

        int Y = int.Parse(tIn.ReadLine());
        Destination[] destinations = new Destination[Y];
        for (int i = 0; i < Y; i++)
        {
            string[] ss = tIn.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            destinations[i] = new Destination();
            destinations[i].Name = ss[0];
            destinations[i].Latitude = double.Parse(ss[1]);
            destinations[i].Longitude = double.Parse(ss[2]);
            int k = int.Parse(ss[3]);
            HashSet<string> passions = new HashSet<string>();
            for (int j = 0; j < k; j++) {
                string passion = ss[j + 4].ToLower();
                if (groupPassions.ContainsKey(passion)) passions.Add(passion);
            }
            destinations[i].Covered = passions.ToArray();
        }

        int maxCovered = -1;
        double minDistance = double.MaxValue;
        string[] result = null;

        for (int i = 0; i < Y - 1; i++)
            for (int j = i + 1; j < Y; j++)
            {
                int covered = destinations[i].Covered.Union(destinations[j].Covered).Count();

                double distance = distance_between(destinations[i].Latitude, destinations[i].Longitude, destinations[j].Latitude, destinations[j].Longitude);
                if (covered < maxCovered) continue;
                if (covered > maxCovered || covered == maxCovered && distance < minDistance)
                {
                    result = (new string[] { destinations[i].Name, destinations[j].Name }).OrderBy(p => p).ToArray();
                    maxCovered = covered;
                    minDistance = distance;
                }
            }

        tOut.WriteLine(string.Join(" ", result));

        //tIn.ReadLine();
    }
}
