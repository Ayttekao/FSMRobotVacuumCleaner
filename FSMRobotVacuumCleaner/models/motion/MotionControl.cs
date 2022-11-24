using System.Drawing;
using FSMRobotVacuumCleaner.models.map;

namespace FSMRobotVacuumCleaner.models.motion;

public class MotionControl
{
    private List<List<int>> _map;
    private RobotPosition _position;
    private TimeSpan _actionTimeout;
    private TimeOnly _actionStart;
    private TimeOnly _actionEnd;
    private int _stepSize;

    public MotionControl(List<List<int>> map, TimeSpan actionTimeout, Point currentPoint, Direction currentDirection,
        int stepSize)
    {
        _map = map;
        _actionTimeout = actionTimeout;
        _actionStart = TimeOnly.MinValue;
        _actionEnd = TimeOnly.MinValue;
        _position = new RobotPosition(currentPoint, currentDirection);
        _stepSize = stepSize;
    }

    public Point GetCurrentPoint() => _position.GetPosition();

    public List<List<int>> GetMap() => _map;

    public void MoveToPoint(Point destination)
    {
        _position.TurnByMovingDirection(destination);
        _position.SetPosition(destination);
    }

    public void Move()
    {
        if (IsEndMap() || IsObstacleForward())
        {
            Detour();
        }
        else
        {
            _position.MoveForward(_stepSize);
        }
    }

    private void Detour()
    {
        UpdateTimer();
        if (_actionStart > _actionEnd)
        {
            _position.TurnRight();
            ResetTimer();
        }
        else
        {
            _position.TurnLeft();
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

    private bool IsEndMap() =>
        _position.GetDirection() switch
        {
            Direction.Up => _position.GetPosition().Y - _stepSize < 0,
            Direction.Down => _position.GetPosition().Y + _stepSize > _map.Count - 1,
            Direction.Right => _position.GetPosition().X + _stepSize > _map.First().Count - 1,
            Direction.Left => _position.GetPosition().X - _stepSize < 0,
            _ => throw new ArgumentOutOfRangeException(nameof(_position.GetDirection))
        };

    private bool IsObstacleForward() =>
        _position.GetDirection() switch
        {
            Direction.Up => _map[_position.GetPosition().Y - _stepSize][_position.GetPosition().X] == (int)PointType.Barrier,
            Direction.Down => _map[_position.GetPosition().Y + _stepSize][_position.GetPosition().X] == (int)PointType.Barrier,
            Direction.Right => _map[_position.GetPosition().Y][_position.GetPosition().X + _stepSize] == (int)PointType.Barrier,
            Direction.Left => _map[_position.GetPosition().Y][_position.GetPosition().X - _stepSize] == (int)PointType.Barrier,
            _ => throw new ArgumentOutOfRangeException(nameof(_position.GetDirection))
        };
}