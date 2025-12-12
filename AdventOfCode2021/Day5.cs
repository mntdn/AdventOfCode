using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AdventCode2021
{
    internal class Day5: AoCBase<long>
    {
        public Day5(string fileName) : base(fileName)
        {
        }

        class Coords
        {
            public int x1 { get; set; }
            public int y1 { get; set; }
            public int x2 { get; set; }
            public int y2 { get; set; }
        }

        public override long Part1()
        {
            long result = 0;
            List<Coords> coordList = new List<Coords>();
            foreach (var l in inputContent)
            {
                coordList.Add(new Coords()
                {
                    x1 = int.Parse(l.Split(" -> ")[0].Split(",")[0]),
                    y1 = int.Parse(l.Split(" -> ")[0].Split(",")[1]),
                    x2 = int.Parse(l.Split(" -> ")[1].Split(",")[0]),
                    y2 = int.Parse(l.Split(" -> ")[1].Split(",")[1])
                });
            }
            int width = coordList.Select(a => Math.Max(a.x1, a.x2)).Max() + 1;
            int height = coordList.Select(a => Math.Max(a.y1, a.y2)).Max() + 1;
            int[, ] diagram = new int[width, height];
            foreach (var coord in coordList) 
            {
                if(coord.x1 == coord.x2)
                {
                    for(int i = Math.Min(coord.y1, coord.y2); i <= Math.Max(coord.y1, coord.y2); i++)
                        diagram[coord.x1,i] += 1;
                }
                else if (coord.y1 == coord.y2)
                {
                    for (int i = Math.Min(coord.x1, coord.x2); i <= Math.Max(coord.x1, coord.x2); i++)
                        diagram[i, coord.y1] += 1;
                }
            }
            for (int i = 0; i < width; i++)
            {
                //Console.WriteLine();
                for(int j = 0; j < height; j++)
                {
                    //Console.Write(diagram[j, i] == 0 ? "." : diagram[j, i]);
                    if (diagram[j, i] >= 2)
                        result++;
                }
            }
            return result;
        }

        public override long Part2()
        {
            long result = 0;
            List<Coords> coordList = new List<Coords>();
            foreach (var l in inputContent)
            {
                coordList.Add(new Coords()
                {
                    x1 = int.Parse(l.Split(" -> ")[0].Split(",")[0]),
                    y1 = int.Parse(l.Split(" -> ")[0].Split(",")[1]),
                    x2 = int.Parse(l.Split(" -> ")[1].Split(",")[0]),
                    y2 = int.Parse(l.Split(" -> ")[1].Split(",")[1])
                });
            }
            int width = coordList.Select(a => Math.Max(a.x1, a.x2)).Max() + 1;
            int height = coordList.Select(a => Math.Max(a.y1, a.y2)).Max() + 1;
            int[,] diagram = new int[width, height];
            foreach (var coord in coordList)
            {
                if (coord.x1 == coord.x2)
                {
                    for (int i = Math.Min(coord.y1, coord.y2); i <= Math.Max(coord.y1, coord.y2); i++)
                        diagram[coord.x1, i] += 1;
                }
                else if (coord.y1 == coord.y2)
                {
                    for (int i = Math.Min(coord.x1, coord.x2); i <= Math.Max(coord.x1, coord.x2); i++)
                        diagram[i, coord.y1] += 1;
                }
                else
                {
                    int nb = Math.Max(coord.y1, coord.y2) - Math.Min(coord.y1, coord.y2) + 1;
                    int directionX = coord.x1 - coord.x2 > 0 ? -1 : 1;
                    int directionY = coord.y1 - coord.y2 > 0 ? -1 : 1;
                    int startX = coord.x1;
                    int startY = coord.y1;
                    for(int i = 0; i < nb; i++)
                    {
                        diagram[startX, startY] += 1;
                        startX += directionX;
                        startY += directionY;
                    }
                }
            }
            for (int i = 0; i < width; i++)
            {
                //Console.WriteLine();
                for (int j = 0; j < height; j++)
                {
                    //Console.Write(diagram[j, i] == 0 ? "." : diagram[j, i]);
                    if (diagram[j, i] >= 2)
                        result++;
                }
            }
            return result;
        }
    }
}
