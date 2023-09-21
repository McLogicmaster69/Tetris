using System;
using System.Collections.Generic;
using System.Threading;
using Tetris.Pieces;

namespace Tetris
{
    class Program
    {
        public const int SIZE_X = 20;
        public const int SIZE_Y = 28;
        public const int FRAMES_PER_SECOND = 30;

        private static int _score = 0;
        private static int _level = 0;
        private static int _completedLines;
        private static List<int> _speeds = new List<int>() { 10200, 9800, 9000, 8100, 7100, 6200, 5300, 4200, 3200, 2100, 1900, 1700, 1500, 1300, 1100, 900, 800, 700 };
        private static List<int> _scores = new List<int>() { 0, 100, 300, 500, 800 };

        private static float _timeSinceLastFrame = float.MaxValue;

        private static bool _aPressed = false;
        private static bool _sPressed = false;
        private static bool _dPressed = false;
        private static bool _qPressed = false;
        private static bool _ePressed = false;
        private static bool _xPressed = false;

        private static int _time = 0;
        private static Piece _currentPiece = PieceBag.Next();
        private static int _currentX = SIZE_X / 2 - 1;
        private static int _currentY = 1;
        private static int _levelFlashes = 0;
        private static bool _shown = false;
        private static int _ticksSinceFlash = 0;
        private static DateTime _previousTick;
        private static Grid _grid;
        private static bool _hardDrop = false;

        static void Main(string[] args)
        {
            PrintOpening();
            _grid = SetupGrid();
            _currentPiece = PieceBag.Next();
            _previousTick = DateTime.Now;
            PrintTop();

            while (true)
            {
                _grid.AddPiece(_currentPiece, _currentX, _currentY);
                _grid.RefreshConsole();
                DateTime currentTick = DateTime.Now;
                Update(currentTick - _previousTick);
                _previousTick = currentTick;

                #region old
                /*
                for (; Time < _speeds[_level]; Time++)
                {
                    if (Console.KeyAvailable)
                    {
                        char key = Console.ReadKey().KeyChar;
                        if (key == 'a')
                        {
                            grid.RemovePiece(CurrentPiece, CurrentX, CurrentY);
                            if (grid.WouldReplace(CurrentPiece, CurrentX - 1, CurrentY))
                            {
                                grid.AddPiece(CurrentPiece, CurrentX, CurrentY);
                            }
                            else
                            {
                                CurrentX--;
                                break;
                            }
                        }
                        else if (key == 'd')
                        {
                            grid.RemovePiece(CurrentPiece, CurrentX, CurrentY);
                            if (grid.WouldReplace(CurrentPiece, CurrentX + 1, CurrentY))
                            {
                                grid.AddPiece(CurrentPiece, CurrentX, CurrentY);
                            }
                            else
                            {
                                CurrentX++;
                                break;
                            }
                        }
                        else if (key == 'q')
                        {
                            grid.RemovePiece(CurrentPiece, CurrentX, CurrentY);
                            if (grid.WouldReplace(CurrentPiece.RotateClockwise(), CurrentX, CurrentY))
                            {
                                grid.AddPiece(CurrentPiece, CurrentX, CurrentY);
                            }
                            else
                            {
                                CurrentPiece = CurrentPiece.RotateClockwise();
                                break;
                            }
                        }
                        else if (key == 'e')
                        {
                            grid.RemovePiece(CurrentPiece, CurrentX, CurrentY);
                            if (grid.WouldReplace(CurrentPiece.RotateAntiClockwise(), CurrentX, CurrentY))
                            {
                                grid.AddPiece(CurrentPiece, CurrentX, CurrentY);
                            }
                            else
                            {
                                CurrentPiece = CurrentPiece.RotateAntiClockwise();
                                break;
                            }
                        }
                        else if (key == 's')
                        {
                            Time = 9999999;
                            break;
                        }
                    }
                    TicksSinceFlash += 1;
                    if(TicksSinceFlash > 15)
                    {
                        if (Shown)
                        {
                            Console.SetCursorPosition(SIZE_X - 4, 1);
                            Console.Write("        ");
                            TicksSinceFlash = 0;
                        }
                        else if(levelFlashes > 0)
                        {
                            Console.SetCursorPosition(SIZE_X - 4, 1);
                            Console.Write("LEVEL UP");
                            levelFlashes++;
                            TicksSinceFlash = 0;
                        }
                    }
                    Thread.Sleep(1);
                }
                if (Time >= _speeds[_level])
                {
                    Time = 0;
                    grid.RemovePiece(CurrentPiece, CurrentX, CurrentY);
                    if (grid.WouldReplace(CurrentPiece, CurrentX, CurrentY + 1))
                    {
                        grid.AddPiece(CurrentPiece, CurrentX, CurrentY);
                        int lines = grid.CheckForRows();
                        _score += (_level + 1) * _scores[lines];
                        _completedLines += lines;
                        CurrentX = SIZE_X / 2 - 1;
                        CurrentY = 1;
                        CurrentPiece = PieceBag.Next();

                        if (_completedLines > (_level + 1) * 10 && _level < 20)
                        {
                            _level++;
                            Console.SetCursorPosition(0, 1);
                            Console.Write(_level.ToString());
                        }
                        Console.SetCursorPosition(SIZE_X * 2 - 10, 1);
                        string scr = _score.ToString();
                        for(int i = scr.Length; i < 10; i++)
                        {
                            Console.Write("0");
                        }
                        Console.Write(scr);
                    }
                    else
                    {
                        grid.RemovePiece(CurrentPiece, CurrentX, CurrentY);
                        CurrentY++;
                    }
                }
                */
                #endregion
            }
        }

