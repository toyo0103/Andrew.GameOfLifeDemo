﻿using System;
using System.Collections.Generic;
using System.Threading;
using GameHost1.Roles;

namespace GameHost1
{
    public class Program
    {
        
        public static bool TimePassRule(bool[,] area)
        {
            // TODO: fill your code here
            return area[1, 1];
        }



        static void Main(string[] args)
        {
            RunGameOfLife();
        }


        private static void RunGameOfLife()
        { 
            bool[,] matrix = new bool[50, 20];

            Init(matrix, 20);
            for (int count = 0; count < 5000; count++)
            {
                int live_count = 0;

                Thread.Sleep(200);
                Console.SetCursorPosition(0, 0);

                for(int y = 0; y < matrix.GetLength(1); y++)
                {
                    for (int x = 0; x < matrix.GetLength(0); x++)
                    {
                        var c = matrix[x, y];
                        live_count += (c ? 1 : 0);
                        Console.Write(c ? '★' : '☆');
                    }
                    Console.WriteLine();
                }

                matrix = GetNextGenMatrix(matrix);
                Console.WriteLine($"total lives: {live_count}, round: {count} / 5000...");
            }
        }


        public static bool[,] GetNextGenMatrix(bool[,] matrix_current)
        {
            var world = new World();
            world.Init(matrix_current);

            return world.NextGen();
        }

        private static void Init(bool[,] matrix, int rate = 20)
        {
            Random rnd = new Random();
            for (int y = 0; y < matrix.GetLength(1); y++)
            {
                for (int x = 0; x < matrix.GetLength(0); x++)
                {
                    matrix[x, y] = (rnd.Next(100) < rate);
                }
            }
        }
    }
}
