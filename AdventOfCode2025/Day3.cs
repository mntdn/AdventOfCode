using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventCode2025
{
    internal class Day3: AoCBase<long>
    {
        public Day3(string fileName) : base(fileName)
        {
        }

        public override long Part1()
        {
            long result = 0;
            foreach (var l in inputContent)
            {
                List<int> batteries = l.ToList().Select(b => int.Parse(b.ToString())).ToList();
                int biggest = 0;
                for(int i = 0; i < batteries.Count; i++)
                {
                    for(int j = i+1;  j < batteries.Count; j++)
                    {
                        int res = (batteries[i] * 10) + batteries[j];
                        if (res > biggest)
                            biggest = res;
                    }
                }
                Console.WriteLine($"{l} -- {biggest}");
                result += biggest;
            }
            return result;
        }

        public override long Part2()
        {
            long result = 0;
            foreach (var l in inputContent)
            {
                List<int> batteries = l.ToList().Select(b => int.Parse(b.ToString())).ToList();
                long biggest = 0;
                for (int i = 0; i < batteries.Count - 12; i++)
                {
                    for(int k = i+1; k <= batteries.Count - 12; k++)
                    {
                        int pos = 12;
                        long res = batteries[i] * (long)Math.Pow(10, pos--);
                        for (int j = k; j < k + 12; j++)
                            res += batteries[j] * (long)Math.Pow(10, pos--);

                        if (res > biggest)
                            biggest = res;
                    }
                }
                Console.WriteLine($"{l} -- {biggest}");
                result += biggest;
            }
            return result;
        }
    }
}
