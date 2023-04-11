using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductionLineTests
{
    internal class FixedRandomGenerator : IRandomGenerator
    {
        private int[] values;
        private int currentIndex = 0;

        public FixedRandomGenerator(params int[] values)
        {
            this.values = values;
        }

        public int Next(int minValue, int maxValue)
        {
            //keep cycling through the given values when asked for next
            int value = values[currentIndex];
            currentIndex = (currentIndex + 1) % values.Length;
            return value;
        }
    }
}
