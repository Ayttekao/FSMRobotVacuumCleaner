using System.Drawing;

namespace FSMRobotVacuumCleaner.algo;

public interface IPathFinder
{
    public List<Tuple<int, int>> GetPath(Point startPoint, Point destination);
}