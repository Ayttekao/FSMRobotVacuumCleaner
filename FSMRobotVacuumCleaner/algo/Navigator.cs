using System.Drawing;
using Lee_Algorithm;

namespace FSMRobotVacuumCleaner.algo;

public class Navigator
{
    private List<List<int>> _map;
    private IPathFinder _pathFinder;

    public Navigator(List<List<int>> map, IPathFinder pathFinder)
    {
        _map = map;
        _pathFinder = pathFinder;
    }

    public List<Tuple<int, int>> GetPath(Point startPoint, Point destinationPoint) =>
        _pathFinder.GetPath(_map, startPoint, destinationPoint);
}