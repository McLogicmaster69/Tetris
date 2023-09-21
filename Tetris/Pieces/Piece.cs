using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris.Pieces
{
    public struct Piece
    {

        public Space[,] Shape { get; private set; }
        public int SizeX { get; private set; }
        public int SizeY { get; private set; }

        public Piece(Space[,] Shape, int x, int y)
        {

            this.Shape = Shape;

            SizeX = x;
            SizeY = y;

        }

        public Piece RotateAntiClockwise()
        {

            Space[,] ret = new Space[SizeY, SizeX];

            for (int x = 0; x < SizeX; x++)
            {

                for (int y = 0; y < SizeY; y++)
                {

                    ret[y, x] = Shape[SizeX - x - 1, y];

                }

            }

            return new Piece(ret, SizeY, SizeX);

        }

        public Piece RotateClockwise()
        {

            Space[,] ret = new Space[SizeY, SizeX];

            for (int x = 0; x < SizeX; x++)
            {

                for (int y = 0; y < SizeY; y++)
                {

                    ret[y, x] = Shape[x, SizeY - y - 1];

                }

            }

            return new Piece(ret, SizeY, SizeX);

        }

    }

}
