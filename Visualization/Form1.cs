using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Visualization
{
    public partial class Form1 : Form
    {
        private bool dragable;
        private Point startPosition;

        public Form1()
        {
            InitializeComponent();
        }

        private void MakeDragable(object sender, MouseEventArgs e)
        {
            dragable = true;
            startPosition = e.Location;
        }

        private void DragForm(object sender, MouseEventArgs e)
        {
            if (dragable)
            {
                Location = new Point(Cursor.Position.X - startPosition.X, Cursor.Position.Y - startPosition.Y);
            }
        }

        private void DisableDrag(object sender, MouseEventArgs e)
        {
            dragable = false;
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