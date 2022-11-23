using System.Drawing;
using Lee_Algorithm;

namespace FSMRobotVacuumCleaner.algo;

public class Navigator
{
    private List<List<int>> _map;
    private IPathFinder _pathFinder;
    private Stack<Tuple<int, int>> _pathToDestinationPoint;
    private Stack<Tuple<int, int>> _pathToStartPoint;

    public Navigator(List<List<int>> map, IPathFinder pathFinder)
    {
        _map = map;
        _pathFinder = pathFinder;
    }

    public bool IsStartPoint() => !_pathToStartPoint.Any();

    public bool IsDestinationPoint() => !_pathToDestinationPoint.Any();

    public Point GetStartWaypoint()
    {
        if (IsStartPoint())
        {
            throw new InvalidOperationException("_pathToStartPoint is empty");
        }
        
        var coords = _pathToStartPoint.Pop();

        return new Point(coords.Item2, coords.Item1);
    }

    public Point GetDestinationWaypoint()
    {
        if (IsDestinationPoint())
        {
            throw new InvalidOperationException("_pathToDestinationPoint is empty");
        }
        
        var coords = _pathToDestinationPoint.Pop();

        return new Point(coords.Item2, coords.Item1);
    }

    public void CreatePath(Point startPoint, Point destinationPoint)
    {
        var path = _pathFinder.GetPath(_map, Swap(startPoint), Swap(destinationPoint));
        _pathToStartPoint = new Stack<Tuple<int, int>>(path.ToArray().Reverse());
        _pathToDestinationPoint = new Stack<Tuple<int, int>>(path);
    }

    private Point Swap(Point point) => new Point(point.Y, point.X);
}