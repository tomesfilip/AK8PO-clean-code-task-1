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

        const char SNAKE_SYMBOL = '■';

        static void Main(string[] args)
        {
            Console.WindowHeight = 16;
            Console.WindowWidth = 32;

            int screenHeight = Console.WindowHeight;
            int screenWidth = Console.WindowWidth;

            Random randomNumber = new Random();

            int score = 5;
            bool isGameOver = false;
            
            head.Position.X = screenWidth / 2;
            head.Position.Y = screenHeight / 2;
            head.Color = ConsoleColor.Red;

            List<int> xPosBody = new List<int>();
            List<int> yPosBody = new List<int>();

            int xPosBerry = randomNumber.Next(0, screenWidth);
            int yPosBerry = randomNumber.Next(0, screenHeight);

            DateTime time1 = DateTime.Now;
            DateTime time2 = DateTime.Now;

            Direction movement = Direction.RIGHT;

            while (true)
            {
                Console.Clear();

                if (head.Position.X == screenWidth - 1 || head.Position.X == 0 || head.Position.Y == screenHeight - 1 || head.Position.Y == 0)
                {
                    isGameOver = true;
                }

                DrawBorder(screenWidth, screenHeight);

                Console.ForegroundColor = ConsoleColor.Green;

                if (xPosBerry == head.Position.X && yPosBerry == head.Position.Y)
                {
                    score++;
                    xPosBerry = randomNumber.Next(1, screenWidth - 2);
                    yPosBerry = randomNumber.Next(1, screenHeight - 2);
                }

                for (int i = 0; i < xPosBody.Count(); i++)
                {
                    Console.SetCursorPosition(xPosBody[i], yPosBody[i]);
                    Console.Write(SNAKE_SYMBOL);

                    if (xPosBody[i] == head.Position.X && yPosBody[i] == head.Position.Y)
                    {
                        isGameOver = true;
                    }
                }

                if (isGameOver)
                {
                    break;
                }

                Console.SetCursorPosition(head.Position.X, head.Position.Y);
                Console.ForegroundColor = head.Color;
                Console.Write(SNAKE_SYMBOL);
                Console.SetCursorPosition(xPosBerry, yPosBerry);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write(SNAKE_SYMBOL);
                time1 = DateTime.Now;

                while (true)
                {
                    time2 = DateTime.Now;

                    if (time2.Subtract(time1).TotalMilliseconds > 500)
                    {
                        break;
                    }

                    HandlePlayerInput(ref movement);
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

                if (xPosBody.Count() > score)
                {
                    xPosBody.RemoveAt(0);
                    yPosBody.RemoveAt(0);
                }
            }

            Console.SetCursorPosition(screenWidth / 5, screenHeight / 2);
            Console.WriteLine("Game over, Score: " + score);
            Console.SetCursorPosition(screenWidth / 5, screenHeight / 2 + 1);
        }

        private static void HandlePlayerInput(ref Direction movement)
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
            for (int i = 0; i < width; i++)
            {
                Console.SetCursorPosition(i, 0);
                Console.Write(SNAKE_SYMBOL);
                Console.SetCursorPosition(i, height - 1);
                Console.Write(SNAKE_SYMBOL);
            }

            for (int i = 0; i < height; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write(SNAKE_SYMBOL);
                Console.SetCursorPosition(width - 1, i);
                Console.Write(SNAKE_SYMBOL);
            }
        }
    }
}