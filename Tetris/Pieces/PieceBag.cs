using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris.Pieces
{
    public static class PieceBag
    {
        public static readonly Piece[] Pieces =
        {
            new Piece(new Space[1, 4]
            {
                { new Space(true, ConsoleColor.Cyan), new Space(true, ConsoleColor.Cyan), new Space(true, ConsoleColor.Cyan), new Space(true, ConsoleColor.Cyan) }
            }, 1, 4),
            new Piece(new Space[2, 3]
            {
                { new Space(true, ConsoleColor.DarkYellow), new Space(false, ConsoleColor.DarkYellow), new Space(false, ConsoleColor.DarkYellow) },
                { new Space(true, ConsoleColor.DarkYellow), new Space(true, ConsoleColor.DarkYellow), new Space(true, ConsoleColor.DarkYellow) }
            }, 2, 3),
            new Piece(new Space[2, 3]
            {
                { new Space(false, ConsoleColor.DarkBlue), new Space(false, ConsoleColor.DarkBlue), new Space(true, ConsoleColor.DarkBlue) },
                { new Space(true, ConsoleColor.DarkBlue), new Space(true, ConsoleColor.DarkBlue), new Space(true, ConsoleColor.DarkBlue) }
            }, 2, 3),
            new Piece(new Space[2, 2]
            {
                { new Space(true, ConsoleColor.Yellow), new Space(true, ConsoleColor.Yellow) },
                { new Space(true, ConsoleColor.Yellow), new Space(true, ConsoleColor.Yellow) }
            }, 2, 2),
            new Piece(new Space[2, 3]
            {
                { new Space(false, ConsoleColor.DarkRed), new Space(true, ConsoleColor.DarkRed), new Space(true, ConsoleColor.DarkRed) },
                { new Space(true, ConsoleColor.DarkRed), new Space(true, ConsoleColor.DarkRed), new Space(false, ConsoleColor.DarkRed) }
            }, 2, 3),
            new Piece(new Space[2, 3]
            {
                { new Space(false, ConsoleColor.DarkMagenta), new Space(true, ConsoleColor.DarkMagenta), new Space(false, ConsoleColor.DarkMagenta) },
                { new Space(true, ConsoleColor.DarkMagenta), new Space(true, ConsoleColor.DarkMagenta), new Space(true, ConsoleColor.DarkMagenta) }
            }, 2, 3),
            new Piece(new Space[2, 3]
            {
                { new Space(true, ConsoleColor.Green), new Space(true, ConsoleColor.Green), new Space(false, ConsoleColor.Green) },
                { new Space(false, ConsoleColor.Green), new Space(true, ConsoleColor.Green), new Space(true, ConsoleColor.Green) }
            }, 2, 3)
        };

        private static List<Piece> _pieces = new List<Piece>();

        public static Piece Next()
        {
            if (_pieces.Count == 0)
                Refill();
            int pieceIndex = Rand.Range(0, _pieces.Count);
            Piece piece = _pieces[pieceIndex];
            _pieces.RemoveAt(pieceIndex);
            return piece;
        }

        private static void Refill()
        {
            _pieces.Clear();
            foreach(Piece piece in Pieces)
            {
                _pieces.Add(piece);
            }
        }
    }
}
