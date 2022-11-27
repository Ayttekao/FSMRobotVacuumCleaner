using System.Drawing;

namespace FSMRobotVacuumCleaner.models.motion;

public class RobotPosition
{
    private Point _point;
    private Direction _direction;

    public RobotPosition(Point point, Direction direction)
    {
        _point = point;
        _direction = direction;
    }

    public Point GetPosition() => _point;

    public Direction GetDirection() => _direction;

    public void SetPosition(Point point) => _point = point;

    public void SetDirection(Direction direction) => _direction = direction;

    public void TurnByMovingDirection(Point destination)
    {
        if (_point.X > destination.X)
        {
            _direction = Direction.Left;
        }
        else if (_point.X < destination.X)
        {
            _direction = Direction.Right;
        }
        else if (_point.Y > destination.Y)
        {
            _direction = Direction.Down;
        }
        else
        {
            _direction = Direction.Down;
        }
    }

    public void TurnLeft()
    {
        _direction = _direction switch
        {
            Direction.Up => Direction.Left,
            Direction.Left => Direction.Down,
            Direction.Down => Direction.Right,
            Direction.Right => Direction.Up,
            _ => throw new ArgumentOutOfRangeException(nameof(_direction))
        };
    }

    public void TurnRight()
    {
        _direction = _direction switch
        {
            Direction.Up => Direction.Right,
            Direction.Right => Direction.Down,
            Direction.Down => Direction.Left,
            Direction.Left => Direction.Up,
            _ => throw new ArgumentOutOfRangeException(nameof(_direction))
        };
    }

    public void MoveForward(int stepSize)
    {
        switch (_direction)
        {
            case Direction.Up:
                _point.Y -= stepSize;
                break;
            case Direction.Down:
                _point.Y += stepSize;
                break;
            case Direction.Right:
                _point.X += stepSize;
                break;
            case Direction.Left:
                _point.X -= stepSize;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(_direction));
        }
    }
    
    public void MoveBackward(int stepSize)
    {
        switch (_direction)
        {
            case Direction.Up:
                _point.Y += stepSize;
                break;
            case Direction.Down:
                _point.Y -= stepSize;
                break;
            case Direction.Right:
                _point.X -= stepSize;
                break;
            case Direction.Left:
                _point.X += stepSize;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(_direction));
        }
    }
}