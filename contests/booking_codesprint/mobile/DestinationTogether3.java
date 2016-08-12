import java.io.*;
import java.util.*;
import java.text.*;
import java.math.*;
import java.util.regex.*;

//
// https://www.hackerrank.com/contests/booking-com-passions-hacked-mobile/challenges/destination-together-3-1
//

public class Solution1 {

    public static long Fact(int n)
    {
        if (n == 0) return 1;
        return Fact(n - 1) * n;
    }
    
    public static void main(String[] args) throws IOException {
        BufferedReader in = new BufferedReader(new InputStreamReader(System.in));
        String s = in.readLine();
        String[] ss = s.split("\\s+");
        
        int N = Integer.parseInt(ss[0]) - 1;
        int M = Integer.parseInt(ss[1]) - 1;
        int C = Integer.parseInt(ss[2]) - 1;

        System.out.println(Fact(N - C + M - C + C));
    }
}