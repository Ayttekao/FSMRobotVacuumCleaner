using System.Drawing;

namespace Lee_Algorithm
{
    public static class Program
    {
        static void Main(string[] args)
        {
            var rand = new Random();
            const int height = 20;
            const int width = 36;
            while (true)
            {
                var map = new int[height][];
                for (var i = 0; i < height; i++)
                {
                    map[i] = new int[width];
                }
                
                for (var i = 0; i < height; i++)
                {
                    for (var j = 0; j < width; j++)
                    {
                        if (rand.Next(100) > 70)
                            map[i][j] = (int)Figures.Barrier;
                        else
                            map[i][j] = (int)Figures.EmptySpace;
                    }
                }

                var random = new Random(DateTime.Now.Millisecond);
                var startPoint = new Point(random.Next(height), random.Next(width));
                var destinationPoint = new Point(random.Next(height), random.Next(width));
                map[startPoint.X][startPoint.Y] = (int)Figures.StartPosition;
                map[destinationPoint.X][destinationPoint.Y] = (int)Figures.Destination;
                Print(map.Select(x => x.ToList()).ToList());

                var leeAlgorithm = new LeeAlgorithm();
                var path = 
                    leeAlgorithm.GetPath(map.Select(x => x.ToList()).ToList(), startPoint, destinationPoint);
                
                Console.WriteLine(leeAlgorithm.PathFound);
                Console.WriteLine($"startPoint {startPoint}");
                Console.WriteLine($"destinationPoint {destinationPoint}");
                if (leeAlgorithm.PathFound)
                {
                    foreach (var item in leeAlgorithm.Path)
                    {
                        if (Equals(item, leeAlgorithm.Path.Last()))
                            map[item.Item1][item.Item2] = (int)Figures.StartPosition;
                        else if (Equals(item, leeAlgorithm.Path.First()))
                            map[item.Item1][item.Item2] = (int)Figures.Destination;
                        else
                            map[item.Item1][item.Item2] = (int)Figures.Path;
                    }

                    Print(map.Select(x => x.ToList()).ToList());
                    Console.WriteLine("Length " + leeAlgorithm.LengthPath);
                }
                else
                    Console.WriteLine("Path not found");

                Console.ReadLine();
            }
        }

        private static void Print(List<List<int>> array)
        {
            Console.WriteLine("***");
            var msg = string.Empty;
            var x = array.First().Count;
            var y = array.Count;
            for (var i = 0; i < y; i++)
            {
                for (var j = 0; j < x; j++)
                {
                    switch (array[i][j])
                    {
                        case (int)Figures.Path:
                            msg = string.Format("{0,3}", "+");
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            break;
                        case (int)Figures.StartPosition:
                            msg = string.Format("{0,3}", "s");
                            Console.ForegroundColor = ConsoleColor.Green;
                            break;
                        case (int)Figures.Destination:
                            msg = string.Format("{0,3}", "d");
                            Console.ForegroundColor = ConsoleColor.Red;
                            break;
                        case (int)Figures.EmptySpace:
                            msg = string.Format("{0,3}", "'");
                            Console.ForegroundColor = ConsoleColor.DarkBlue;
                            break;
                        case (int)Figures.Barrier:
                            msg = string.Format("{0,3}", "*");
                            Console.ForegroundColor = ConsoleColor.Blue;
                            break;
                        default:
                            msg = string.Format("{0,3}", array[i][j]);
                            Console.ForegroundColor = ConsoleColor.DarkGray;
                            break;
                    }

                    Console.Write(msg);
                    Console.ResetColor();
                }

                Console.WriteLine();
            }

            Console.WriteLine(msg);
        }
    }
}