using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Visualization
{
    public partial class Form1 : Form
    {
        private bool _dragable;
        private Point _startPosition;

        public Form1()
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.None;
            Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 40, 40));
        }

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse
        );


        private void MakeDragable(object sender, MouseEventArgs e)
        {
            _dragable = true;
            _startPosition = e.Location;
        }

        private void DragForm(object sender, MouseEventArgs e)
        {
            if (_dragable)
            {
                Location = new Point(Cursor.Position.X - _startPosition.X, Cursor.Position.Y - _startPosition.Y);
            }
        }

        private void DisableDrag(object sender, MouseEventArgs e)
        {
            _dragable = false;
        }

        private void HideForm_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void CloseForm_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}