using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Snake
{
    class Program
    {
        const int WindowWidth = 32;
        const int WindowHeight = 16;
        const char PixelSymbol = '■';
        const int TickDuration = 400;

        enum Direction
        {
            Up,
            Down,
            Left,
            Right
        }

        struct Position
        {
            public int X;
            public int Y;
        }

        struct Pixel
        {
            public Position Position;
            public ConsoleColor Color;
        }

        static Pixel head;
        static Pixel berry;

        static List<Position> body = new List<Position>();

        static Random randomNumber = new Random();
        static Direction movement = Direction.Right;
        static int score = 5;
        static bool isGameOver = false;

        static void Main(string[] args)
        {
            InitializeGame();
            GameLoop();
            ShowGameOverScreen();
        }

        static void InitializeGame()
        {
            Console.WindowWidth = WindowWidth;
            Console.WindowHeight = WindowHeight;

           head = new Pixel
            {
                Position = new Position { X = WindowWidth / 2, Y = WindowHeight / 2 },
                Color = ConsoleColor.Red
            };

            GenerateBerryPosition();
        }

        static void GameLoop()
        {
            while (!isGameOver)
            {
                HandlePlayerInput();
                UpdateGameState();
                Render();

                Thread.Sleep(TickDuration);
            }
        }

        static void UpdateGameState()
        {
            if (head.Position.X == WindowWidth - 1 || head.Position.X == 0 || head.Position.Y == WindowHeight - 1 || head.Position.Y == 0)
            {
                isGameOver = true;
                return;
            }

            if (head.Position.X == berry.Position.X && head.Position.Y == berry.Position.Y)
            {
                score++;
                GenerateBerryPosition();
            }

            body.Add(new Position {  X = head.Position.X, Y = head.Position.Y });

            switch (movement)
            {
                case Direction.Up:
                    head.Position.Y--;
                    break;
                case Direction.Down:
                    head.Position.Y++;
                    break;
                case Direction.Left:
                    head.Position.X--;
                    break;
                case Direction.Right:
                    head.Position.X++;
                    break;
            }

            if (body.Count > score)
            {
                body.RemoveAt(0);
            }

            isGameOver = body.Any(part => part.X == head.Position.X && part.Y == head.Position.Y);
        }

        static void Render()
        {
            Console.Clear();

            DrawBorder(WindowWidth, WindowHeight);
            DrawPixel(berry);
            DrawPixel(head);

            foreach (var part in body)
            {
                DrawPixel(new Pixel { Position = part, Color = ConsoleColor.Green });
            }
        }

        static void HandlePlayerInput()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKey pressedKey = Console.ReadKey(true).Key;

                switch (pressedKey)
                {
                    case ConsoleKey.UpArrow:
                        if (movement != Direction.Down)
                            movement = Direction.Up;
                        break;
                    case ConsoleKey.DownArrow:
                        if (movement != Direction.Up)
                            movement = Direction.Down;
                        break;
                    case ConsoleKey.LeftArrow:
                        if (movement != Direction.Right)
                            movement = Direction.Left;
                        break;
                    case ConsoleKey.RightArrow:
                        if (movement != Direction.Left)
                            movement = Direction.Right;
                        break;
                }
            }
        }

        static void GenerateBerryPosition()
        {
            berry = new Pixel
            {
                Position = new Position { X = randomNumber.Next(1, WindowWidth - 1), Y = randomNumber.Next(1, WindowHeight - 1) },
                Color = ConsoleColor.Blue
            };
        }

        static void ShowGameOverScreen()
        {
            Console.SetCursorPosition(WindowWidth / 5, WindowHeight / 2);
            Console.WriteLine("Game over, Score: " + score);
            Console.SetCursorPosition(WindowWidth / 5, WindowHeight / 2 + 1);
        }

        static void DrawBorder(int width, int height)
        {
            Console.ForegroundColor = ConsoleColor.White;

            for (int i = 0; i < width; i++)
            {
                Console.SetCursorPosition(i, 0);
                Console.Write(PixelSymbol);
                Console.SetCursorPosition(i, height - 1);
                Console.Write(PixelSymbol);
            }

            for (int i = 0; i < height; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write(PixelSymbol);
                Console.SetCursorPosition(width - 1, i);
                Console.Write(PixelSymbol);
            }
        }

        static void DrawPixel(Pixel pixel)
        {
            Console.SetCursorPosition(pixel.Position.X, pixel.Position.Y);
            Console.ForegroundColor = pixel.Color;
            Console.Write(PixelSymbol);
            Console.SetCursorPosition(0, 0);
        }
    }
}