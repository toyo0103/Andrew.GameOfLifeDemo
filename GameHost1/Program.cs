using System;
using System.Threading;

using GameHost1.Cells;
namespace GameHost1
{
    class Program
    {
        static void Main(string[] args)
        {
            Cell[,] matrix = new Cell[50, 50];
            Cell[,] area = new Cell[3, 3];

            Init(matrix);

            for (int count = 0; count < 5000; count++)
            {
                int live_count = 0;
                Thread.Sleep(200);

                Console.SetCursorPosition(0, 0);
                for (int y = 0; y < matrix.GetLength(0); y++)
                {
                    for (int x = 0; x < matrix.GetLength(1); x++)
                    {
                        // // clone area
                        // for (int ay = 0; ay < 3; ay++)
                        // {
                        //     for (int ax = 0; ax < 3; ax++)
                        //     {
                        //         int cx = x - 1 + ax;
                        //         int cy = y - 1 + ay;

                        //         if (cx < 0) area[ax, ay] = false;
                        //         else if (cy < 0) area[ax, ay] = false;
                        //         else if (cx >= matrix.GetLength(1)) area[ax, ay] = false;
                        //         else if (cy >= matrix.GetLength(0)) area[ax, ay] = false;
                        //         else area[ax, ay] = matrix[cx, cy];
                        //     }
                        // }
                        // matrix[x, y] = TimePassRule(area);
                        

                        var cellState = matrix[x, y].State;
                        Console.Write(cellState == CellStateEnum.Alive ?  '★' : '☆');
                        if (cellState == CellStateEnum.Alive) live_count++;
                    }
                    Console.WriteLine();
                }
                Console.WriteLine($"total lives: {live_count}, round: {count} / 5000...");
            }
        }


        static void Init(Cell[,] matrix)
        {
            Random rnd = new Random();
            int rate = 20;

            for (int y = 0; y < matrix.GetLength(0); y++)
            {
                for (int x = 0; x < matrix.GetLength(1); x++)
                {
                    var currentCell = new Cell((rnd.Next(100) < rate) ? CellStateEnum.Alive : CellStateEnum.Dead);
                    matrix[x, y] = currentCell;

                    var hasTop = false; 
                    if(y - 1 >= 0)
                    {
                        hasTop = true;
                        //matrix[x, y - 1].Connect(currentCell.CallTunnel,currentCell.Echo);
                        matrix[x, y - 1].Connect(currentCell);
                    }

                    //Stupid
                    if(x-1 >= 0)
                    {
                        //matrix[x-1, y].Connect(currentCell.CallTunnel,currentCell.Echo);
                        matrix[x-1, y].Connect(currentCell);
                        //if(hasTop) matrix[x-1, y-1].Connect(currentCell.CallTunnel,currentCell.Echo);
                        if(hasTop) matrix[x-1, y-1].Connect(currentCell);
                    }

                    if(x+1 < matrix.GetLength(1) && hasTop)
                    {
                        //matrix[x+1, y-1].Connect(currentCell.CallTunnel, currentCell.Echo);
                        matrix[x+1, y-1].Connect(currentCell);
                    }
                }
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="area">must be bool[3, 3]</param>
        /// <returns></returns>
        static bool TimePassRule(bool[,] area)
        {
            var counter = 0;
            var isLive = area[1,1];
            // TODO: fill your code here
            if(area[0,0]) counter++;
            if(area[0,1]) counter++;
            if(area[0,2]) counter++;
            if(area[1,0]) counter++;
            if(area[1,2]) counter++;
            if(area[2,0]) counter++;
            if(area[2,1]) counter++;
            if(area[2,2]) counter++;   


            if(isLive)
            {
                if(counter < 2) return false;
                if(counter == 2 || counter == 3 ) return true;
                if(counter > 3 ) return false;
            }

            return counter == 3;
        }
    }
}
