using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AdventCode2021
{
    internal class Day4: AoCBase<long>
    {
        public Day4(string fileName) : base(fileName)
        {
        }

        public class Board
        {
            public (int, bool)[,] Content { get; set; }
            private int CurrentLine;
            public bool hasWon;
            public Board()
            {
                Content = new (int, bool)[5,5];
                CurrentLine = 0;
            }
            public void AddLine(string l)
            {
                l = l.TrimStart().Replace("  ", " ");
                for(var i = 0; i < 5; i++)
                    Content[CurrentLine, i] = (int.Parse(l.Split(' ')[i]), false);
                CurrentLine++;
            }
            public void MarkNumber(int n)
            {
                for (int i = 0; i < 5; i++)
                    for (int j = 0; j < 5; j++)
                        if (Content[i, j].Item1 == n)
                            Content[i, j].Item2 = true;
            }
            public bool IsWinner()
            {
                for (int i = 0; i < 5; i++)
                {
                    int isLine = 0;
                    int isCol = 0;
                    for (int j = 0; j < 5; j++)
                    {
                        isLine += Content[i, j].Item2 ? 1 : 0;
                        isCol += Content[j, i].Item2 ? 1 : 0;
                    }
                    if (isLine == 5 || isCol == 5)
                    {
                        hasWon = true;
                        return true;
                    }
                }
                return false;
            }
            public int GetScore(int n)
            {
                int sumUnmarked = 0;
                for (int i = 0; i < 5; i++)
                    for (int j = 0; j < 5; j++)
                        if (!Content[i, j].Item2)
                            sumUnmarked += Content[i,j].Item1;
                return sumUnmarked * n;
            }
        }

        public override long Part1()
        {
            long result = 0;
            List<int> numbers = new List<int>();
            List<Board> boards = new List<Board>();
            Board currentBoard = null;
            foreach (var l in inputContent) 
            {
                if (numbers.Count == 0)
                {
                    numbers = l.Split(',').Select(_ => int.Parse(_)).ToList();
                    continue;
                }
                if (string.IsNullOrWhiteSpace(l))
                {
                    if (currentBoard != null)
                        boards.Add(currentBoard);
                    currentBoard = new Board();
                }
                else
                    currentBoard.AddLine(l);
            }
            boards.Add(currentBoard);
            bool isFinished = false;
            foreach (var n in numbers)
            {
                foreach (var board in boards)
                {
                    board.MarkNumber(n);
                    if(board.IsWinner())
                    {
                        result = board.GetScore(n);
                        isFinished = true;
                    }
                    if (isFinished) break;
                }
                if (isFinished) break;
            }
            return result;
        }

        public override long Part2()
        {
            long result = 0;
            List<int> numbers = new List<int>();
            List<Board> boards = new List<Board>();
            Board currentBoard = null;
            foreach (var l in inputContent)
            {
                if (numbers.Count == 0)
                {
                    numbers = l.Split(',').Select(_ => int.Parse(_)).ToList();
                    continue;
                }
                if (string.IsNullOrWhiteSpace(l))
                {
                    if (currentBoard != null)
                        boards.Add(currentBoard);
                    currentBoard = new Board();
                }
                else
                    currentBoard.AddLine(l);
            }
            boards.Add(currentBoard);
            bool isFinished = false;
            foreach (var n in numbers)
            {
                foreach (var board in boards)
                {
                    board.MarkNumber(n);
                    if (board.IsWinner() && !boards.Any(b => !b.hasWon))
                    {
                        result = board.GetScore(n);
                        isFinished = true;
                    }
                    if (isFinished) break;
                }
                if (isFinished) break;
            }
            return result;
        }
    }
}
