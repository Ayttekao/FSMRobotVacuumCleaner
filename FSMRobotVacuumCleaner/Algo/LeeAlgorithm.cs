using System.Drawing;
using FSMRobotVacuumCleaner.Models.Map;

namespace FSMRobotVacuumCleaner.Algo;

public class LeeAlgorithm : IPathFinder
{
    private readonly List<List<int>> _map;
    private readonly int _width;
    private readonly int _height;
    private int _step;
    private int _finishPointI;
    private int _finishPointJ;

    public LeeAlgorithm(List<List<int>> map)
    {
        _map = map;
        _width = GetListGraph().Count;
        _height = GetListGraph().First().Count;
    }

    private List<List<int>> GetListGraph() => _map;

    private int GetWidth() => _width;

    private int GetHeight() => _height;

    public List<Tuple<int, int>> GetPath(Point startPoint, Point destination)
    {
        var localMap = GetListGraph().Select(x => new List<int>(x)).ToList();
        localMap = GetMapWithStarCell(localMap, startPoint.X, startPoint.Y);
        localMap = GetMapWithDestinationCell(localMap, destination.X, destination.Y);
        return GetPath(localMap);
    }

    private List<List<int>> GetMapWithStarCell(List<List<int>> map, int startX, int startY)
    {
        if (startX > GetWidth() || startX < 0)
        {
            throw new ArgumentException($"Incorrect start coordinate x = {startX}");
        }

        if (startY > GetHeight() || startY < 0)
        {
            throw new ArgumentException($"Incorrect start coordinate y = {startY}");
        }

        _step = 0;
        map[startX][startY] = _step;

        return map;
    }

    private List<List<int>> GetMapWithDestinationCell(List<List<int>> map, int destinationX, int destinationY)
    {
        if (destinationX > GetWidth() || destinationX < 0)
        {
            throw new ArgumentException($"Incorrect start coordinate x = {destinationX}");
        }

        if (destinationY > GetHeight() || destinationY < 0)
        {
            throw new ArgumentException($"Incorrect start coordinate y = {destinationY}");
        }

        map[destinationX][destinationY] = (int)PointType.Destination;
        return map;
    }

    private List<Tuple<int, int>> GetPath(List<List<int>> map)
    {
        var listWithWavePropagation = GetListWithWavePropagation(map);
        return GetRestoredPath(listWithWavePropagation);
    }

    private List<List<int>> GetListWithWavePropagation(List<List<int>> map)
    {
        var finished = false;
        var width = GetWidth();
        var height = GetHeight();

        do
        {
            for (var i = 0; i < width; i++)
            {
                for (var j = 0; j < height; j++)
                {
                    if (map[i][j] == _step)
                    {
                        if (i != width - 1)
                        {
                            if (map[i + 1][j] == (int)PointType.EmptySpace)
                            {
                                map[i + 1][j] = _step + 1;
                            }
                        }

                        if (j != height - 1)
                        {
                            if (map[i][j + 1] == (int)PointType.EmptySpace)
                            {
                                map[i][j + 1] = _step + 1;
                            }
                        }

                        if (i != 0)
                        {
                            if (map[i - 1][j] == (int)PointType.EmptySpace)
                            {
                                map[i - 1][j] = _step + 1;
                            }
                        }

                        if (j != 0)
                        {
                            if (map[i][j - 1] == (int)PointType.EmptySpace)
                            {
                                map[i][j - 1] = _step + 1;
                            }
                        }

                        if (i < width - 1)
                        {
                            if (map[i + 1][j] == (int)PointType.Destination)
                            {
                                _finishPointI = i + 1;
                                _finishPointJ = j;
                                finished = true;
                            }
                        }

                        if (j < height - 1)
                        {
                            if (map[i][j + 1] == (int)PointType.Destination)
                            {
                                _finishPointI = i;
                                _finishPointJ = j + 1;
                                finished = true;
                            }
                        }

                        if (i > 0)
                        {
                            if (map[i - 1][j] == (int)PointType.Destination)
                            {
                                _finishPointI = i - 1;
                                _finishPointJ = j;
                                finished = true;
                            }
                        }

                        if (j > 0)
                        {
                            if (map[i][j - 1] == (int)PointType.Destination)
                            {
                                _finishPointI = i;
                                _finishPointJ = j - 1;
                                finished = true;
                            }
                        }
                    }
                }
            }

            _step++;
        } while (!finished && _step < width * GetHeight());

        return map;
    }

    private List<Tuple<int, int>> GetRestoredPath(List<List<int>> map)
    {
        if (!map.Any())
        {
            return new List<Tuple<int, int>>();
        }

        var i = _finishPointI;
        var j = _finishPointJ;
        var path = new List<Tuple<int, int>>();
        path.Add(new Tuple<int, int>(i, j));

        do
        {
            if (i < GetWidth() - 1)
            {
                if (map[i + 1][j] == _step - 1)
                {
                    path.Add(new Tuple<int, int>(++i, j));
                }
            }

            if (j < GetHeight() - 1)
            {
                if (map[i][j + 1] == _step - 1)
                {
                    path.Add(new Tuple<int, int>(i, ++j));
                }
            }

            if (i > 0)
            {
                if (map[i - 1][j] == _step - 1)
                {
                    path.Add(new Tuple<int, int>(--i, j));
                }
            }

            if (j > 0)
            {
                if (map[i][j - 1] == _step - 1)
                {
                    path.Add(new Tuple<int, int>(i, --j));
                }
            }

            _step--;
        } while (_step != 0);

        return path;
    }
}