        private static void PrintOpening()
        {
            Console.Title = "Tetris";
            Console.WriteLine("Welcome to Tetris: Console Edition!");
            Console.WriteLine("Press any key to start");
            Console.ReadKey();
            Console.SetWindowSize(SIZE_X * 2, SIZE_Y + 3);
        }

        private static void PrintTop()
        {
            Console.Clear();
            Console.SetCursorPosition(SIZE_X * 2 - 5, 0);
            Console.Write("SCORE");
            Console.SetCursorPosition(SIZE_X * 2 - 10, 1);
            Console.Write("0000000000");
            Console.SetCursorPosition(0, 0);
            Console.Write("LEVEL");
            Console.SetCursorPosition(0, 1);
            Console.Write(_level.ToString());
        }

        private static Grid SetupGrid()
        {
            Grid grid = new Grid(SIZE_X, SIZE_Y, 0, 3);
            for (int i = 0; i < SIZE_Y; i++)
            {
                grid[0, i] = new Space(true, ConsoleColor.White);
                grid[SIZE_X - 1, i] = new Space(true, ConsoleColor.White);
                grid[1, i] = new Space(true, ConsoleColor.White);
                grid[SIZE_X - 2, i] = new Space(true, ConsoleColor.White);
            }
            for (int i = 0; i < SIZE_X; i++)
            {
                grid[i, SIZE_Y - 1] = new Space(true, ConsoleColor.White);
                grid[i, SIZE_Y - 2] = new Space(true, ConsoleColor.White);
            }
            return grid;
        }

        private static void Update(TimeSpan timeElapsed)
        {
            _timeSinceLastFrame += (float)timeElapsed.TotalMilliseconds;
            CheckInputs();
            if (_timeSinceLastFrame >= 1f / FRAMES_PER_SECOND)
            {
                _timeSinceLastFrame -= 1f / FRAMES_PER_SECOND;
                Tick(new bool[] {
                    _aPressed,
                    _sPressed,
                    _dPressed,
                    _qPressed,
                    _ePressed,
                    _xPressed
                });

                _aPressed = false;
                _sPressed = false;
                _dPressed = false;
                _qPressed = false;
                _ePressed = false;
                _xPressed = false;
            }
        }

        private static void CheckInputs()
        {
            if (Console.KeyAvailable)
            {
                switch (Console.ReadKey().KeyChar)
                {
                    case 'a':
                        _aPressed = true;
                        break;
                    case 's':
                        _sPressed = true;
                        break;
                    case 'd':
                        _dPressed = true;
                        break;
                    case 'q':
                        _qPressed = true;
                        break;
                    case 'e':
                        _ePressed = true;
                        break;
                    case 'x':
                        _xPressed = true;
                        break;
                }
            }
        }

