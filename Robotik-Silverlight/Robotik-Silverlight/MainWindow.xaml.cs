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
using System.Windows.Navigation;
using Robotik_Proje.App_Codes;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Robotik_Silverlight
{
    public partial class MainWindow : Page
    {
        public static Tablo tablo;
        public Image iRobot;
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
            this.Unloaded += MainWindow_Unloaded;
            initTable();
        }

        void MainWindow_Unloaded(object sender, RoutedEventArgs e)
        {
            hakk.Close();
            ayar.Close();
        }

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        // Executes when the user navigates to this page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            
        }
        public void initTable()
        {
            tablo = new Tablo();
            tablo.row = Settings.Default_ROW;
            tablo.column = Settings.Default_COLUMN;
            dragGrid.Visibility = Visibility.Visible;
            robotImg.Visibility = Visibility.Visible;
            label.Content = "• Bölgelere tıklayarak duvar yapabilirsiniz. \n• Robotu ve bayrağı herhangi bir bölgeye atayınız. \n• Başlatmak için Seçenekler -> Başlat tıklayınız.";

            tablo.initBolgeler();
            tablo.initKomsular();

            gridHazirla();
            fillGrid();
        }

        void w1_Closed(object sender, EventArgs e)
        {
            initTable();

        }
        public void gridHazirla()
        {
            for (int i = 0; i < tablo.row; i++)
            {
                RowDefinition rr = new RowDefinition();
                rr.Height = new GridLength(Settings.BolgeSIZE);
                grid1.RowDefinitions.Add(rr);
            }
            for (int j = 0; j < tablo.column; j++)
            {
                ColumnDefinition cc = new ColumnDefinition();
                cc.Width = new GridLength(Settings.BolgeSIZE); //GridLength.Auto;
                grid1.ColumnDefinitions.Add(cc);
            }
            grid1.Width = tablo.column * Settings.BolgeSIZE;
            grid1.Height = tablo.row * Settings.BolgeSIZE;

        }
        public void fillGrid()
        {
            grid1.Children.Clear();
            grid1.ShowGridLines = false;
            for (int i = 0; i < tablo.row; i++)
            {
                for (int j = 0; j < tablo.column; j++)
                {
                    ImageBrush brs = new ImageBrush();
                    brs.ImageSource = new BitmapImage(new Uri(@"../../Assets/empty.png", UriKind.Relative));
                    Button btn = tablo.bolgeler[i, j].button;
                    btn.Margin = new Thickness(0, 0, 0, 0);
                    btn.Background = brs;
                    btn.Height = Settings.BolgeSIZE - 1;
                    btn.Width = Settings.BolgeSIZE - 1;
                    btn.Style = this.Resources["btnStyle"] as Style;
                    btn.Click += btn_Click;
                    grid1.Children.Add(btn);
                    buttonToGrid(btn, i, j);
                }
            }

        }

        void btn_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;


        }
        public void buttonToGrid(Button bb, int x, int y)
        {
            Grid.SetRow(bb, x);
            Grid.SetColumn(bb, y);
        }


        /*  DRAG AND DROP EVENTS  */
        bool validDropRobot = false;
        bool validDropFlag = false;

        private bool isRobotDragging = false;
        double xRobot, yRobot;
        double dropXRobot, dropYRobot;
        private void robotImg_MouseDown(object sender, MouseButtonEventArgs e)
        {
            isRobotDragging = true;
            xRobot = e.GetPosition(dragGrid).X;
            yRobot = e.GetPosition(dragGrid).Y;
            SetPositionRobot();
        }
        private void robotImg_MouseMove(object sender, MouseEventArgs e)
        {
            if (isRobotDragging)
            {
                xRobot = e.GetPosition(dragGrid).X;
                yRobot = e.GetPosition(dragGrid).Y;
                SetPositionRobot();
            }
        }

        private void robotImg_MouseUp(object sender, MouseButtonEventArgs e)
        {
            xRobot = e.GetPosition(dragGrid).X;
            yRobot = e.GetPosition(dragGrid).Y;
            dropXRobot = e.GetPosition(grid1).X;
            dropYRobot = e.GetPosition(grid1).Y;
            isRobotDragging = false;

            int cc = (int)(dropXRobot / Settings.BolgeSIZE);
            int rr = (int)(dropYRobot / Settings.BolgeSIZE);

            if (cc >= 0 && rr >= 0 && cc < tablo.column && rr < tablo.row)
            {
                // valid
                tablo.Start = tablo.bolgeler[rr, cc];
                validDropRobot = true;
                if (validDropFlag)
                {
                    item2.Visibility = Visibility.Visible;
                }
                Canvas.SetLeft(robotImg, Settings.BolgeSIZE * (-tablo.column + cc) - 18);
                Canvas.SetTop(robotImg, Settings.BolgeSIZE * rr);

            }
            else
            {
                // invalid
                validDropRobot = false;
                item2.Visibility = Visibility.Collapsed;

            }

        }
        private void SetPositionRobot()
        {
            //robotImg.Margin = new Thickness(x - robotImg.Width / 2, y - robotImg.Height / 2, 0,0);
            Canvas.SetLeft(robotImg, xRobot - robotImg.Width / 2);
            Canvas.SetTop(robotImg, yRobot - robotImg.Height / 2);
        }


        private bool isFlagDragging = false;
        double xFlag, yFlag;
        double dropXFlag, dropYFlag;
        private void flagImg_MouseDown(object sender, MouseButtonEventArgs e)
        {
            isFlagDragging = true;
            xFlag = e.GetPosition(dragGrid).X;
            yFlag = e.GetPosition(dragGrid).Y;
            SetPositionFlag();
        }

        private void flagImg_MouseMove(object sender, MouseEventArgs e)
        {
            if (isFlagDragging)
            {
                xFlag = e.GetPosition(dragGrid).X;
                yFlag = e.GetPosition(dragGrid).Y;
                SetPositionFlag();
            }
        }

        private void flagImg_MouseUp(object sender, MouseButtonEventArgs e)
        {
            xFlag = e.GetPosition(dragGrid).X;
            yFlag = e.GetPosition(dragGrid).Y;
            dropXFlag = e.GetPosition(grid1).X;
            dropYFlag = e.GetPosition(grid1).Y;
            isFlagDragging = false;

            int cc = (int)(dropXFlag / Settings.BolgeSIZE);
            int rr = (int)(dropYFlag / Settings.BolgeSIZE);

            if (cc >= 0 && rr >= 0 && cc < tablo.column && rr < tablo.row)
            {
                // valid
                tablo.End = tablo.bolgeler[rr, cc];
                validDropFlag = true;
                if (validDropRobot)
                {
                    item2.Visibility = Visibility.Visible; ;
                }
                Canvas.SetLeft(flagImg, Settings.BolgeSIZE * (-tablo.column + cc) - 18);
                Canvas.SetTop(flagImg, Settings.BolgeSIZE * rr);
            }
            else
            {
                // invalid
                validDropFlag = false;
                item2.Visibility = Visibility.Collapsed;
            }
        }
        private void SetPositionFlag()
        {
            Canvas.SetLeft(flagImg, xFlag - flagImg.Width / 2);
            Canvas.SetTop(flagImg, yFlag - flagImg.Height / 2);
        }

        void putiRobot()
        {
            iRobot = new Image();
            iRobot.Width = iRobot.Height = 30;
            iRobot.Source = new BitmapImage(new Uri(@"../../Assets/android.png", UriKind.Relative));
            iRobot.RenderTransformOrigin = new Point(0.5, 0.5);
            TransformGroup tg = new TransformGroup();
            tg.Children.Add(new TranslateTransform());
            iRobot.RenderTransform = tg;

            grid1.Children.Add(iRobot);
            Grid.SetRow(iRobot, tablo.Start.i);
            Grid.SetColumn(iRobot, tablo.Start.j);
        }
        Bolge findNext(Gezinti gez)
        {
            int s = gez.current.sayi;
            foreach (var item in gez.current.komsular)
            {
                if (item.sayi < s && item.sayi != 0 && item.sayi != 1)
                {
                    return item;
                }
            }
            return null;
        }
        void animateCurrentToNext(Gezinti gez)
        {
            Animasyon.DIRECTION dir = Animasyon.DIRECTION.Right;
            if (gez.next.i > gez.current.i) // alt satırda
            {
                if (gez.next.j > gez.current.j)
                    dir = Animasyon.DIRECTION.Right_Down;
                else if (gez.next.j < gez.current.j)
                    dir = Animasyon.DIRECTION.Left_Down;
                else
                    dir = Animasyon.DIRECTION.Down;
            }
            else if (gez.next.i < gez.current.i) // üst satırda
            {
                if (gez.next.j > gez.current.j)
                    dir = Animasyon.DIRECTION.Right_Up;
                else if (gez.next.j < gez.current.j)
                    dir = Animasyon.DIRECTION.Left_Up;
                else
                    dir = Animasyon.DIRECTION.Up;
            }
            else // orta satırda
            {
                if (gez.next.j > gez.current.j)
                    dir = Animasyon.DIRECTION.Right;
                else if (gez.next.j < gez.current.j)
                    dir = Animasyon.DIRECTION.Left;
            }
            Animasyon anim = new Animasyon();
            anim.sb.Completed += delegate
            {
                anim.sb.Stop();
                anim.sb = null;
                {
                    Grid.SetRow(iRobot, gez.next.i);
                    Grid.SetColumn(iRobot, gez.next.j);
                    gez.current = gez.next;
                    //   gez.next = null;                    
                }
            };
            anim.animate(iRobot, dir);
        }
        DispatcherTimer dt;
        void hareketeBasla()
        {
            Gezinti gez = new Gezinti();
            gez.start = tablo.Start;
            gez.end = tablo.End;
            gez.current = tablo.Start;

            dt = new DispatcherTimer();
            dt.Interval = TimeSpan.FromMilliseconds(Settings.HIZ );
            dt.Tick += delegate
            {
                /* iterate */
                Bolge next = findNext(gez);
                if (next != null)
                {
                    gez.next = next;
                    // animate. current -> next
                    animateCurrentToNext(gez);
                }
                else
                {
                    
                    dt.Stop();
                    Alert a = new Alert("Robot hedefe ulaştı.", "Tebrikler");
                    a.WindowStartupLocation = Telerik.Windows.Controls.WindowStartupLocation.CenterOwner;
                    a.Show();
                }
            };
            dt.Start();
        }
        void sayilariYazdir()
        {
            foreach (var item in tablo.bolgeler)
            {
                if (item.sayi != 1)
                {
                    Label lbl = new Label();
                    lbl.Content = item.sayi;
                    lbl.FontSize = 9;
                    lbl.Width = lbl.Height = 11;
                    lbl.Margin = new Thickness(2, 2, 0, 0);
                    lbl.Background = new SolidColorBrush(Colors.Yellow);
                    lbl.HorizontalAlignment = HorizontalAlignment.Left;
                    lbl.VerticalAlignment = VerticalAlignment.Top;
                    lbl.Padding = new Thickness(0);
                    grid1.Children.Add(lbl);
                    Grid.SetRow(lbl, item.i);
                    Grid.SetColumn(lbl, item.j);
                }
            }
        }
        private void item2_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            item2.Visibility = Visibility.Collapsed;
            tablo.End.sayi = 2;

            BFS_Algorithm bfs = new BFS_Algorithm();
            if (bfs.BFSSearch(tablo.End) == null)
            {
                Alert a = new Alert("Robot hedefe ulaşamaz!!!");
                a.WindowStartupLocation = Telerik.Windows.Controls.WindowStartupLocation.CenterOwner;
                a.Show();
            }
            else
            {
                sayilariYazdir();
                robotImg.Visibility = System.Windows.Visibility.Collapsed;
                putiRobot();
                hareketeBasla();
            }
        }
        private void item3_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            if (iRobot != null)
                iRobot.Visibility = Visibility.Collapsed;
            grid1.Children.Clear();
            grid1.RowDefinitions.Clear();
            grid1.ColumnDefinitions.Clear();
            label.Content = " ";

            item2.Visibility = Visibility.Collapsed;

            Canvas.SetLeft(robotImg, 0);
            Canvas.SetTop(robotImg, 0);
            Canvas.SetLeft(flagImg, 0);
            Canvas.SetTop(flagImg, 42);

            dragGrid.Visibility = Visibility.Collapsed;
            tablo.bolgeler = null;
            tablo.Start = null;
            tablo.End = null;
            tablo = null;
            if (dt != null)
            {
                dt.Stop();
            }
            initTable();
        }


        private void close_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            System.Windows.Browser.HtmlPage.Window.Invoke("close");
        }

        Hakkinda hakk = new Hakkinda();
        private void hakkinda_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            hakk.WindowStartupLocation = Telerik.Windows.Controls.WindowStartupLocation.CenterOwner;
            hakk.Show();

        }
        Ayarlar ayar = new Ayarlar();
        private void ayarlar_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            ayar.Closed += ayar_Closed;
            ayar.WindowStartupLocation = Telerik.Windows.Controls.WindowStartupLocation.CenterOwner;
            ayar.Width = 340;
            ayar.Height = 280;
            ayar.Show();
        }

        void ayar_Closed(object sender, Telerik.Windows.Controls.WindowClosedEventArgs e)
        {
            if (dt != null)
                    dt.Interval = TimeSpan.FromMilliseconds(Settings.HIZ + 50);
        }



    }
}
