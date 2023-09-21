using System;
using Tetris.Pieces;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace Tetris
{
    struct Grid
    {
        public Space[,] grid { get; private set; }
        public Space[,] OldGrid { get; private set; }
        public int SizeX { get; private set; }
        public int SizeY { get; private set; }
        public int XOffset { get; private set; }
        public int YOffset { get; private set; }
        public Space this[int x, int y]
        {
            get
            {
                return grid[x, y];
            }
            set
            {
                grid[x, y] = value;
            }
        }
        public Grid(int x, int y, int xoff, int yoff)
        {
            grid = new Space[x, y];
            OldGrid = new Space[x, y];
            SizeX = x;
            SizeY = y;
            XOffset = xoff;
            YOffset = yoff;
        }
        public Grid(int x, int y, int xoff, int yoff, Space[,] shape)
        {
            grid = shape;
            OldGrid = shape;
            SizeX = x;
            SizeY = y;
            XOffset = xoff;
            YOffset = yoff;
        }
        public void PrintGrid()
        {
            for (int y = 0; y < SizeY; y++)
            {
                for (int x = 0; x < SizeX; x++)
                {
                    if (grid[x, y].Value != OldGrid[x, y].Value)
                    {
                        if (grid[x, y].Value)
                        {
                            Console.BackgroundColor = grid[x, y].Color;
                        }
                        else
                        {
                            Console.BackgroundColor = ConsoleColor.Black;
                        }
                        Console.SetCursorPosition((x + XOffset) * 2, y + YOffset);
                        Console.Write("  ");
                    }
                }
            }
            Console.BackgroundColor = ConsoleColor.Black;
        }
        public void RefreshConsole()
        {
            PrintGrid();
            OldGrid = (Space[,])grid.Clone();
            Console.SetCursorPosition(0, 2);
        }
        public void AddPiece(Piece piece, int x, int y)
        {
            for (int py = 0; py < piece.SizeY; py++)
            {
                for (int px = 0; px < piece.SizeX; px++)
                {
                    if (piece.Shape[px, py].Value)
                    {
                        grid[x + px, y + py] = piece.Shape[px, py];
                    }
                }
            }
        }
        public void RemovePiece(Piece piece, int x, int y)
        {
            for (int py = 0; py < piece.SizeY; py++)
            {
                for (int px = 0; px < piece.SizeX; px++)
                {
                    if (piece.Shape[px, py].Value)
                    {
                        grid[x + px, y + py].Value = false;
                    }
                }
            }
        }
        public int CheckForRows()
        {
            int completes = 0;
            for(int y = 0; y < SizeY - 2; y++)
            {
                bool Completed = true;
                for(int x = 2; x < SizeX - 2; x++)
                {
                    if(!grid[x, y].Value)
                    {
                        Completed = false;
                        break;
                    }
                }
                if (Completed)
                {
                    completes++;
                    for (int ay = y; ay > 0; ay--)
                    {
                        for (int x = 2; x < SizeX - 2; x++)
                        {
                            grid[x, ay] = grid[x, ay - 1];
                        }
                    }
                    y--;
                }
            }
            return completes;
        }
        public bool WouldReplace(Piece piece, int x, int y)
        {
            for (int py = 0; py < piece.SizeY; py++)
            {
                for (int px = 0; px < piece.SizeX; px++)
                {
                    if (piece.Shape[px, py].Value)
                    {
                        if (grid[x + px, y + py].Value)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
    }
}
