using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AdventCode2021
{
    internal class Day3: AoCBase<long>
    {
        public Day3(string fileName) : base(fileName)
        {
        }

        public class NbBits
        {
            public int nb0 { get; set; }
            public int nb1 { get; set; }
        }

        public long stob(string s)
        {
            long result = 0;
            int j = 0;
            for (int i = s.Length - 1; i >= 0; i--)
                result += long.Parse(s[i].ToString()) * (long)Math.Pow(2, j++);
            return result;
        }

        public override long Part1()
        {
            long result = 0;
            List<NbBits> diags = new List<NbBits>();
            foreach (var l in inputContent)
            {
                if(diags.Count == 0)
                    for (int i = 0; i < l.Length; i++)
                        diags.Add(new NbBits() { nb0 = 0, nb1 = 0 });
                var j = 0;
                foreach (var n in l.ToList())
                {
                    if (n == '0')
                        diags[j++].nb0++;
                    else
                        diags[j++].nb1++;
                }
            }
            string gamma = new string(diags.Select(_ => _.nb0 > _.nb1 ? '0' : '1').ToArray());
            string epsilon = new string(diags.Select(_ => _.nb0 < _.nb1 ? '0' : '1').ToArray());
            result = stob(gamma) * stob(epsilon);
            return result;
        }

        public List<NbBits> calcDiags(List<string> inputContent)
        {
            List<NbBits> diags = new List<NbBits>();
            foreach (var l in inputContent)
            {
                if (diags.Count == 0)
                    for (int i = 0; i < l.Length; i++)
                        diags.Add(new NbBits() { nb0 = 0, nb1 = 0 });
                var j = 0;
                foreach (var n in l.ToList())
                {
                    if (n == '0')
                        diags[j++].nb0++;
                    else
                        diags[j++].nb1++;
                }
            }
            return diags;
        }

        public override long Part2()
        {
            long result = 0;
            var oxygenRating = new List<string>(inputContent);
            List<NbBits> diagsOxy = calcDiags(oxygenRating);
            var scrubberRating = new List<string>(inputContent);
            List<NbBits> diagsScrub = calcDiags(scrubberRating);
            string finalO = "";
            string finalS = "";
            for(var i = 0; i < inputContent[0].Length; i++)
            {
                if (oxygenRating.Count > 1)
                    oxygenRating = oxygenRating.Where(o => o[i] == (diagsOxy[i].nb0 == diagsOxy[i].nb1 ? '1' : (diagsOxy[i].nb0 > diagsOxy[i].nb1 ? '0' : '1'))).ToList();
                if (oxygenRating.Count == 1)
                    finalO = oxygenRating[0];
                diagsOxy = calcDiags(oxygenRating);

                if (scrubberRating.Count > 1)
                    scrubberRating = scrubberRating.Where(o => o[i] == (diagsScrub[i].nb0 == diagsScrub[i].nb1 ? '0' : (diagsScrub[i].nb0 < diagsScrub[i].nb1 ? '0' : '1'))).ToList();
                if (scrubberRating.Count == 1)
                    finalS = scrubberRating[0];
                diagsScrub = calcDiags(scrubberRating);
            }
            result = stob(finalO) * stob(finalS);
            return result;
        }
    }
}
