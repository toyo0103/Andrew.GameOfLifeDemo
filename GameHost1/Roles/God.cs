using System;
using GameHost1.Cells;

namespace GameHost1.Roles
{
    public class God
    {
        public God()
        {

        }

        public World CreateWorld(int width, int height)
        {
            Random rnd = new Random();
            int rate = 20;

            var newWorld = new World(width, height);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    var status = (rnd.Next(100) < rate) ? CellStateEnum.Alive : CellStateEnum.Dead;
                    //var cell = new Cell(status, x, y);
                    //newWorld.PutCell(cell, x, y);

                //     newWorld[x, y] = currentCell;

                //     var hasTop = false; 
                //     if(y - 1 >= 0)
                //     {
                //         hasTop = true;
                //         newWorld[x, y - 1].Connect(currentCell);
                //     }

                //     //Stupid
                //     if(x-1 >= 0)
                //     {
                //         newWorld[x-1, y].Connect(currentCell);
                //         if(hasTop) newWorld[x-1, y-1].Connect(currentCell);
                //     }

                //     if(x+1 < newWorld.GetLength(1) && hasTop)
                //     {
                //         newWorld[x+1, y-1].Connect(currentCell);
                //     }
                }
            }

            return newWorld;
        }
    }
}