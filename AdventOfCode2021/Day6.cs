using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AdventCode2021
{
    internal class Day6: AoCBase<long>
    {
        public Day6(string fileName) : base(fileName)
        {
        }


        public override long Part1()
        {
            long result = 0;
            int nbDays = 80;
            List<int> state = inputContent[0].Split(",").Select(i => int.Parse(i)).ToList();
            for (int i = 0; i < nbDays; i++)
            {
                //string debut = i == 0 ? "Initial state" : $"After {i.ToString().PadLeft(2)} days";
                //Console.WriteLine($"{debut}: {string.Join(",", state.Select(s => s.ToString()))}");
                int nbToAdd = 0;
                for(int j = 0; j < state.Count; j++)
                {
                    if(state[j] == 0)
                    {
                        state[j] = 6;
                        nbToAdd++;
                    }
                    else
                        state[j] -= 1;
                }
                for(int j = 0; j < nbToAdd; j++)
                    state.Add(8);
            }
            result = state.Count;
            return result;
        }

        public override long Part2()
        {
            long result = 0;
            int nbDays = 256;
            long[] nbFishPerTimer = new long[9];
            inputContent[0].Split(",").Select(i => int.Parse(i)).ToList().ForEach(i => nbFishPerTimer[i]++);
            for(int i = 0; i < nbDays; i++)
            {
                long[] toAdd = new long[9];
                //for (int j = 0; j < 9; j++) toAdd[j] = 0;
                for (int j = 8; j > 0; j--) 
                {
                    if (nbFishPerTimer[j] > 0)
                    {
                        toAdd[j-1] = nbFishPerTimer[j];
                        nbFishPerTimer[j] = 0;
                    }
                }
                if (i > 0)
                {
                    nbFishPerTimer[8] = nbFishPerTimer[0];
                    nbFishPerTimer[6] += nbFishPerTimer[0];
                    nbFishPerTimer[0] = 0;
                }

                for (int j = 8; j >= 0; j--)
                {
                    if(toAdd[j] > 0)
                        nbFishPerTimer[j] += toAdd[j];
                }
            }
            for (int j = 0; j < 9; j++) result += nbFishPerTimer[j];
            return result;
        }
    }
}
