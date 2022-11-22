using System.Diagnostics;
using System.Windows.Forms;

namespace Visualization
{
    public partial class Form1 : Form
    {
        private bool _createRobotButtonClicked = false;
        private bool _moveRobot = false;
        
        public Form1()
        {
            InitializeComponent();
        }

        public void CreateRobotButtonChangeState(object sender, MouseEventArgs e)
        {
            _createRobotButtonClicked = !_createRobotButtonClicked;
            Debug.WriteLine($"Clicked {_createRobotButtonClicked}");
        }
        
        private void AddObject(object sender, MouseEventArgs e)
        {
            if (_createRobotButtonClicked)
            {
                drawingSurface.AddRobot(e);
            }
        }

        private void MoveRobotButton(object sender, MouseEventArgs e)
        {
            _moveRobot = !_moveRobot;
            if (_moveRobot)
            {
                drawingSurface.MoveRobot();
            }
            else
            {
                // drawingSurface.StopRobot();
            }
        }
    }
}