using System.Drawing;
using FSMRobotVacuumCleaner.algo;
using FSMRobotVacuumCleaner.models.motion;
using FSMRobotVacuumCleaner.models.robot;
using Lee_Algorithm;

namespace FSMRobotVacuumCleaner
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var rand = new Random();
            const int heigth = 6;
            const int width = 6;
            var my = new int[heigth, width];
            for (var i = 0; i < heigth; i++)
            {
                for (var j = 0; j < width; j++)
                {
                    if (rand.Next(100) > 70)
                        my[i, j] = (int)Figures.Barrier;
                    else
                        my[i, j] = (int)Figures.EmptySpace;
                }
            }

            var timeout = new TimeSpan(0, 0, 60);
            var motion = new MotionControl(my, timeout, new Point(0, 3), Direction.Down, 1);
            var battery = new Battery(80, 100);
            var dustCollector = new DustCollector(0, 100);
            var robot = new RobotVacuumCleaner(battery, dustCollector, motion);
            
            while (true)
            {
                Console.WriteLine(motion.GetCurrentPoint());
                Print(my, robot.GetCurrentPoint());
                robot.Update();
                Thread.Sleep(1000);
            }
        }

        private static void Print(int[,] array, Point currentPosition)
        {
            Console.WriteLine("***");
            var msg = string.Empty;
            var x = array.GetLength(0);
            var y = array.GetLength(1);
            for (var i = 0; i < x; i++)
            {
                for (var j = 0; j < y; j++)
                {
                    if (i == currentPosition.Y && j == currentPosition.X)
                    {
                        msg = string.Format("{0,3}", "o");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        switch (array[i, j])
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
                                msg = string.Format("{0,3}", array[i, j]);
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