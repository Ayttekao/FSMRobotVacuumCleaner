using System.Drawing;
using FSMRobotVacuumCleaner.models.motion;
using Lee_Algorithm;

namespace FSMRobotVacuumCleaner.algo;

public class MotionControl
{
    private List<List<int>> _map;
    private Direction _currentDirection;
    private TimeSpan _actionTimeout;
    private TimeOnly _actionStart;
    private TimeOnly _actionEnd;
    private Point _currentPoint;
    private int _stepSize;

    public MotionControl(List<List<int>> map, TimeSpan actionTimeout, Point currentPoint, Direction currentDirection,
        int stepSize)
    {
        _map = map;
        _actionTimeout = actionTimeout;
        _actionStart = TimeOnly.MinValue;
        _actionEnd = TimeOnly.MinValue;
        _currentPoint = currentPoint;
        _currentDirection = currentDirection;
        _stepSize = stepSize;
    }

    public Point GetCurrentPoint() => _currentPoint;

    public List<List<int>> GetMap() => _map;

    public void MoveToPoint(Point destination)
    {
        TurnByMovingDirection(destination);
        _currentPoint = destination;
    }

    public void Move()
    {
        if (IsEndMap() || IsObstacleForward())
        {
            Detour();
        }
        else
        {
            MoveForward();
        }
    }

    private void Detour()
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

    private void TurnByMovingDirection(Point destination)
    {
        if (_currentPoint.X > destination.X)
        {
            _currentDirection = Direction.Left;
        }
        else if (_currentPoint.X < destination.X)
        {
            _currentDirection = Direction.Right;
        }
        else if (_currentPoint.Y > destination.Y)
        {
            _currentDirection = Direction.Down;
        }
        else
        {
            _currentDirection = Direction.Down;
        }
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
            Direction.Up => _currentPoint.Y - _stepSize < 0,
            Direction.Down => _currentPoint.Y + _stepSize > _map.Count - 1,
            Direction.Right => _currentPoint.X + _stepSize > _map.First().Count - 1,
            Direction.Left => _currentPoint.X - _stepSize < 0,
            _ => throw new ArgumentOutOfRangeException(nameof(_currentDirection))
        };
    }

    private bool IsObstacleForward()
    {
        return _currentDirection switch
        {
            Direction.Up => _map[_currentPoint.Y - _stepSize][_currentPoint.X] == (int)Figures.Barrier,
            Direction.Down => _map[_currentPoint.Y + _stepSize][_currentPoint.X] == (int)Figures.Barrier,
            Direction.Right => _map[_currentPoint.Y][_currentPoint.X + _stepSize] == (int)Figures.Barrier,
            Direction.Left => _map[_currentPoint.Y][_currentPoint.X - _stepSize] == (int)Figures.Barrier,
            _ => throw new ArgumentOutOfRangeException(nameof(_currentDirection))
        };
    }
}