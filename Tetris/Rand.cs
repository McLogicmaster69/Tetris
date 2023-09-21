using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public static class Rand
    {
        private static Random rand = new Random();

        /// <summary>
        /// Returns a random number between a lower inclusive and upper exclusive
        /// </summary>
        /// <param name="lower"></param>
        /// <param name="upper"></param>
        /// <returns></returns>
        public static int Range(int lower, int upper) => rand.Next(lower, upper);
    }
}
