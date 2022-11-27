using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using FSMRobotVacuumCleaner.models.map;
using Visualization.models.motion;
using Visualization.models.robot;
using WinFormAnimation_NET5;

namespace Visualization
{
    internal partial class Demo2 : UserControl
    {
        private readonly Animator2D _animator = new();
        private readonly List<List<int>> _map;
        private List<PictureBox> _obstacleBoxes;
        private RobotVacuumCleaner _robot;
        private int scale = 25;


        public Demo2()
        {
            InitializeComponent();
            _map = GenerateMap(10, 10);
            _obstacleBoxes = GetObstacleBoxes();
            AddToControlsPb();
            
            _robot = new RobotVacuumCleaner
            (
                new Battery(90, 100), 
                new DustCollector(0, 1000),
                new MotionControl(_map, new Point(0 ,0), Direction.Down, 1)
            );
        }

        private List<List<int>> GenerateMap(int height, int width)
        {
            var rand = new Random();
            var map = new int[height][];
                
            for (var i = 0; i < height; i++)
            {
                map[i] = new int[width];
                for (var j = 0; j < width; j++)
                {
                    if (rand.Next(100) > 90)
                        map[i][j] = (int)PointType.Barrier;
                    else
                        map[i][j] = (int)PointType.EmptySpace;
                }
            }

            return map.Select(x => x.ToList()).ToList();
        }

        private List<PictureBox> GetObstacleBoxes()
        {
            var list = new List<PictureBox>();
            var image = Image.FromFile(
                @"D:\RiderProjects\FSMRobotVacuumCleaner\FSMRobotVacuumCleaner\Visualization\resources\crate_0.png");
            
            for (var i = 0; i < _map.Count; i++)
            {
                for (var j = 0; j < _map.First().Count; j++)
                {
                    if (_map[i][j] == (int)PointType.Barrier)
                    {
                        var pb = new PictureBox();
                        pb.Location = new Point(j * scale, i * scale);
                        pb.Image = image;
                        pb.Size = new Size(25, 25);
                        pb.SizeMode = PictureBoxSizeMode.StretchImage;
                        list.Add(pb);
                    }
                }
            }

            return list;
        }

        private void AddToControlsPb()
        {
            foreach (var box in _obstacleBoxes)
            {
                Controls.Add(box);
            }
        }

        private async void PlayButton(object sender, EventArgs e)
        {
            await Task.Run(() => Cleaning());
        }

        private Task Cleaning()
        {
            while (true)
            {
                var startPoint = new Point(_robot.GetCurrentPoint().X * scale, _robot.GetCurrentPoint().Y * scale);
                _robot.Update();
                var endPoint = new Point(_robot.GetCurrentPoint().X * scale, _robot.GetCurrentPoint().Y * scale);
                _animator.Paths = new Path2D
                (
                    new Float2D(startPoint.X, startPoint.Y),
                    new Float2D(startPoint.X, startPoint.Y),
                    100
                ).ContinueTo(new Float2D(endPoint.X, endPoint.Y), 100);
                _animator.Play(pb_image, Animator2D.KnownProperties.Location);
                Thread.Sleep(500);
            }
            return Task.CompletedTask;
        }

        private void StopButton(object sender, EventArgs e)
        {
            _animator.Stop();
        }

        private void ResumeButton(object sender, EventArgs e)
        {
            _animator.Resume();
        }

        private void PauseButton(object sender, EventArgs e)
        {
            _animator.Pause();
        }

        private void RepeatChecked(object sender, EventArgs e)
        {
            _animator.Repeat = cb_repeat.Checked;
            cb_reverse.Enabled = cb_repeat.Checked;
        }

        private void ReverseChecked(object sender, EventArgs e)
        {
            _animator.ReverseRepeat = cb_reverse.Checked;
        }
    }
}