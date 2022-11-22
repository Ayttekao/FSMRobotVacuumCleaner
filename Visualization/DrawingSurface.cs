using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Visualization
{
    public class DrawingSurface : Panel
    {
        private Point _robotCoords;
        private static string _projectPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
        private string _filePath = Path.Combine(_projectPath, "resources");
        // private Imge image = Image.FromFile(Path.Combine(_filePath, "RVC.png"));

        protected override void OnMouseDown(MouseEventArgs e)
        {
            
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (_robotCoords != Point.Empty)
            {
                e.Graphics.DrawImage(Image.FromFile(Path.Combine(_filePath, "RVC.png")), _robotCoords);
            }
        }

        public void MoveRobot()
        {
            _robotCoords.X += 5;
            Invalidate();
        }

        public void AddRobot(MouseEventArgs e)
        {
            _robotCoords = new Point(e.X, e.Y);
            Invalidate();
        }
    }
}