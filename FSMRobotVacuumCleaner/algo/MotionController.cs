using System.Drawing;
using Lee_Algorithm;

namespace FSMRobotVacuumCleaner.algo;

public class MotionController
{
    private int[,] _map;
    private TimeSpan _actionTimeout;
    private TimeOnly _actionStart;
    private TimeOnly _actionEnd;
    private Point _currentPoint;
    private Direction _currentDirection;
    private int _stepSize;

    public MotionController(int[,] map, TimeSpan actionTimeout, Point currentPoint, Direction currentDirection, int stepSize)
    {
        _map = map;
        _actionTimeout = actionTimeout;
        _actionStart = TimeOnly.MinValue;
        _actionEnd = TimeOnly.MinValue;
        _currentPoint = currentPoint;
        _currentDirection = currentDirection;
        _stepSize = stepSize;
    }

    public Point GetCurrentPosition() => _currentPoint;

    public void Move()
    {
        if (IsEndMap() || IsObstacleForward())
        {
            UpdateTimer();
            if (_actionStart > _actionEnd)
            {
                TurnRight();
                ResetTimer();
            }
            else
            {
                TurnLeft();
            }
        }
        else
        {
            MoveForward();
        }
    }

    private void UpdateTimer()
    {
        var currentTime = DateTime.Now;
        _actionStart = TimeOnly.FromDateTime(currentTime);
        if (_actionEnd == TimeOnly.MinValue)
        {
            _actionEnd = TimeOnly.FromDateTime(currentTime).Add(_actionTimeout);
        }
    }

    private void ResetTimer()
    {
        _actionEnd = TimeOnly.MinValue;
    }

    private void TurnLeft()
    {
        _currentDirection = _currentDirection switch
        {
            Direction.Up => Direction.Left,
            Direction.Left => Direction.Down,
            Direction.Down => Direction.Right,
            Direction.Right => Direction.Up,
            _ => throw new ArgumentOutOfRangeException(nameof(_currentDirection))
        };
    }
    
    private void TurnRight()
    {
        _currentDirection = _currentDirection switch
        {
            Direction.Up => Direction.Right,
            Direction.Right => Direction.Down,
            Direction.Down => Direction.Left,
            Direction.Left => Direction.Up,
            _ => throw new ArgumentOutOfRangeException(nameof(_currentDirection))
        };
    }

    private void MoveForward()
    {
        switch (_currentDirection)
        {
            case Direction.Up:
                _currentPoint.Y -= _stepSize;
                break;
            case Direction.Down:
                _currentPoint.Y += _stepSize;
                break;
            case Direction.Right:
                _currentPoint.X += _stepSize;
                break;
            case Direction.Left:
                _currentPoint.X -= _stepSize;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(_currentDirection));
        }
    }

    private bool IsEndMap()
    {
        return _currentDirection switch
        {
            Direction.Up => _currentPoint.Y - _stepSize <= 0,
            Direction.Down => _currentPoint.Y + _stepSize >= _map.GetLength(0),
            Direction.Right => _currentPoint.X + _stepSize >= _map.GetLength(1),
            Direction.Left => _currentPoint.X - _stepSize <= 0,
            _ => throw new ArgumentOutOfRangeException(nameof(_currentDirection))
        };
    }

    private bool IsObstacleForward()
    {
        return _currentDirection switch
        {
            Direction.Up => _map[_currentPoint.Y - _stepSize, _currentPoint.X] == (int)Figures.Barrier,
            Direction.Down => _map[_currentPoint.Y + _stepSize, _currentPoint.X] == (int)Figures.Barrier,
            Direction.Right => _map[_currentPoint.Y, _currentPoint.X + _stepSize] == (int)Figures.Barrier,
            Direction.Left => _map[_currentPoint.Y, _currentPoint.X - _stepSize] == (int)Figures.Barrier,
            _ => throw new ArgumentOutOfRangeException(nameof(_currentDirection))
        };
    }
}