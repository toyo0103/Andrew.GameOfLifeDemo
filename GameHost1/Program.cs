using System;
using System.Threading;
using GameHost1.Cells;
using GameHost1.Roles;

namespace GameHost1
{
    class Program
    {
        static void Main(string[] args)
        {
            var god = new God();
            int height = 50;
            int width = 50;

            var world = god.CreateWorld(height, width);

            
            for (int count = 0; count < 5000; count++)
            {
                int live_count = 0;
                Thread.Sleep(200);

                Console.SetCursorPosition(0, 0);
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        var cellStatus = world.Look(x,y);
                        
                        Console.Write(cellStatus == CellStateEnum.Alive ?  '★' : '☆');
                        if (cellStatus == CellStateEnum.Alive) live_count++;
                    }
                    Console.WriteLine();
                }
                Console.WriteLine($"total lives: {live_count}, round: {count} / 5000...");
            }
        }
    }
}
