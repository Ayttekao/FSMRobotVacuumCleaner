using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using FSMRobotVacuumCleaner.models.map;

namespace Visualization;

public static class Utils
{
    public static Point RandomStartPoint(List<List<int>> map)
    {
        var rand = new Random();
        var j = rand.Next(map.First().Count);
        var i = rand.Next(map.Count);

        while (map[i][j] != (int)PointType.Barrier)
        {
            j = rand.Next(map.First().Count);
            i = rand.Next(map.Count);
        }

        return new Point(i, j);
    }

    public static List<List<int>> GenerateMap(int height, int width)
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

    public static List<PictureBox> GetObstacleBoxes(List<List<int>> map, int scale)
    {
        var list = new List<PictureBox>();
        var image = Properties.Resources.crate_0;

        for (var i = 0; i < map.Count; i++)
        {
            for (var j = 0; j < map.First().Count; j++)
            {
                if (map[i][j] == (int)PointType.Barrier)
                {
                    var pb = CreatePictureBox(image, new Point(j * scale, i * scale), new Size(scale, scale));
                    list.Add(pb);
                }
            }
        }

        return list;
    }

    public static PictureBox CreatePictureBox(Image image, Point point, Size size)
    {
        var pb = new PictureBox();
        pb.Image = image;
        pb.Location = point;
        pb.Size = size;
        pb.SizeMode = PictureBoxSizeMode.StretchImage;

        return pb;
    }
}