using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
/// <summary>
/// https://www.hackerrank.com/contests/booking-passions-hacked-backend/challenges/reviews
/// </summary>
class Solution3
{
    static DateTime UnixTimeStampToDateTime(double unixTimeStamp)
    {
        // Unix timestamp is seconds past epoch
        System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
        dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToUniversalTime(); //.ToLocalTime();
        return dtDateTime;
    }
    static void Main(String[] args)
    {
        TextReader tIn = Console.In;
        TextWriter tOut = Console.Out;

        //        tIn = new StringReader(@"3 4
        //Skating
        //Food
        //Climbing
        //1 1467720000
        //Skating is good in Austria
        //22 1464782400
        //I loved the Spanish food, it had so many varieties and it was super super delicious. The price was a little bit high but it was worth it. People who don't like spicy food might need to think twice as it could be a little bit problematic for them.
        //4 1467720000
        //I didn’t like the Indian food!
        //50  1467720000
        //People were really friendly, I enjoyed being there.
        //");


        const int MAX_ID = 1000;

        int[] nm = tIn.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(p => int.Parse(p)).ToArray();
        int N = nm[0];
        int M = nm[1];

        int[,] scores = new int[N, MAX_ID + 1];

        List<string> passions = new List<string>();
        for (int i = 0; i < N; i++)
            passions.Add(tIn.ReadLine().ToLower());

        for (int i = 0; i < M; i++)
        {
            string[] ss = tIn.ReadLine().Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            int id = int.Parse(ss[0]);

            int time = int.Parse(UnixTimeStampToDateTime(double.Parse(ss[1])).ToString("yyyyMMdd"));
            string body = tIn.ReadLine().ToLower();

            int score = 20;
            if (time >= 20160615 && time < 20160715) score += 10;
            if (body.Length >= 100) score += 10;

            for (int j = 0; j < N; j++)
                if (body.Contains(passions[j]))
                    scores[j, id] += score;
        }

        for (int i = 0; i < N; i++)
        {
            int maxScore = 0;
            int bestID = 0;

            for (int j = 0; j < MAX_ID + 1; j++)
                if (scores[i, j] > 0 && (maxScore < scores[i, j] || maxScore == scores[i, j] && j < bestID))
                {
                    maxScore = scores[i, j];
                    bestID = j;
                }

            tOut.WriteLine(maxScore == 0 ? -1 : bestID);
        }

        //tIn.ReadLine();
    }
}
