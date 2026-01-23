using System;
using System.Collections.Generic;

public class Maze
{
    private readonly Dictionary<(int, int), bool[]> _mazeMap;
    private int _currX = 1;
    private int _currY = 1;

    public Maze(Dictionary<(int, int), bool[]> mazeMap)
    {
        _mazeMap = mazeMap;
    }

    public void MoveLeft()
    {
        if (!_mazeMap.ContainsKey((_currX, _currY)))
            throw new InvalidOperationException("Can't go that way!");
        
        bool[] movements = _mazeMap[(_currX, _currY)];
        
        if (movements[0])
        {
            _currX--;
        }
        else
        {
            throw new InvalidOperationException("Can't go that way!");
        }
    }

    public void MoveRight()
    {
        if (!_mazeMap.ContainsKey((_currX, _currY)))
            throw new InvalidOperationException("Can't go that way!");
        
        bool[] movements = _mazeMap[(_currX, _currY)];
        
        if (movements[1])
        {
            _currX++;
        }
        else
        {
            throw new InvalidOperationException("Can't go that way!");
        }
    }

    public void MoveUp()
    {
        if (!_mazeMap.ContainsKey((_currX, _currY)))
            throw new InvalidOperationException("Can't go that way!");
        
        bool[] movements = _mazeMap[(_currX, _currY)];
        
        if (movements[2])
        {
            _currY--;
        }
        else
        {
            throw new InvalidOperationException("Can't go that way!");
        }
    }

    public void MoveDown()
    {
        if (!_mazeMap.ContainsKey((_currX, _currY)))
            throw new InvalidOperationException("Can't go that way!");
        
        bool[] movements = _mazeMap[(_currX, _currY)];
        
        if (movements[3])
        {
            _currY++;
        }
        else
        {
            throw new InvalidOperationException("Can't go that way!");
        }
    }

    public string GetStatus()
    {
        return $"Current location (x={_currX}, y={_currY})";
    }
}