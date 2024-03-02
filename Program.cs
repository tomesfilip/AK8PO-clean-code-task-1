using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Snake
{
    enum Direction
    {
        UP,
        DOWN,
        LEFT,
        RIGHT
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WindowHeight = 16;
            Console.WindowWidth = 32;

            int screenHeight = Console.WindowHeight;
            int screenWidth = Console.WindowWidth;

            Random randomNumber = new Random();
            Pixel head = new Pixel();

            int score = 5;
            bool isGameOver = false;
            
            head.XPos = screenWidth / 2;
            head.YPos = screenHeight / 2;
            head.ScreenColor = ConsoleColor.Red;

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

                if (head.XPos == screenWidth - 1 || head.XPos == 0 || head.YPos == screenHeight - 1 || head.YPos == 0)
                {
                    isGameOver = true;
                }

                DrawBorder(screenWidth, screenHeight);

                Console.ForegroundColor = ConsoleColor.Green;

                if (xPosBerry == head.XPos && yPosBerry == head.YPos)
                {
                    score++;
                    xPosBerry = randomNumber.Next(1, screenWidth - 2);
                    yPosBerry = randomNumber.Next(1, screenHeight - 2);
                }

                for (int i = 0; i < xPosBody.Count(); i++)
                {
                    Console.SetCursorPosition(xPosBody[i], yPosBody[i]);
                    Console.Write("■");

                    if (xPosBody[i] == head.XPos && yPosBody[i] == head.YPos)
                    {
                        isGameOver = true;
                    }
                }

                if (isGameOver)
                {
                    break;
                }

                Console.SetCursorPosition(head.XPos, head.YPos);
                Console.ForegroundColor = head.ScreenColor;
                Console.Write("■");
                Console.SetCursorPosition(xPosBerry, yPosBerry);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("■");
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

                xPosBody.Add(head.XPos);
                yPosBody.Add(head.YPos);

                switch (movement)
                {
                    case Direction.UP:
                        head.YPos--;
                        break;
                    case Direction.DOWN:
                        head.YPos++;
                        break;
                    case Direction.LEFT:
                        head.XPos--;
                        break;
                    case Direction.RIGHT:
                        head.XPos++;
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

        class Pixel
        {
            public int XPos { get; set; }
            public int YPos { get; set; }
            public ConsoleColor ScreenColor { get; set; }
        }

        static void DrawBorder(int width, int height)
        {
            for (int i = 0; i < width; i++)
            {
                Console.SetCursorPosition(i, 0);
                Console.Write("■");
                Console.SetCursorPosition(i, height - 1);
                Console.Write("■");
            }

            for (int i = 0; i < height; i++)
            {
                Console.SetCursorPosition(0, i);
                Console.Write("■");
                Console.SetCursorPosition(width - 1, i);
                Console.Write("■");
            }
        }
    }
}