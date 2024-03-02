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
        const int WINDOW_WIDTH = 32;
        const int WINDOW_HEIGHT = 16;
        const char PIXEL_SYMBOL = '■';
        const int TICK_DURATION = 400;

        enum Direction
        {
            UP,
            DOWN,
            LEFT,
            RIGHT
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
        static List<int> xPosBody = new List<int>();
        static List<int> yPosBody = new List<int>();

        static Random randomNumber = new Random();
        static Direction movement = Direction.RIGHT;
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
            Console.WindowWidth = WINDOW_WIDTH;
            Console.WindowHeight = WINDOW_HEIGHT;

            head.Position.X = WINDOW_WIDTH / 2;
            head.Position.Y = WINDOW_HEIGHT / 2;
            head.Color = ConsoleColor.Red;

            berry.Position.X = randomNumber.Next(0, WINDOW_WIDTH);
            berry.Position.Y = randomNumber.Next(0, WINDOW_HEIGHT);
            berry.Color = ConsoleColor.Blue;
        }

        static void GameLoop()
        {
            while (!isGameOver)
            {
                HandlePlayerInput();
                UpdateGameState();
                Render();

                Thread.Sleep(TICK_DURATION);
            }
        }

        static void UpdateGameState()
        {
            if (head.Position.X == WINDOW_WIDTH - 1 || head.Position.X == 0 || head.Position.Y == WINDOW_HEIGHT - 1 || head.Position.Y == 0)
            {
                isGameOver = true;
                return;
            }

            if (head.Position.X == berry.Position.X && head.Position.Y == berry.Position.Y)
            {
                score++;
                GenerateBerryPosition();
            }


            xPosBody.Add(head.Position.X);
            yPosBody.Add(head.Position.Y);

            switch (movement)
            {
                case Direction.UP:
                    head.Position.Y--;
                    break;
                case Direction.DOWN:
                    head.Position.Y++;
                    break;
                case Direction.LEFT:
                    head.Position.X--;
                    break;
                case Direction.RIGHT:
                    head.Position.X++;
                    break;
            }

            if (xPosBody.Count > score)
            {
                xPosBody.RemoveAt(0);
                yPosBody.RemoveAt(0);
            }
        }

        static void Render()
        {
            Console.Clear();

            DrawBorder(WINDOW_WIDTH, WINDOW_HEIGHT);
            DrawSnake();
            DrawBerry();
        }

        static void ShowGameOverScreen()
        {
            Console.SetCursorPosition(WINDOW_WIDTH / 5, WINDOW_HEIGHT / 2);
            Console.WriteLine("Game over, Score: " + score);
            Console.SetCursorPosition(WINDOW_WIDTH / 5, WINDOW_HEIGHT / 2 + 1);
        }

        static void HandlePlayerInput()
        {
            if (Console.KeyAvailable)
            {
                ConsoleKey pressedKey = Console.ReadKey(true).Key;

                switch (pressedKey)
                {
                    case ConsoleKey.UpArrow:
                        if (movement != Direction.DOWN)
                            movement = Direction.UP;
                        break;
                    case ConsoleKey.DownArrow:
                        if (movement != Direction.UP)
                            movement = Direction.DOWN;
                        break;
                    case ConsoleKey.LeftArrow:
                        if (movement != Direction.RIGHT)
                            movement = Direction.LEFT;
                        break;
                    case ConsoleKey.RightArrow:
                        if (movement != Direction.LEFT)
                            movement = Direction.RIGHT;
                        break;
                }
            }
        }

        static void DrawBorder(int width, int height)
        {
            Console.ForegroundColor = ConsoleColor.White;

            for (int i = 0; i < width; i++)
            {
                Console.SetCursorPosition(i, 0);
                Console.Write(PIXEL_SYMBOL);
                Console.SetCursorPosition(i, height - 1);
                Console.Write(PIXEL_SYMBOL);
            }

            for (int i = 0; i < height; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write(PIXEL_SYMBOL);
                Console.SetCursorPosition(width - 1, i);
                Console.Write(PIXEL_SYMBOL);
            }
        }

        static void GenerateBerryPosition()
        {
            berry.Position.X = randomNumber.Next(1, WINDOW_WIDTH - 1);
            berry.Position.Y = randomNumber.Next(1, WINDOW_HEIGHT - 1);
        }

        static void DrawBerry()
        {
            Console.SetCursorPosition(berry.Position.X, berry.Position.Y);
            Console.ForegroundColor = berry.Color;
            Console.Write(PIXEL_SYMBOL);
        }

        static void DrawSnake()
        {
            Console.ForegroundColor = ConsoleColor.Green;

            for (int i = 0; i < xPosBody.Count(); i++)
            {
                Console.SetCursorPosition(xPosBody[i], yPosBody[i]);
                Console.Write(PIXEL_SYMBOL);

                if (xPosBody[i] == head.Position.X && yPosBody[i] == head.Position.Y)
                {
                    isGameOver = true;
                    return;
                }
            }

            Console.SetCursorPosition(head.Position.X, head.Position.Y);
            Console.ForegroundColor = head.Color;
            Console.Write(PIXEL_SYMBOL);
        }
    }
}