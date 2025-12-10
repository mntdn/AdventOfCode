using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventCode2025
{
    internal class Day2: AoCBase<long>
    {
        public Day2(string fileName) : base(fileName)
        {
        }

        public override long Part1()
        {
            long result = 0;
            foreach (var l in inputContent)
            {
                foreach(var ranges in l.Split(","))
                {
                    var range = ranges.Split("-");
                    long start = long.Parse(range[0]);
                    long end = long.Parse(range[1]);
                    for(long i = start; i <= end; i++)
                    {
                        var nbString = i.ToString();
                        if (nbString.Length % 2 != 0)
                            continue;
                        var nbList = nbString.ToList().Select(a => a.ToString()).ToList();
                        var q = new string (nbString.ToList().Take(nbString.Length / 2).ToArray());
                        var r = new string(nbString.ToList().TakeLast(nbString.Length / 2).ToArray());
                        if (q == r)
                        {
                            Console.WriteLine($"{i}");
                            result += i;
                        }
                    }
                }

            }
            return result;
        }

        public override long Part2()
        {
            long result = 0;
            foreach (var l in inputContent)
            {
                foreach (var ranges in l.Split(","))
                {
                    var range = ranges.Split("-");
                    long start = long.Parse(range[0]);
                    long end = long.Parse(range[1]);
                    for (long i = start; i <= end; i++)
                    {
                        var nbString = i.ToString();
                        for(int j = 1; j <= nbString.Length / 2; j++)
                        {
                            var split = nbString.Split(new string(nbString.Take(j).ToArray()));
                            if (split.Any(s => s.Length > 0))
                                continue;
                            Console.WriteLine($"{i}");
                            result += i;
                            break;
                        }
                    }
                }

            }
            return result;
        }
    }
}
