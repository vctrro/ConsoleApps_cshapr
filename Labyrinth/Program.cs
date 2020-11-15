using System;
using System.Collections.Generic;

while (Console.ReadLine() != "q")
{
    Maze lab = new Maze(40, 40);
    lab.DrawGrid();
}

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

class Maze
{
    public readonly Cell[,] _cells;
    private int _width;
    private int _height;
    public Stack<Cell> _path = new Stack<Cell>();
    public List<Cell> _neighbors = new List<Cell>();
    public Random _rnd = new Random();
    public Cell _start;
    public Cell _finish;
    public Maze(int width, int height)
    {
        _start = new Cell(1, 1, true, true);
        _finish = new Cell(width - 3, height - 3, true, true);
        _width = width;
        _height = height;
        _cells = new Cell[width, height];
        for (var i = 0; i < width; i++)
            for (var j = 0; j < height; j++)
                if ((i % 2 != 0 && j % 2 != 0) && (i < _width - 1 && j < _height - 1)) //если ячейка нечетная по х и по у и не выходит за пределы лабиринта
                {
                    _cells[i, j] = new Cell(i, j); //то это клетка (по умолчанию)
                }
                else
                {
                    _cells[i, j] = new Cell(i, j, false, false);
                }
        _path.Push(_start);
        _cells[_start.X, _start.Y] = _start;
        CreateMaze();
    }

    /// <summary>
    /// Создание лабиринта
    /// </summary>
    public void CreateMaze()
    {
        _cells[_start.X, _start.Y] = _start;
        while (_path.Count != 0) //пока в стеке есть клетки ищем соседей и строим путь
        {
            _neighbors.Clear();
            GetNeighbors(_path.Peek());
            if (_neighbors.Count != 0)
            {
                Cell nextCell = ChooseNeighbor(_neighbors);
                RemoveWall(_path.Peek(), nextCell);
                nextCell.IsVisited = true; //делаем текущую клетку посещенной
                _cells[nextCell.X, nextCell.Y].IsVisited = true; //и в общем массиве
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
                if (_cells[curNeighbor.X, curNeighbor.Y].IsCell && !_cells[curNeighbor.X, curNeighbor.Y].IsVisited)
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
        _cells[first.X + addX, first.Y + addY].IsCell = true; //обращаем стену в клетку
        _cells[first.X + addX, first.Y + addY].IsVisited = true; //и делаем ее посещенной
        second.IsVisited = true; //делаем клетку посещенной
        _cells[second.X, second.Y] = second;
    }

    /// <summary>
    /// Рисуем лабиринт в консоль
    /// </summary>
    public void DrawGrid()
    {
        //Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.CursorVisible = false;
        for (var i = 0; i < _cells.GetUpperBound(0); i++)
            for (var j = 0; j < _cells.GetUpperBound(1); j++)
            {
                Console.SetCursorPosition(i, j);
                if (_cells[i, j].IsCell)
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