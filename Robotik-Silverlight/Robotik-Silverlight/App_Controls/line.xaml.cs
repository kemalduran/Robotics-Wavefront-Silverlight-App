using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Robotik_Proje.App_Codes;

namespace Robotik_Silverlight.App_Controls
{
    public partial class line : UserControl
    {
        public line(Animasyon.DIRECTION dir)
        {
            InitializeComponent();
            Line l = new Line();
            l.X1 = 0;
            l.Y1 = 0;
            l.X2 = 0;
            l.Y2 = 0;

            getDir(dir, l);

            l.Stroke = new SolidColorBrush(Colors.Red);
            l.Fill = new SolidColorBrush(Colors.Red);
            l.StrokeThickness = 3;

            can.Children.Add(l);

        }
        void getDir(Animasyon.DIRECTION dir, Line l)
        {
            if (dir == Animasyon.DIRECTION.Right || dir == Animasyon.DIRECTION.Right_Down || dir == Animasyon.DIRECTION.Right_Up)
            {
                l.X2 = 36;
            }
            else if (dir == Animasyon.DIRECTION.Left || dir == Animasyon.DIRECTION.Left_Down || dir == Animasyon.DIRECTION.Left_Up)
            {
                l.X2 = -36;
            }

            if (dir == Animasyon.DIRECTION.Up || dir == Animasyon.DIRECTION.Left_Up || dir == Animasyon.DIRECTION.Right_Up)
            {
                l.Y2 = -36;
            }
            else if (dir == Animasyon.DIRECTION.Down || dir == Animasyon.DIRECTION.Left_Down || dir == Animasyon.DIRECTION.Right_Down)
            {
                l.Y2 = 36;
            }

        }
    }
}
