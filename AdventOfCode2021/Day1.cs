using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventCode2021
{
    internal class Day1: AoCBase<long>
    {
        public Day1(string fileName) : base(fileName)
        {
        }

        public override long Part1()
        {
            long result = 0;
            long baseDepth = long.MaxValue;
            foreach (var l in inputContent)
            {
                long currentDepth = long.Parse(l);
                if (currentDepth > baseDepth)
                    result++;
                baseDepth = currentDepth;
            }
            return result;
        }

        public override long Part2()
        {
            long result = 0;
            int skipAmt = 0;
            List<long> depths = new List<long>();
            foreach (var l in inputContent)
            {
                long currentDepth = long.Parse(l);
                depths.Add(currentDepth);
                if(depths.Count() > 3)
                {
                    Console.WriteLine($"{depths.TakeLast(3).Sum()} -- {depths.Skip(skipAmt).Take(3).Sum()}");
                    if(depths.TakeLast(3).Sum() > depths.Skip(skipAmt).Take(3).Sum())
                        result++;
                    skipAmt++;
                }
            }
            return result;
        }
    }
}
