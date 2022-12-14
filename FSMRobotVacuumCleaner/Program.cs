using System.Drawing;
using FSMRobotVacuumCleaner.Models.Map;
using FSMRobotVacuumCleaner.Models.Motion;
using FSMRobotVacuumCleaner.Models.Robot;

namespace FSMRobotVacuumCleaner
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var rand = new Random();
            const int height = 6;
            const int width = 6;
            var map = new int[height][];
            for (var i = 0; i < height; i++)
            {
                map[i] = new int[width];
            }
                
            for (var i = 0; i < height; i++)
            {
                for (var j = 0; j < width; j++)
                {
                    if (rand.Next(100) > 80)
                        map[i][j] = (int)PointType.Barrier;
                    else
                        map[i][j] = (int)PointType.EmptySpace;
                }
            }

            var startPoint = new Point(0, 3);
            var motion = new MotionControl(map.Select(x => x.ToList()).ToList(), startPoint, Direction.Up, 1);
            var battery = new Battery(80, 100);
            var dustCollector = new DustCollector(0, 100);
            var robot = new RobotVacuumCleaner(battery, dustCollector, motion);
            
            while (true)
            {
                Console.WriteLine(motion.GetCurrentPoint());
                Print(map.Select(x => x.ToList()).ToList(), robot.GetCurrentPoint(), startPoint);
                robot.Update();
                Thread.Sleep(500);
            }
        }

        private static void Print(List<List<int>> array, Point currentPosition, Point startPoint)
        {
            Console.WriteLine("***");
            var msg = string.Empty;
            var x = array.First().Count;
            var y = array.Count;
            for (var i = 0; i < x; i++)
            {
                for (var j = 0; j < y; j++)
                {
                    if (i == currentPosition.Y && j == currentPosition.X)
                    {
                        msg = string.Format("{0,3}", "o");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else if (i == startPoint.Y && j == startPoint.X)
                    {
                        msg = string.Format("{0,3}", "U");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    }
                    else
                    {
                        switch (array[i][j])
                        {
                            case (int)PointType.Path:
                                msg = string.Format("{0,3}", "+");
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                break;
                            case (int)PointType.StartPosition:
                                msg = string.Format("{0,3}", "s");
                                Console.ForegroundColor = ConsoleColor.Green;
                                break;
                            case (int)PointType.Destination:
                                msg = string.Format("{0,3}", "d");
                                Console.ForegroundColor = ConsoleColor.Red;
                                break;
                            case (int)PointType.EmptySpace:
                                msg = string.Format("{0,3}", "'");
                                Console.ForegroundColor = ConsoleColor.DarkBlue;
                                break;
                            case (int)PointType.Barrier:
                                msg = string.Format("{0,3}", "*");
                                Console.ForegroundColor = ConsoleColor.DarkYellow;
                                break;
                            default:
                                msg = string.Format("{0,3}", array[i][j]);
                                Console.ForegroundColor = ConsoleColor.DarkGray;
                                break;
                        }
                    }

                    Console.Write(msg);
                    Console.ResetColor();
                }

                Console.WriteLine();
            }
        }
    }
}