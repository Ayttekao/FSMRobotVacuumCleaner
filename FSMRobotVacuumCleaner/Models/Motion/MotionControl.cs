using System.Drawing;
using FSMRobotVacuumCleaner.Models.Map;

namespace FSMRobotVacuumCleaner.Models.Motion;

public class MotionControl
{
    private List<List<int>> _map;
    private RobotPosition _position;
    private int _stepSize;

    public MotionControl(List<List<int>> map, Point currentPoint, Direction currentDirection, int stepSize)
    {
        _map = map;
        _position = new RobotPosition(currentPoint, currentDirection);
        _stepSize = stepSize;
    }

    public Point GetCurrentPoint() => _position.GetPosition();

    public Direction GetDirection() => _position.GetDirection();

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
            var count = new Random().Next(1, 3);
            for (var i = 0; i < count; i++)
            {
                _position.TurnRight();
            }
        }
        else
        {
            _position.MoveForward(_stepSize);
        }
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
    
    private bool IsObstacleRight() =>
        _position.GetDirection() switch
        {
            Direction.Up => _map[_position.GetPosition().Y][_position.GetPosition().X + _stepSize] == (int)PointType.Barrier,
            Direction.Down => _map[_position.GetPosition().Y][_position.GetPosition().X  - _stepSize] == (int)PointType.Barrier,
            Direction.Right => _map[_position.GetPosition().Y - _stepSize][_position.GetPosition().X] == (int)PointType.Barrier,
            Direction.Left => _map[_position.GetPosition().Y + _stepSize][_position.GetPosition().X] == (int)PointType.Barrier,
            _ => throw new ArgumentOutOfRangeException(nameof(_position.GetDirection))
        };
    
    private bool IsObstacleLeft() =>
        _position.GetDirection() switch
        {
            Direction.Up => _map[_position.GetPosition().Y][_position.GetPosition().X - _stepSize] == (int)PointType.Barrier,
            Direction.Down => _map[_position.GetPosition().Y][_position.GetPosition().X  + _stepSize] == (int)PointType.Barrier,
            Direction.Right => _map[_position.GetPosition().Y + _stepSize][_position.GetPosition().X] == (int)PointType.Barrier,
            Direction.Left => _map[_position.GetPosition().Y - _stepSize][_position.GetPosition().X] == (int)PointType.Barrier,
            _ => throw new ArgumentOutOfRangeException(nameof(_position.GetDirection))
        };
}