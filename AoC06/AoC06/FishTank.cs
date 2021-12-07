using System.Collections.Generic;
using System.Linq;

namespace AoC06
{
    public class FishTank
    {
        private Dictionary<int, long> _fishByAge = new Dictionary<int, long>
        {
            {0,0},
            {1,0},
            {2,0},
            {3,0},
            {4,0},
            {5,0},
            {6,0},
            {7,0},
            {8,0},
        };
        public long Count => _fishByAge.Sum(entry => entry.Value);

        public FishTank(IEnumerable<int> fish)
        {
            foreach (var fishAge in fish)
            {
                _fishByAge[fishAge]++;
            }
        }

        public void CalculateNextDay()
        {
            _fishByAge = new Dictionary<int, long>
            {
                {0, _fishByAge[1]},
                {1, _fishByAge[2]},
                {2, _fishByAge[3]},
                {3, _fishByAge[4]},
                {4, _fishByAge[5]},
                {5, _fishByAge[6]},
                {6, _fishByAge[7] + _fishByAge[0]},
                {7, _fishByAge[8]},
                {8, _fishByAge[0]},
            };
        }
    }
}