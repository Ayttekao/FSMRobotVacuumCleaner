using System.Drawing;

namespace Lee_Algorithm;

public interface IPathFinder
{
    public List<Tuple<int, int>> GetPath(Point startPoint, Point destination);
}