        private static void Tick(bool[] keysPressed)
        {
            _time++;
            if (_hardDrop)
                _time = int.MaxValue;
            else
                HandleTickInputs(keysPressed);
            HandleFlash();
            DropPiece();
        }

        private static void HandleTickInputs(bool[] keysPressed)
        {
            if (keysPressed[0])
            {
                _grid.RemovePiece(_currentPiece, _currentX, _currentY);
                if (_grid.WouldReplace(_currentPiece, _currentX - 1, _currentY))
                {
                    _grid.AddPiece(_currentPiece, _currentX, _currentY);
                }
                else
                {
                    _currentX--;
                    return;
                }
            }
            else if (keysPressed[1])
            {
                _time = int.MaxValue;
                _score += 1;
                UpdateScore();
                return;
            }
            else if (keysPressed[2])
            {
                _grid.RemovePiece(_currentPiece, _currentX, _currentY);
                if (_grid.WouldReplace(_currentPiece, _currentX + 1, _currentY))
                {
                    _grid.AddPiece(_currentPiece, _currentX, _currentY);
                }
                else
                {
                    _currentX++;
                    return;
                }
            }
            else if (keysPressed[3])
            {
                _grid.RemovePiece(_currentPiece, _currentX, _currentY);
                if (_grid.WouldReplace(_currentPiece.RotateClockwise(), _currentX, _currentY))
                {
                    _grid.AddPiece(_currentPiece, _currentX, _currentY);
                }
                else
                {
                    _currentPiece = _currentPiece.RotateClockwise();
                    return;
                }
            }
            else if (keysPressed[4])
            {
                _grid.RemovePiece(_currentPiece, _currentX, _currentY);
                if (_grid.WouldReplace(_currentPiece.RotateAntiClockwise(), _currentX, _currentY))
                {
                    _grid.AddPiece(_currentPiece, _currentX, _currentY);
                }
                else
                {
                    _currentPiece = _currentPiece.RotateAntiClockwise();
                    return;
                }
            }
            else if (keysPressed[5])
            {
                _hardDrop = true;
                return;
            }
        }

        private static void HandleFlash()
        {
            _ticksSinceFlash += 1;
            if (_ticksSinceFlash > 15)
            {
                if (_shown)
                {
                    Console.SetCursorPosition(SIZE_X - 4, 1);
                    Console.Write("        ");
                    _ticksSinceFlash = 0;
                }
                else if (_levelFlashes > 0)
                {
                    Console.SetCursorPosition(SIZE_X - 4, 1);
                    Console.Write("LEVEL UP");
                    _levelFlashes++;
                    _ticksSinceFlash = 0;
                }
            }
        }

        private static void DropPiece()
        {
            if (_time >= _speeds[_level])
            {
                _time = 0;
                _grid.RemovePiece(_currentPiece, _currentX, _currentY);

                if (_hardDrop)
                    _score += 2;

                if (_grid.WouldReplace(_currentPiece, _currentX, _currentY + 1))
                {
                    _grid.AddPiece(_currentPiece, _currentX, _currentY);
                    int lines = _grid.CheckForRows();
                    _score += (_level + 1) * _scores[lines];
                    _completedLines += lines;
                    _currentX = SIZE_X / 2 - 1;
                    _currentY = 1;
                    _currentPiece = PieceBag.Next();
                    _hardDrop = false;

                    CheckLevel();
                    UpdateScore();
                }
                else
                {
                    _grid.RemovePiece(_currentPiece, _currentX, _currentY);
                    _currentY++;
                }
            }
        }

        private static void CheckLevel()
        {
            if (_completedLines > (_level + 1) * 4 && _level < 20)
            {
                _level++;
                Console.SetCursorPosition(0, 1);
                Console.Write(_level.ToString());

                Console.SetCursorPosition(SIZE_X - 4, 1);
                Console.Write("LEVEL UP");
                _shown = true;
            }
        }

        private static void UpdateScore()
        {
            Console.SetCursorPosition(SIZE_X * 2 - 10, 1);
            string scr = _score.ToString();
            for (int i = scr.Length; i < 10; i++)
            {
                Console.Write("0");
            }
            Console.Write(scr);
        }
    }
}
