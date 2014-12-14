using Robotik_Proje.App_Codes;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Robotik_Silverlight.App_Codes
{
    public class Pacman
    {
        public int initX { get; set; }
        public int initY { get; set; }
        public string path { get; set; }
        public Image image { get; set; }
        public List<Bolge> bolgesi { get; set; }
        public Bolge Point1 { get; set; }
        public Bolge Point2 { get; set; }
        public bool LeftRight { get; set; }
        public bool UpDown { get; set; }
        public Animasyon.DIRECTION CurrentDirection { get; set; }
        public Bolge current;
        public Bolge next;

        public Pacman(int x, int y, string img)
        {
            initX = x;
            initY = y;
            path = img;

            image = new Image();
            image.Width = image.Height = 30;
            image.Source = new BitmapImage(new Uri(path, UriKind.Relative));
            image.RenderTransformOrigin = new Point(0.5, 0.5);
            TransformGroup tg = new TransformGroup();
            tg.Children.Add(new TranslateTransform());
            image.RenderTransform = tg;


        }
        public void setBolgesi(params Bolge[] list)
        {
            bolgesi = new List<Bolge>();
            foreach (var item in list)
            {
                bolgesi.Add(item);
                item.canDuvar = false;
                ImageBrush brs = new ImageBrush();
                brs.ImageSource = new BitmapImage(new Uri(@"../../Assets/empty2.png", UriKind.Relative));
                item.button.Background = brs;
            }
        }

    }
}
