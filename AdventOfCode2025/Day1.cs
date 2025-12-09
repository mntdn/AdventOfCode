using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventCode2025
{
    internal class Day1: AoCBase<long>
    {
        public Day1(string fileName) : base(fileName)
        {
        }

        public override long Part1()
        {
            long result = 0;
            int start = 50;
            foreach (var l in inputContent)
            {
                bool isLeft = l[0] == 'L';
                int nb = int.Parse(new string(l.ToList().Skip(1).ToArray())) % 100;
                if (isLeft && nb > start)
                    start = 100 - (nb - start);
                else if (isLeft)
                    start -= nb;
                else if (!isLeft && nb + start > 100)
                    start = nb - (100 - start);
                else
                    start += nb;
                start %= 100;
                result += start == 0 ? 1 : 0;
                Console.WriteLine($"{l} -- {start}");
            }
            return result;
        }

        public override long Part2()
        {
            long result = 0;
            int start = 50;
            foreach (var l in inputContent)
            {
                bool isLeft = l[0] == 'L';
                int nb = int.Parse(new string(l.ToList().Skip(1).ToArray())) % 100;
                int reste = int.Parse(new string(l.ToList().Skip(1).ToArray())) / 100;
                if (isLeft && nb > start)
                {
                    if(start > 0) result++;
                    start = 100 - (nb - start);
                }
                else if (isLeft)
                    start -= nb;
                else if (!isLeft && nb + start > 100)
                {
                    result++;
                    start = nb - (100 - start);
                }
                else
                    start += nb;
                start %= 100;
                result += start == 0 ? 1 : 0;
                result += reste;
                Console.WriteLine($"{l} -- {start} -- {result}");
            }
            return result;
        }
    }
}
