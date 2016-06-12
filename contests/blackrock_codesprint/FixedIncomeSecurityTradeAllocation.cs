using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
/// <summary>
/// https://www.hackerrank.com/contests/blackrock-codesprint/challenges/fixed-income-alloc
/// </summary>
class Solution2
{
    public class PortfolioOrder
    {
        public string ID { get; private set; }
        public long OrderAmount { get; private set; }
        public long AllocatedAmount { get; private set; }

        public PortfolioOrder(string line)
        {
            string[] ss = line.Split().ToArray();
            ID = ss[0];
            OrderAmount = long.Parse(ss[1]);
            AllocatedAmount = 0;
        }

        public void Allocate(long amount)
        {
            AllocatedAmount = amount;
        }
    }

    public class PortfolioOrderComparer : IComparer<PortfolioOrder>
    {
        public int Compare(PortfolioOrder x, PortfolioOrder y)
        {
            int cmp = x.OrderAmount.CompareTo(y.OrderAmount);
            if (cmp == 0) cmp = x.ID.CompareTo(y.ID);
            return cmp;
        }
    }

    public class FISecurity
    {
        public long MinTradeSize { get; private set; }
        public long Increment { get; private set; }
        public long AvailableUnits { get; private set; }

        public FISecurity(string line)
        {
            long[] data = line.Split().Select(p => long.Parse(p)).ToArray();

            MinTradeSize = data[0];
            Increment = data[1];
            AvailableUnits = data[2];
        }

        private long TradeableAmount(long amount)
        {
            return amount < MinTradeSize ? 0 : MinTradeSize + (amount - MinTradeSize) / Increment * Increment;
        }

        private long TradeableAmount(long amount, long orderAmount)
        {
            if (amount < orderAmount)
            {
                if (orderAmount < MinTradeSize * 2 || (orderAmount - MinTradeSize * 2) % Increment != 0) return 0;
                amount = Math.Min(amount, orderAmount - MinTradeSize);
            }
            return TradeableAmount(amount);
        }

        public bool Allocate(PortfolioOrder order, double proportionalAllocation)
        {
            if (AvailableUnits < MinTradeSize) return true;

            if (proportionalAllocation < MinTradeSize)
            {
                if (proportionalAllocation > (double)MinTradeSize / 2)
                {
                    long toAllocate = TradeableAmount(MinTradeSize, order.OrderAmount);
                    if (toAllocate > 0)
                    {
                        order.Allocate(MinTradeSize);
                        AvailableUnits -= MinTradeSize;
                        return true;
                    }
                    return true;
                }
                else return true;
            }
            else
            {
                long toAllocate = TradeableAmount(Math.Min(Math.Min((long)proportionalAllocation, order.OrderAmount), AvailableUnits), order.OrderAmount);
                if (toAllocate > 0)
                {
                    order.Allocate(toAllocate);
                    AvailableUnits -= toAllocate;
                    return true;
                }
                else return true;
            }
        }
    }

    static void Main(String[] args)
    {
        TextReader tIn = Console.In;
        TextWriter tOut = Console.Out;

//        tIn = new StringReader(@"2
//10 2 40
//p1 16
//p2 134");

        int T = int.Parse(tIn.ReadLine());
        FISecurity fiSecurity = new FISecurity(tIn.ReadLine());

        List<PortfolioOrder> orders = new List<PortfolioOrder>();
        for (int t = 0; t < T; t++) orders.Add(new PortfolioOrder(tIn.ReadLine()));

        List<PortfolioOrder> allocatedOrders = new List<PortfolioOrder>();

        orders.Sort(new PortfolioOrderComparer());
        while (orders.Count > 0)
        {
            long totalOrder = orders.Sum(p => p.OrderAmount);
            double proportion = (double)fiSecurity.AvailableUnits / totalOrder;

            bool changes = false;
            int ix = 0;
            while (ix < orders.Count)
            {
                if (fiSecurity.Allocate(orders[ix], orders[ix].OrderAmount * (double)fiSecurity.AvailableUnits / totalOrder))
                {
                    totalOrder -= orders[ix].OrderAmount;
                    allocatedOrders.Add(orders[ix]);
                    orders.RemoveAt(ix);
                    changes = true;
                }
                else
                {
                    ix++;
                }
            }
            if (!changes) break;
        }

        allocatedOrders.AddRange(orders);

        foreach (PortfolioOrder po in allocatedOrders.OrderBy(p => p.ID))
            tOut.WriteLine(string.Format("{0} {1}", po.ID, po.AllocatedAmount));

        //Console.ReadLine();
    }
}
