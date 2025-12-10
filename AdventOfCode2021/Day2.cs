using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventCode2021
{
    internal class Day2: AoCBase<long>
    {
        public Day2(string fileName) : base(fileName)
        {
        }

        public override long Part1()
        {
            long result = 0;
            int position = 0;
            int depth = 0;
            foreach (var l in inputContent)
            {
                string direction = l.Split(' ')[0];
                int amount = int.Parse(l.Split(' ')[1]);
                switch (direction)
                {
                    case "forward":
                        position += amount;
                        break;
                    case "down":
                        depth += amount;
                        break;
                    case "up":
                        depth -= amount;
                        break;
                }
            }
            result = position * depth;
            return result;
        }

        public override long Part2()
        {
            long result = 0;
            int position = 0;
            int depth = 0;
            int aim = 0;
            foreach (var l in inputContent)
            {
                string direction = l.Split(' ')[0];
                int amount = int.Parse(l.Split(' ')[1]);
                switch (direction)
                {
                    case "forward":
                        position += amount;
                        depth += aim * amount;
                        break;
                    case "down":
                        aim += amount;
                        break;
                    case "up":
                        aim -= amount;
                        break;
                }
            }
            result = position * depth;
            return result;
        }
    }
}
