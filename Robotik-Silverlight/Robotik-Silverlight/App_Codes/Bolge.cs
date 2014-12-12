using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Robotik_Proje.App_Codes
{
    public class Bolge
    {
        public Button button;
        public List<Bolge> komsular;
        public int i, j;
        public int sayi; // 1 duvar 0 boş
        public bool visited; // ??

        public Bolge(int ii, int jj)
        {
            i = ii;
            j = jj;
            komsular = new List<Bolge>();
            button = new Button();
            button.Click += button_Click;
            sayi = 0;
        }

        void button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            // Duvar atama
            if (this.sayi != 1)
            {
                this.sayi = 1;
                ImageBrush brs = new ImageBrush();
                brs.ImageSource = new BitmapImage(new Uri(@"../../Assets/duvar.png", UriKind.Relative));
                button.Background = brs;
            }
            else if (this.sayi == 1)
            {
                this.sayi = 0;
                ImageBrush brs = new ImageBrush();
                brs.ImageSource = new BitmapImage(new Uri(@"../../Assets/empty.png", UriKind.Relative));
                button.Background = brs;
            }

        }

    }
}
