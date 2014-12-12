using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Robotik_Proje.App_Codes
{
    public class Animasyon
    {
        public Storyboard sb;
        public Animasyon()
        {
            sb = new Storyboard();
            
        }
        public enum DIRECTION
        {
            Left, Right, Up, Down, Left_Up, Left_Down, Right_Up, Right_Down
        }
        public int SIZE = Settings.BolgeSIZE;
        public void animate(Image image, DIRECTION dir)
        {
            DoubleAnimation animX, animY;

            if (dir == DIRECTION.Right || dir == DIRECTION.Left)
            {
                int to = SIZE;
                if (dir == DIRECTION.Left)
                    to = -to;
                animX = new DoubleAnimation(){ From=0, To=to,
                    Duration = TimeSpan.FromMilliseconds(Settings.HIZ)
                };
                Storyboard.SetTarget(animX, image);

                Storyboard.SetTargetProperty(animX, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[0].(TranslateTransform.X)"));
                sb.Children.Add(animX);
            }
            else if (dir == DIRECTION.Down || dir == DIRECTION.Up)
            {
                int to = SIZE;
                if (dir == DIRECTION.Up)
                    to = -to;

                animY = new DoubleAnimation() {
                    From = 0,
                    To = to,
                    Duration = TimeSpan.FromMilliseconds(Settings.HIZ)
                };
                Storyboard.SetTarget(animY, image); 
                Storyboard.SetTargetProperty(animY, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[0].(TranslateTransform.Y)"));
                sb.Children.Add(animY);
            }
            else
            {
                int toX = SIZE, toY=SIZE;
                if (dir == DIRECTION.Left_Down || dir == DIRECTION.Left_Up)
                    toX = -toX;
                if (dir == DIRECTION.Left_Up || dir == DIRECTION.Right_Up)
                    toY = -toY;


                animX = new DoubleAnimation() {
                    From = 0,
                    To = toX,
                    Duration = TimeSpan.FromMilliseconds(Settings.HIZ)
                };
                Storyboard.SetTarget(animX, image);
                Storyboard.SetTargetProperty(animX, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[0].(TranslateTransform.X)"));
                sb.Children.Add(animX);

                animY = new DoubleAnimation() {
                    From = 0,
                    To = toY,
                    Duration = TimeSpan.FromMilliseconds(Settings.HIZ)
                };
                Storyboard.SetTarget(animY, image);
                Storyboard.SetTargetProperty(animY, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[0].(TranslateTransform.Y)"));
                sb.Children.Add(animY);
            }
            sb.Begin();
        }

    }
}
