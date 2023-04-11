using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionLine
{
    internal class DefaultRandomGenerator : IRandomGenerator
    {
        private Random random = new Random();
        public int Next(int minValue, int maxValue)
        {
            return random.Next(minValue, maxValue);
        }
    }
}
