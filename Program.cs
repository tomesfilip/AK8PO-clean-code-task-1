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
        static void Main(string[] args)
        {
            Console.WindowHeight = 16;
            Console.WindowWidth = 32;

            int screenHeight = Console.WindowHeight;
            int screenWidth = Console.WindowWidth;

            Random randomNumber = new Random();
            Pixel head = new Pixel();

            int score = 5;
            int gameover = 0;
            
            head.XPos = screenWidth / 2;
            head.YPos = screenHeight / 2;
            head.ScreenColor = ConsoleColor.Red;

            List<int> xPosBody = new List<int>();
            List<int> yPosBody = new List<int>();

            int xPosBerry = randomNumber.Next(0, screenWidth);
            int yPosBerry = randomNumber.Next(0, screenHeight);

            DateTime time1 = DateTime.Now;
            DateTime time2 = DateTime.Now;

            string movement = "RIGHT";
            bool isButtonPressed = false;


            while (true)
            {
                Console.Clear();

                if (head.XPos == screenWidth - 1 || head.XPos == 0 || head.YPos == screenHeight - 1 || head.YPos == 0)
                {
                    gameover = 1;
                }

                for (int i = 0; i < screenWidth; i++)
                {
                    Console.SetCursorPosition(i, 0);
                    Console.Write("■");
                }

                for (int i = 0; i < screenWidth; i++)
                {
                    Console.SetCursorPosition(i, screenHeight - 1);
                    Console.Write("■");
                }

                for (int i = 0; i < screenHeight; i++)
                {
                    Console.SetCursorPosition(0, i);
                    Console.Write("■");
                }

                for (int i = 0; i < screenHeight; i++)
                {
                    Console.SetCursorPosition(screenWidth - 1, i);
                    Console.Write("■");
                }

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
                        gameover = 1;
                    }
                }

                if (gameover == 1)
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
                isButtonPressed = false;

                while (true)
                {
                    time2 = DateTime.Now;
                    if (time2.Subtract(time1).TotalMilliseconds > 500) 
                    { 
                        break; 
                    }

                    if (Console.KeyAvailable)
                    {
                        ConsoleKeyInfo toets = Console.ReadKey(true);
                        if (toets.Key.Equals(ConsoleKey.UpArrow) && movement != "DOWN" && !isButtonPressed)
                        {
                            movement = "UP";
                            isButtonPressed = true;
                        }
                        if (toets.Key.Equals(ConsoleKey.DownArrow) && movement != "UP" && !isButtonPressed)
                        {
                            movement = "DOWN";
                            isButtonPressed = true;
                        }
                        if (toets.Key.Equals(ConsoleKey.LeftArrow) && movement != "RIGHT" && !isButtonPressed)
                        {
                            movement = "LEFT";
                            isButtonPressed = true;
                        }
                        if (toets.Key.Equals(ConsoleKey.RightArrow) && movement != "LEFT" && !isButtonPressed)
                        {
                            movement = "RIGHT";
                            isButtonPressed = true;
                        }
                    }
                }

                xPosBody.Add(head.XPos);
                yPosBody.Add(head.YPos);

                switch (movement)
                {
                    case "UP":
                        head.YPos--;
                        break;
                    case "DOWN":
                        head.YPos++;
                        break;
                    case "LEFT":
                        head.XPos--;
                        break;
                    case "RIGHT":
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

        class Pixel
        {
            public int XPos { get; set; }
            public int YPos { get; set; }
            public ConsoleColor ScreenColor { get; set; }
        }
    }
}