using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using FSMRobotVacuumCleaner.models.motion;
using FSMRobotVacuumCleaner.models.robot;
using WinFormAnimation_NET5;

namespace Visualization
{
    internal partial class Demo2 : UserControl
    {
        private readonly Animator2D _animator = new();
        private readonly List<List<int>> _map;
        private List<PictureBox> _obstacleBoxes;
        private PictureBox _robotPicture;
        private RobotVacuumCleaner _robot;
        private int _scale = 25;


        public Demo2()
        {
            InitializeComponent();
            _map = Utils.GenerateMap(10, 11);
            _obstacleBoxes = Utils.GetObstacleBoxes(_map, _scale);
            var randomStartPoint = Utils.RandomStartPoint(_map);
            _robotPicture =
                Utils.CreatePictureBox
                (
                    Properties.Resources.RVC,
                    new Point(randomStartPoint.X * _scale, randomStartPoint.Y * _scale),
                    new Size(_scale, _scale)
                );
            Controls.Add(_robotPicture);
            AddToControls(_obstacleBoxes);

            _robot = new RobotVacuumCleaner
            (
                new Battery(90, 100),
                new DustCollector(0, 1000),
                new MotionControl(_map, randomStartPoint, Direction.Down, 1)
            );
        }

        private void AddToControls(List<PictureBox> list)
        {
            foreach (var element in list)
            {
                Controls.Add(element);
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
                var duration = 150UL;
                var delayForAnimation = 500;
                var startPoint = new Point(_robot.GetCurrentPoint().X * _scale, _robot.GetCurrentPoint().Y * _scale);
                _robot.Update();
                var endPoint = new Point(_robot.GetCurrentPoint().X * _scale, _robot.GetCurrentPoint().Y * _scale);
                _animator.Paths = new Path2D
                (
                    new Float2D(startPoint.X, startPoint.Y),
                    new Float2D(startPoint.X, startPoint.Y),
                    duration
                ).ContinueTo(new Float2D(endPoint.X, endPoint.Y), duration);
                _animator.Play(_robotPicture, Animator2D.KnownProperties.Location);
                stateLabel.Text = $@"Current state: {_robot.GetStateName()}";
                Thread.Sleep(((int)duration + delayForAnimation));
            }

            return Task.CompletedTask;
        }

        private void StopButton(object sender, EventArgs e)
        {
            _animator.Stop();
        }
    }
}