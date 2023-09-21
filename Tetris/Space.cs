using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public struct Space
    {
        public bool Value;
        public ConsoleColor Color;

        public Space(bool Value, ConsoleColor Color)
        {
            this.Value = Value;
            this.Color = Color;
        }
    }
}
