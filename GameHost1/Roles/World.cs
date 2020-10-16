using System;
using System.Collections.Generic;
using GameHost1.Cells;

namespace GameHost1.Roles
{
    public class World
    {
        public readonly int Height;
        public readonly int Width;

        private Cell[,] _map;
        public World(int width, int height)
        {
            this.Height = height;
            this.Width = width;

            _map = new Cell[width, height];
        }

        public void PutCell(Cell cell, int x, int y)
        {
            _map[x, y] = cell;

            var hasTop = false; 
            if(y - 1 >= 0)
            {
                hasTop = true;
                //matrix[x, y - 1].Connect(currentCell.CallTunnel,currentCell.Echo);
                //_map[x, y - 1].Connect(currentCell);
            }

            //Stupid
            if(x-1 >= 0)
            {
                //matrix[x-1, y].Connect(currentCell.CallTunnel,currentCell.Echo);
                //_map[x-1, y].Connect(currentCell);
                //if(hasTop) matrix[x-1, y-1].Connect(currentCell.CallTunnel,currentCell.Echo);
                //if(hasTop) _map[x-1, y-1].Connect(currentCell);
            }

            if(x+1 < _map.GetLength(1) && hasTop)
            {
                //matrix[x+1, y-1].Connect(currentCell.CallTunnel, currentCell.Echo);
                //_map[x+1, y-1].Connect(currentCell);
            }
        }

        public CellStateEnum Look(int x, int y)
        {
            return _map[x, y].State;
        }
    }

    public class Place
    {
        public readonly Cell Cell;
        public void Put(Cell cell)
        {   
            //this.Cell = cell;
        }
    }
}