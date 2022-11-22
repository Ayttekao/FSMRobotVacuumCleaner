using System.Drawing;

namespace Lee_Algorithm;

public interface IPathFinder
{
    public List<Tuple<int, int>> GetPath(List<List<int>> map, Point startPoint, Point destination);
}