using System.Drawing;

namespace Lee_Algorithm;

public class LeeAlgorithm : IPathFinder
{
    private List<List<int>> ListGraph { get; set; }

    public List<Tuple<int, int>> Path { get; private set; }

    private int Width { get; set; }
    private int Height { get; set; }
    public bool PathFound { get; private set; }

    public int LengthPath => Path.Count;

    private int _step;
    private bool _finishingCellMarked;
    private int _finishPointI;
    private int _finishPointJ;
    
    public List<Tuple<int, int>> GetPath(List<List<int>> map, Point startPoint, Point destination)
    {
        ListGraph = map;
        Width = ListGraph.Count;
        Height = ListGraph.First().Count;
        SetStarCell(startPoint.X, startPoint.Y);
        SetDestinationCell(destination.X, destination.Y);
        PathFound = PathSearch();
        _finishingCellMarked = false;
        return Path;
    }

    public LeeAlgorithm() { }

    private void SetStarCell(int startX, int startY)
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
        ListGraph[startX][startY] = _step;
    }

    private void SetDestinationCell(int destinationX, int destinationY)
    {
        if (destinationX > Width || destinationX < 0)
        {
            throw new ArgumentException($"Incorrect start coordinate x = {destinationX}");
        }
        if (destinationY > Height || destinationY < 0)
        {
            throw new ArgumentException($"Incorrect start coordinate y = {destinationY}");
        }
        ListGraph[destinationX][destinationY] = (int)Figures.Destination;
    }

    private bool PathSearch()
    {
        if (WavePropagation())
        {
            if (RestorePath())
            {
                return true;
            }
        }

        return false;
    }

    private bool WavePropagation()
    {
        var w = Width;
        var h = Height;
        var finished = false;
        
        do
        {
            for (var i = 0; i < w; i++)
            {
                for (var j = 0; j < h; j++)
                {
                    if (ListGraph[i][j] == _step)
                    {
                        if (i != w - 1)
                        {
                            if (ListGraph[i + 1][j] == (int)Figures.EmptySpace)
                            {
                                ListGraph[i + 1][j] = _step + 1;
                            }
                        }
                        if (j != h - 1)
                        {
                            if (ListGraph[i][j + 1] == (int)Figures.EmptySpace)
                            {
                                ListGraph[i][j + 1] = _step + 1;
                            }
                        }
                        if (i != 0)
                        {
                            if (ListGraph[i - 1][j] == (int)Figures.EmptySpace)
                            {
                                ListGraph[i - 1][j] = _step + 1;
                            }
                        }
                        if (j != 0)
                        {
                            if (ListGraph[i][j - 1] == (int)Figures.EmptySpace)
                            {
                                ListGraph[i][j - 1] = _step + 1;
                            }
                        }

                        if (i < w - 1)
                        {
                            if (ListGraph[i + 1][j] == (int)Figures.Destination)
                            {
                                _finishPointI = i + 1;
                                _finishPointJ = j;
                                finished = true;
                            }
                        }

                        if (j < h - 1)
                        {
                            if (ListGraph[i][j + 1] == (int)Figures.Destination)
                            {
                                _finishPointI = i;
                                _finishPointJ = j + 1;
                                finished = true;
                            }
                        }

                        if (i > 0)
                        {
                            if (ListGraph[i - 1][j] == (int)Figures.Destination)
                            {
                                _finishPointI = i - 1;
                                _finishPointJ = j;
                                finished = true;
                            }
                        }

                        if (j > 0)
                        {
                            if (ListGraph[i][j - 1] == (int)Figures.Destination)
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
        } while (!finished && _step < w * h);

        _finishingCellMarked = finished;
        return finished;
    }
    
    private bool RestorePath()
    {
        if (!_finishingCellMarked)
        {
            return false;
        }

        var w = Width;
        var h = Height;
        var i = _finishPointI;
        var j = _finishPointJ;
        Path = new List<Tuple<int, int>>();
        AddToPath(i, j);

        do
        {
            if (i < w - 1)
            {
                if (ListGraph[i + 1][j] == _step - 1)
                {
                    AddToPath(++i, j);
                }
            }

            if (j < h - 1)
            {
                if (ListGraph[i][j + 1] == _step - 1)
                {
                    AddToPath(i, ++j);
                }
            }

            if (i > 0)
            {
                if (ListGraph[i - 1][j] == _step - 1)
                {
                    AddToPath(--i, j);
                }
            }

            if (j > 0)
            {
                if (ListGraph[i][j - 1] == _step - 1)
                {
                    AddToPath(i, --j);
                }
            }

            _step--;
        } while (_step != 0);

        return true;
    }

    private void AddToPath(int x, int y)
    {
        Path.Add(new Tuple<int, int>(x, y));
    }
}