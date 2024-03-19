using System;
using System.Collections.Generic;
using System.Threading;

class Program
{
    static int width = 20;
    static int height = 20;
    static int score = 0;
    static int delay = 100;
    static bool gameover = false;
    static Queue<Tuple<int, int>> snake = new Queue<Tuple<int, int>>();
    static Tuple<int, int> food = new Tuple<int, int>(width / 2, height / 2);
    static Random rnd = new Random();

    static void Main(string[] args)
    {
        Console.CursorVisible = false;
        Console.SetWindowSize(width + 1, height + 1);
        Console.SetBufferSize(width + 1, height + 1);

        ConsoleKeyInfo key = new ConsoleKeyInfo();

        // Initialize snake
        snake.Enqueue(new Tuple<int, int>(width / 2, height / 2));
        Draw();

        while (!gameover)
        {
            if (Console.KeyAvailable)
            {
                key = Console.ReadKey(true);
            }

            switch (key.Key)
            {
                case ConsoleKey.UpArrow:
                    Move(-1, 0);
                    break;
                case ConsoleKey.DownArrow:
                    Move(1, 0);
                    break;
                case ConsoleKey.LeftArrow:
                    Move(0, -1);
                    break;
                case ConsoleKey.RightArrow:
                    Move(0, 1);
                    break;
            }

            if (snake.Contains(food))
            {
                score++;
                food = new Tuple<int, int>(rnd.Next(0, width), rnd.Next(0, height));
            }

            if (snake.Count == width * height)
            {
                Console.Clear();
                Console.WriteLine("You Win!");
                gameover = true;
            }

            Draw();
            Thread.Sleep(delay);
        }

        Console.ReadLine();
    }

    static void Move(int dy, int dx)
    {
        Tuple<int, int> head = snake.Peek();
        Tuple<int, int> newHead = new Tuple<int, int>(head.Item1 + dy, head.Item2 + dx);

        if (newHead.Item1 < 0 || newHead.Item1 >= height ||
            newHead.Item2 < 0 || newHead.Item2 >= width ||
            snake.Contains(newHead))
        {
            Console.Clear();
            Console.WriteLine("Game Over! Score: " + score);
            gameover = true;
        }
        else
        {
            snake.Enqueue(newHead);

            if (newHead.Item1 != food.Item1 || newHead.Item2 != food.Item2)
            {
                Tuple<int, int> tail = snake.Dequeue();
                Console.SetCursorPosition(tail.Item2, tail.Item1);
                Console.Write(" ");
            }
        }
    }

    static void Draw()
    {
        Console.Clear();
        Console.SetCursorPosition(food.Item2, food.Item1);
        Console.Write("*");

        foreach (Tuple<int, int> point in snake)
        {
            Console.SetCursorPosition(point.Item2, point.Item1);
            Console.Write("O");
        }

        Console.SetCursorPosition(0, height);
        Console.Write("Score: " + score);
    }
}

