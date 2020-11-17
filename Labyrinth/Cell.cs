﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Labyrinth
{
    public struct Cell
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool IsCell { get; set; }
        public bool IsVisited { get; set; }
        public Cell(int x, int y, bool isVisited = false, bool isCell = true)
        {
            X = x;
            Y = y;
            IsCell = isCell;
            IsVisited = isVisited;
        }
    }
}
