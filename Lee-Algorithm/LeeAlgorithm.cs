using System.Drawing;

namespace Lee_Algorithm;

public class LeeAlgorithm : IPathFinder
{
    private List<List<int>> ListGraph { get; }
    private int Width { get; }
    private int Height { get; }
    private int _step;
    private int _finishPointI;
    private int _finishPointJ;

    public List<Tuple<int, int>> GetPath(Point startPoint, Point destination)
    {
        var localMap = ListGraph.Select(x => new List<int>(x)).ToList();
        localMap = GetMapWithStarCell(localMap, startPoint.X, startPoint.Y);
        localMap = GetMapWithDestinationCell(localMap, destination.X, destination.Y);
        return GetPath(localMap);
    }

    public LeeAlgorithm(List<List<int>> map)
    {
        ListGraph = map;
        Width = ListGraph.Count;
        Height = ListGraph.First().Count;
    }

    private List<List<int>> GetMapWithStarCell(List<List<int>> map, int startX, int startY)
    {
        if (startX > Width || startX < 0)
        {
            throw new ArgumentException($"Incorrect start coordinate x = {startX}");
        }

        if (startY > Height || startY < 0)
        {
            throw new ArgumentException($"Incorrect start coordinate y = {startY}");
        }

        _step = 0;
        map[startX][startY] = _step;

        return map;
    }

    private List<List<int>> GetMapWithDestinationCell(List<List<int>> map, int destinationX, int destinationY)
    {
        if (destinationX > Width || destinationX < 0)
        {
            throw new ArgumentException($"Incorrect start coordinate x = {destinationX}");
        }

        if (destinationY > Height || destinationY < 0)
        {
            throw new ArgumentException($"Incorrect start coordinate y = {destinationY}");
        }

        map[destinationX][destinationY] = (int)Figures.Destination;
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

        do
        {
            for (var i = 0; i < Width; i++)
            {
                for (var j = 0; j < Height; j++)
                {
                    if (map[i][j] == _step)
                    {
                        if (i != Width - 1)
                        {
                            if (map[i + 1][j] == (int)Figures.EmptySpace)
                            {
                                map[i + 1][j] = _step + 1;
                            }
                        }

                        if (j != Height - 1)
                        {
                            if (map[i][j + 1] == (int)Figures.EmptySpace)
                            {
                                map[i][j + 1] = _step + 1;
                            }
                        }

                        if (i != 0)
                        {
                            if (map[i - 1][j] == (int)Figures.EmptySpace)
                            {
                                map[i - 1][j] = _step + 1;
                            }
                        }

                        if (j != 0)
                        {
                            if (map[i][j - 1] == (int)Figures.EmptySpace)
                            {
                                map[i][j - 1] = _step + 1;
                            }
                        }

                        if (i < Width - 1)
                        {
                            if (map[i + 1][j] == (int)Figures.Destination)
                            {
                                _finishPointI = i + 1;
                                _finishPointJ = j;
                                finished = true;
                            }
                        }

                        if (j < Height - 1)
                        {
                            if (map[i][j + 1] == (int)Figures.Destination)
                            {
                                _finishPointI = i;
                                _finishPointJ = j + 1;
                                finished = true;
                            }
                        }

                        if (i > 0)
                        {
                            if (map[i - 1][j] == (int)Figures.Destination)
                            {
                                _finishPointI = i - 1;
                                _finishPointJ = j;
                                finished = true;
                            }
                        }

                        if (j > 0)
                        {
                            if (map[i][j - 1] == (int)Figures.Destination)
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
        } while (!finished && _step < Width * Height);

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
            if (i < Width - 1)
            {
                if (map[i + 1][j] == _step - 1)
                {
                    path.Add(new Tuple<int, int>(++i, j));
                }
            }

            if (j < Height - 1)
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