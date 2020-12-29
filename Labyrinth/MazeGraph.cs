using System;
using System.Collections.Generic;
#nullable enable

namespace Labyrinth3
{
    /// <summary>
    /// Содержит набор методов для создания лабиринта
    /// </summary>
    public class MazeGraph
    {
        private class Cell
        {
            public bool IsVisited { get; set; }
            // public Cell(Cell? up, Cell? down, Cell? left, Cell? right, Cell? back)
            // {
            //     Up = up;
            //     Down = down;
            //     Left = left;
            //     Right = right;
            //     Back = back;
            // }
            public Cell? Up { get; set; }
            public Cell? Down { get; set; }
            public Cell? Left { get; set; }
            public Cell? Right { get; set; }
            public Cell? Back { get; set; }
        }
        private Cell root = new Cell();
        private int _width;
        private int _height;
        private readonly Random _rnd = new Random();

        /// <summary>
        /// Создаём лабиринт
        /// </summary>
        public MazeGraph(int width, int height)
        {
            _width = width;
            _height = height;
            Cell firstLine;
            Cell secondLine = root;
            for (int i = 0; i < _height; i++)
            {
                for (int j = 0; j < _width; j++)
                {
                    firstLine = secondLine;
                    secondLine = firstLine.Down = new Cell();
                    secondLine.Up = firstLine;
                }
            }
            
        }

        private void Initialize(int width, int height, Cell current)
        {

        }

        /// <summary>
        /// Создание лабиринта
        /// </summary>
        /// <param name="width">Ширина лабиринта</param>
        /// <param name="height">Высота лабиринта</param>
        /// <returns></returns>
        public bool[,] Create(int width, int height)
        {
            _isWall[_start.X, _start.Y] = _start;
            while (_path.Count != 0) //пока в стеке есть клетки ищем соседей и строим путь
            {
                _neighbors.Clear();
                GetNeighbors(_path.Peek());
                if (_neighbors.Count != 0)
                {
                    Cell nextCell = ChooseNeighbor(_neighbors);
                    RemoveWall(_path.Peek(), nextCell);
                    nextCell.IsVisited = true; //делаем текущую клетку посещенной
                    _isWall[nextCell.X, nextCell.Y].IsVisited = true; //и в общем массиве
                    _path.Push(nextCell); //затем добавляем её в стек
                }
                else
                {
                    _path.Pop();
                }
            }
        }

        /// <summary>
        /// Получаем соседа текущей клетки
        /// </summary>
        /// <param name="localcell">Текущая ячейка</param>
        private void GetNeighbors(Cell localcell)
        {
            int x = localcell.X;
            int y = localcell.Y;
            const int distance = 2;
            Cell[] possibleNeighbors = new[] // Список всех возможных соседей
            {
                     new Cell(x, y - distance), // Up
                     new Cell(x + distance, y), // Right
                     new Cell(x, y + distance), // Down
                     new Cell(x - distance, y) // Left
                 };
            for (int i = 0; i < 4; i++) // Проверяем все 4 направления
            {
                Cell curNeighbor = possibleNeighbors[i];
                if (curNeighbor.X > 0 && curNeighbor.X < _width && curNeighbor.Y > 0 && curNeighbor.Y < _height)
                {// Если сосед не выходит за стенки лабиринта
                    if (_isWall[curNeighbor.X, curNeighbor.Y].IsCell && !_isWall[curNeighbor.X, curNeighbor.Y].IsVisited)
                    { // А также является клеткой и не посещен
                        _neighbors.Add(curNeighbor);
                    }// добавляем соседа в Лист соседей
                }
            }
        }

        /// <summary>
        /// Выбор случайного соседа
        /// </summary>
        /// <param name="neighbors">Соседи</param>
        /// <returns></returns>
        private Cell ChooseNeighbor(List<Cell> neighbors)
        {
            int r = _rnd.Next(neighbors.Count);
            return neighbors[r];
        }

        /// <summary>
        /// Удаление стены
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        private void RemoveWall(Cell first, Cell second)
        {
            int xDiff = second.X - first.X;
            int yDiff = second.Y - first.Y;
            int addX = (xDiff != 0) ? xDiff / Math.Abs(xDiff) : 0; // Узнаем направление удаления стены
            int addY = (yDiff != 0) ? yDiff / Math.Abs(yDiff) : 0;
            // Координаты удаленной стены
            _isWall[first.X + addX, first.Y + addY].IsCell = true; //обращаем стену в клетку
            _isWall[first.X + addX, first.Y + addY].IsVisited = true; //и делаем ее посещенной
            second.IsVisited = true; //делаем клетку посещенной
            _isWall[second.X, second.Y] = second;
        }

        /// <summary>
        /// Рисуем лабиринт в консоль
        /// </summary>
        public void DrawGrid()
        {
            //Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.CursorVisible = false;
            for (var i = 0; i < _isWall.GetUpperBound(0); i++)
                for (var j = 0; j < _isWall.GetUpperBound(1); j++)
                {
                    Console.SetCursorPosition(i, j);
                    if (_isWall[i, j].IsCell)
                    {
                        Console.Write(" ");
                    }
                    else
                    {
                        //Console.Write((char)2593);
                        Console.Write("\u2593");
                    }
                }
            Console.SetCursorPosition(_start.X, _start.Y - 1);
            Console.Write(" ");
            Console.SetCursorPosition(_finish.X, _finish.Y + 1);
            Console.Write(" ");
        }

    }
}