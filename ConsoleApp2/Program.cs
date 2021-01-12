using System;

namespace ConsoleApp2
{
    class Program
    {
        static int red = 0, blue = 0;
        static int checkwin;
        static int[,] dots = new int[13, 13];
        static int[,] con = new int[13, 13];

        static void print()
        {
            Console.Clear();
            Console.Write("  ");
            for (int i = 1; i < 10; i++)
                Console.Write($"  {i}");
            for (int i = 10; i < 12; i++)
                Console.Write($" {i}");
            Console.Write("\n");
            for (int i = 1; i < 12; i++)
            {
                Console.Write((i < 10 ? $"{i}  |" : $"{i} |"));
                for (int j = 1; j < 12; j++)
                {
                    switch (dots[i, j])
                    {
                        case 0: Console.ResetColor(); break;
                        case 1: Console.ForegroundColor = ConsoleColor.Red; break;
                        case 2: Console.ForegroundColor = ConsoleColor.Blue; break;
                        case 3: Console.ForegroundColor = ConsoleColor.DarkRed; break;
                        case 4: Console.ForegroundColor = ConsoleColor.DarkBlue; break;

                    }

                    Console.Write(" * "); Console.ResetColor();
                }
                Console.Write("\n");
            }

            Console.Write($"\nScore: Red: {red}  Blue: { blue} \n");
        }

        static void setupp()
        {
            for (int i = 0; i <= 12; i++)
                for (int j = 0; j <= 12; j++)
                    dots[i, j] = 0;
            red = 0; blue = 0;
            checkwin = 0;
            print();
        }

        static int contur(int x, int y, int x1, int y1, int x0, int y0, int turn)
        {
            if (x != x0 && y != y0)
            {
                int temp = 0;
                if (dots[x - 1, y - 1] == turn && (x1 != x && y1 != y)) { temp = contur(x - 1, y - 1, x, y, x0, y0, turn); con[x, y] = temp; };
                if (dots[x, y - 1] == turn && (x1 != x && y1 != y)) { temp = contur(x, y - 1, x, y, x0, y0, turn); con[x, y] = temp; };
                if (dots[x + 1, y - 1] == turn && (x1 != x && y1 != y)) { temp = contur(x + 1, y - 1, x, y, x0, y0, turn); con[x, y] = temp; };
                if (dots[x - 1, y] == turn && (x1 != x && y1 != y)) { temp = contur(x - 1, y, x, y, x0, y0, turn); con[x, y] = temp; };
                if (dots[x + 1, y] == turn && (x1 != x && y1 != y)) { temp = contur(x + 1, y, x, y, x0, y0, turn); con[x, y] = temp; };
                if (dots[x - 1, y + 1] == turn && (x1 != x && y1 != y)) { temp = contur(x - 1, y + 1, x, y, x0, y0, turn); con[x, y] = temp; };
                if (dots[x, y + 1] == turn && (x1 != x && y1 != y)) { temp = contur(x, y + 1, x, y, x0, y0, turn); con[x, y] = temp; };
                if (dots[x + 1, y + 1] == turn && (x1 != x && y1 != y)) { temp = contur(x + 1, y + 1, x, y, x0, y0, turn); con[x, y] = temp; };
                return 0;
            }
            else return 1;
        }

        static int Check(int x, int y, int turn)
        {
            for (int i = 0; i <= 12; i++)
                for (int j = 0; j <= 12; j++)
                    con[i, j] = 0;
            if (dots[x, y] != 0) return 0;
            else return turn;

        }
        static void capture(int turn)
        {
            int[] score = new int[13];
            for (int i = 0; i <= 12; i++)
                for (int j = 0; j <= 12; j++)
                    if (con[i, j] == 1) score[i]++;
            for (int i = 0; i <= 12; i++)
                if (score[i] == 2)
                {
                    int temp = 0;
                    int[] dot = new int[2];
                    for (int j = 0; j <= 12; j++)
                        if (con[i, j] == 1)
                        {
                            dot[temp] = j;
                            temp++;
                        }
                    for (int j = dot[0]; j <= dot[1]; j++)
                    {
                        if (turn == 1) { if (dots[i, j] == 2) { dots[i, j] = 3; red++; } }
                        else { if (dots[i, j] == 2) { dots[i, j] = 4; blue++; } }
                    }


                }

        }


        static int logic(int turn)
        {
            int check = 0, x, y;
        turn:

            Console.WriteLine($"\n Turn Player { turn} : ");
            x = int.Parse(Console.ReadLine()); y = int.Parse(Console.ReadLine());
            int temp = Check(x, y, turn);
            if (temp == 0)
            { Console.WriteLine("Error!"); goto turn; }
            dots[x, y] = temp;
            contur(x, y, 0, 0, x, y, turn);

            capture(turn);

            print();
            if ((turn == 1 ? red : blue) == 20) check = turn;
            return check;
        }


        static void Main(string[] args)
        {
            setupp();

            do { checkwin = logic(1); if (checkwin == 0) logic(2); } while (checkwin == 0);
            Console.WriteLine((checkwin == 1 ? "\nPlayer 1 Win! " : "\nPlayer 2 Win! "));
            Console.ReadKey();
        }
    }
}