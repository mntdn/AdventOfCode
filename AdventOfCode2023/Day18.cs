using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventCode2023
{
    internal class Day18 : AoCBase<long>
    {
        public Day18(string fileName) : base(fileName)
        {
        }

        public class Instruction
        {
            public Instruction(string direction, int nbSteps, string color)
            {
                Direction = direction;
                NbSteps = nbSteps;
                Color = color;
            }

            public string Direction { get; set; }
            public int NbSteps { get; set; }
            public string Color { get; set; }
        }

        public override long Part1()
        {
            long result = 0;
            var r = new Regex(@"(\w) (\d+) \((.*)\)");
            List<Instruction> instructions = new List<Instruction>();
            foreach (var l in inputContent)
            {
                var m = r.Match(l);
                if(m.Success)
                {
                    string direction = m.Groups[1].Value;
                    int nb = int.Parse(m.Groups[2].Value);
                    string color = m.Groups[3].Value;
                    instructions.Add(new Instruction(direction, nb, color));
                }
            }
            (int, int) minPos = (int.MaxValue, int.MaxValue);
            (int, int) maxPos = (0, 0);
            (int, int) curPos = (0, 0);
            foreach (var instruction in instructions)
            {
                for (int i = 0; i < instruction.NbSteps; i++)
                {
                    maxPos.Item1 = curPos.Item1 > maxPos.Item1 ? curPos.Item1 : maxPos.Item1;
                    maxPos.Item2 = curPos.Item2 > maxPos.Item2 ? curPos.Item2 : maxPos.Item2;
                    minPos.Item1 = curPos.Item1 < minPos.Item1 ? curPos.Item1 : minPos.Item1;
                    minPos.Item2 = curPos.Item2 < minPos.Item2 ? curPos.Item2 : minPos.Item2;
                    switch (instruction.Direction)
                    {
                        case "R":
                            curPos.Item2++;
                            break;
                        case "D":
                            curPos.Item1++;
                            break;
                        case "L":
                            curPos.Item2--;
                            break;
                        case "U":
                            curPos.Item1--;
                            break;
                        default:
                            break;
                    }
                }
            }

            int maxWidth = maxPos.Item1 - minPos.Item1 + 1;
            int maxHeight = maxPos.Item2 - minPos.Item2 + 1;
            string[,] map = new string[maxWidth,maxHeight];
            (int, int) pos = (minPos.Item1 * -1, minPos.Item2 * -1);
            int p = 0;
            foreach (var instruction in instructions)
            {
                for (int i = 0; i < instruction.NbSteps; i++)
                {
                    map[pos.Item1, pos.Item2] = instruction.Color;
                    switch (instruction.Direction)
                    {
                        case "R":
                            pos.Item2++;
                            break;
                        case "D":
                            pos.Item1++;
                            break;
                        case "L":
                            pos.Item2--;
                            break;
                        case "U":
                            pos.Item1--;
                            break;
                        default:
                            break;
                    }
                }
                p++;
            }

            string header = @"<!DOCTYPE html>
<html>
    <head>
        <style>
            .content {
                overflow: scroll;
            }
            br {
                clear: both;
                display: block;
            }
            .block {
                width: 4px;
                height: 4px;
                box-sizing: content-box;
                border: 1px solid white;
                display: inline-block;
                float: left;
            }
            .block.full{
                background-color: blue;
            }
        </style>
    </head>

    <body><div class=""content"">";

            string path = @"c:\temp\map.html";
            if (File.Exists(path))
                File.Delete(path);

            File.AppendAllText(path, header);

            int isFilling = 0;
            bool longWall = false;
            bool longWallEnds = false;
            bool start = false;
            bool end = false;
            for (int i = 0; i < maxWidth; i++)
            {
                string curLine = "";
                string currentBgColor = "";
                for (int j = 0; j < maxHeight; j++)
                {
                    bool isBorder = !string.IsNullOrEmpty(map[i, j]);
                    //if (isFilling == 0 && isBorder)
                    //{
                    //    currentBgColor = map[i, j];
                    //    isFilling++;
                    //}
                    //else if (isFilling >= 1 && isBorder)
                    //{
                    //    if (isFilling == 1 && !longWall)
                    //        longWall = true;
                    //    isFilling++;
                    //    if (isFilling == 2 && !longWall)
                    //    {
                    //        currentBgColor = "";
                    //        isFilling = 0;
                    //        result++;
                    //    }
                    //    else
                    //    {
                    //        if (longWallEnds)
                    //        {
                    //            longWall = false;
                    //            longWallEnds = false;
                    //        }
                    //    }
                    //}
                    //else if (isFilling >= 2 && !isBorder)
                    //{
                    //    if (longWall)
                    //        longWallEnds = true;
                    //    else
                    //    {
                    //        currentBgColor = "";
                    //        isFilling = 0;
                    //    }
                    //}
                    //else if (isFilling == 1 && !isBorder)
                    //    longWall = false;

                    if (isBorder && isFilling == 0 && (!start || !end))
                    {
                        isFilling = 1;
                        currentBgColor = map[i, j];
                        if (!start)
                            start = true;
                        else if (!end)
                            end = true;
                    }

                    if(!isBorder && (start || end))
                    {
                        if (start)
                            start = false;
                        else if (end)
                        {
                            end = false;
                            isFilling = 0;
                            currentBgColor = "";
                        }
                    }

                    result += isFilling > 0 ? 1 : 0;

                    curLine += @$"<div class=""block"" style=""{(isBorder ? @$"border-color:{map[i, j]};" : "")}{(isFilling > 0 ? @$"background-color:{currentBgColor};" : "")}""></div>";
                }
                isFilling = 0;
                start = false;
                end = false;
                longWall = false;
                longWallEnds = false;
                File.AppendAllText(path, curLine + "<br />");
            }
            File.AppendAllText(path, @"    </div></body>
</html>");

            return result;
        }

        public override long Part2()
        {
            throw new NotImplementedException();
        }
    }
}
