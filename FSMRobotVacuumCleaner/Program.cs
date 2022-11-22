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

            var motion = new MotionControl(my, timeout, new Point(0, 0), Direction.Down, 1);
            var battery = new Battery(0, 30);
            var dustCollector = new DustCollector(0, 5);
            var robot = new RobotVacuumCleaner(battery, dustCollector, motion);
            while (true)
            {
                Console.WriteLine(motion.GetCurrentPoint());
                PrintMotion(my, robot.GetCurrentPoint());
                robot.Update();
                Thread.Sleep(1000);
            }
        }

        public static void PrintMotion(int[,] matrix, Point currentPosition)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (i == currentPosition.Y && j == currentPosition.X)
                    {
                        Console.Write(string.Format("*  "));
                    }
                    else
                    {
                        Console.Write(string.Format("{0}  ", matrix[i, j]));
                    }
                }

                Console.Write(Environment.NewLine);
            }

            Console.Write(Environment.NewLine + "*********" + Environment.NewLine);
        }
    }
}