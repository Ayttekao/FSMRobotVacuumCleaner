using System.Drawing;

namespace FSMRobotVacuumCleaner.Algo;

public interface IPathFinder
{
    public List<Tuple<int, int>> GetPath(Point startPoint, Point destination);
}