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
        private PictureBox _robotPicture;
        private PictureBox _dockstationPicture;
        private RobotVacuumCleaner _robot;
        private static CancellationTokenSource _tokenSource = new();
        private int _scale = 25;
        private int _maxChargeLevel = 100;


        public Demo2()
        {
            InitializeComponent();
            var map = Utils.GenerateMap(10, 11);
            var obstacleBoxes = Utils.GetObstacleBoxes(map, _scale);
            var randomStartPoint = Utils.RandomStartPoint(map);
            
            _dockstationPicture =
                Utils.CreatePictureBox
                (
                    Properties.Resources.dockstation,
                    new Point(randomStartPoint.X * _scale, randomStartPoint.Y * _scale),
                    new Size(_scale, _scale)
                );
            
            _robotPicture =
                Utils.CreatePictureBox
                (
                    Properties.Resources.RVC_DOWN,
                    new Point(randomStartPoint.X * _scale, randomStartPoint.Y * _scale),
                    new Size(_scale, _scale)
                );
            
            Controls.Add(_robotPicture);
            Controls.Add(_dockstationPicture);
            AddToControls(obstacleBoxes);

            _robot = new RobotVacuumCleaner
            (
                new Battery(90, _maxChargeLevel),
                new DustCollector(0, 1000),
                new MotionControl(map, randomStartPoint, Direction.Down, 1)
            );
        }

        private void AddToControls(List<PictureBox> list)
        {
            foreach (var element in list)
            {
                Controls.Add(element);
            }
        }

        private async void StartStopButtonClick(object sender, EventArgs e)
        {
            if (btn_play.Text == @"Start")
            {
                btn_play.Text = @"Stop";
                _tokenSource.Dispose();
                _tokenSource = new CancellationTokenSource();
                await Task.Run(() => Cleaning(_tokenSource.Token), _tokenSource.Token);
            }
            else
            {
                btn_play.Text = @"Start";
                _tokenSource.Cancel();
            }
        }

        private Task Cleaning(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                var duration = 150UL;
                var delayForAnimation = 500;
                var startPoint = new Point(_robot.GetCurrentPoint().X * _scale, _robot.GetCurrentPoint().Y * _scale);
                
                _robot.Update();
                var endPoint = new Point(_robot.GetCurrentPoint().X * _scale, _robot.GetCurrentPoint().Y * _scale);
                
                RotateRobot(_robot.GetDirection());
                DisplayCharge(_robot.GetCurrentCharge());

                _animator.Paths = new Path2D
                (
                    new Float2D(startPoint.X, startPoint.Y),
                    new Float2D(startPoint.X, startPoint.Y),
                    duration
                ).ContinueTo(new Float2D(endPoint.X, endPoint.Y), duration);

                _animator.Play(_robotPicture, Animator2D.KnownProperties.Location);

                Thread.Sleep(((int)duration + delayForAnimation));
                stateLabel.Text = $@"Current state: {_robot.GetStateName()}";
            }

            return Task.CompletedTask;
        }

        private void DisplayCharge(int charge)
        {
            Image image = charge switch
            {
                < 20 => Properties.Resources.BATTERY_20,
                < 40 => Properties.Resources.BATTERY_40,
                < 60 => Properties.Resources.BATTERY_60,
                < 80 => Properties.Resources.BATTERY_80,
                _ => Properties.Resources.BATTERY_100
            };

            batteryImage.Image = image;
        }
        
        private void RotateRobot(Direction direction)
        {
            var image = direction switch
            {
                Direction.Up => Properties.Resources.RVC_UP,
                Direction.Down => Properties.Resources.RVC_DOWN,
                Direction.Left => Properties.Resources.RVC_LEFT,
                Direction.Right => Properties.Resources.RVC_RIGHT,
                _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
            };

            _robotPicture.Image = image;
        }
    }
}