using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;
using Robotik_Proje.App_Codes;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Robotik_Silverlight.App_Codes;
using Robotik_Silverlight.App_Controls;

namespace Robotik_Silverlight
{
    public partial class MainWindow : Page
    {
        public static Tablo tablo;
        public static bool isAnimBegin = false;
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
            putPacMans();
            beginPacManAnimation();
        }

        // Executes when the user navigates to this page.
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            
        }
        Pacman pac1, pac2, pac3;
        void putPacMans()
        {
            pac1 = new Pacman(3, 3, @"../../Assets/pac1-sag.png");
            pac2 = new Pacman(9, 3, @"../../Assets/pac2-sol.png");
            pac3 = new Pacman(2, 14, @"../../Assets/pac3-sol.png");

            pac1.setBolgesi(tablo.bolgeler[3, 3], tablo.bolgeler[3, 4], tablo.bolgeler[3, 5],
                tablo.bolgeler[3, 6], tablo.bolgeler[3, 7], tablo.bolgeler[3, 8], tablo.bolgeler[3, 9]);

            pac2.setBolgesi(tablo.bolgeler[9, 3], tablo.bolgeler[9, 4], tablo.bolgeler[9, 5],
                tablo.bolgeler[9, 6], tablo.bolgeler[9, 7], tablo.bolgeler[9, 8], tablo.bolgeler[9, 9]);

            pac3.setBolgesi(tablo.bolgeler[2, 14], tablo.bolgeler[3, 14], tablo.bolgeler[4, 14],
                tablo.bolgeler[5, 14], tablo.bolgeler[6, 14], tablo.bolgeler[7, 14], tablo.bolgeler[8, 14], 
                tablo.bolgeler[9, 14], tablo.bolgeler[10, 14]);

            pac1.Point1 = tablo.bolgeler[3, 3];
            pac1.Point2 = tablo.bolgeler[3, 9];

            pac2.Point1 = tablo.bolgeler[9, 3];
            pac2.Point2 = tablo.bolgeler[9, 9];

            pac3.Point1 = tablo.bolgeler[2, 14];
            pac3.Point2 = tablo.bolgeler[10, 14];

            putPacMan(pac1, pac1.Point1);
            putPacMan(pac2, pac2.Point2);
            putPacMan(pac3, pac3.Point1);

            pac1.LeftRight = true;
            pac2.LeftRight = true;
            pac3.UpDown = true;
            pac1.CurrentDirection = Animasyon.DIRECTION.Right;
            pac2.CurrentDirection = Animasyon.DIRECTION.Left;
            pac3.CurrentDirection = Animasyon.DIRECTION.Down;

            pac1.current = pac1.Point1;
            pac2.current = pac2.Point2;
            pac3.current = pac3.Point1;

        }
        void putPacMan(Pacman p, Bolge bas)
        {
            grid1.Children.Add(p.image);
            Grid.SetRow(p.image, bas.i);
            Grid.SetColumn(p.image, bas.j);
        }
        DispatcherTimer dt_pac;
        void beginPacManAnimation()
        {
            dt_pac = new DispatcherTimer();
            dt_pac.Interval = TimeSpan.FromMilliseconds(Settings.HIZ_PAC);
            dt_pac.Tick += delegate
            {
                Bolge next1 = findNextPac(pac1);
                if (next1 != null)
                {
                    make0AllPac(pac1);
                    pac1.next = next1;
                    pac1.current.isPac = true;
                    pac1.next.isPac = true;
                    if (next1 != pac1.Point1 && next1 != pac1.Point2)
                    {
                        Bolge next_next = findNextNextPac(pac1);
                        next_next.isPac = true;
                    }

                    animatePac(pac1);
                }
                Bolge next2 = findNextPac(pac2);
                if (next2 != null)
                {
                    make0AllPac(pac2);
                    pac2.next = next2;
                    pac2.current.isPac = true;
                    pac2.next.isPac = true;
                    if (next2 != pac2.Point1 && next2 != pac2.Point2)
                    {
                        Bolge next_next = findNextNextPac(pac2);
                        next_next.isPac = true;
                    }
                        animatePac(pac2);
                }
                Bolge next3 = findNextPac(pac3);
                if (next3 != null)
                {
                    make0AllPac(pac3);
                    pac3.next = next3;
                    pac3.current.isPac = true;
                    pac3.next.isPac = true;
                    if (next3 != pac3.Point1 && next3 != pac3.Point2)
                    {
                        Bolge next_next = findNextNextPac(pac3);
                        next_next.isPac = true;
                    }
                    animatePac(pac3);
                }
            };
            dt_pac.Start();
        }
        void make0AllPac(Pacman p)
        {
            foreach (var bb in p.bolgesi)
            {
                bb.isPac = false;
            }
        }
        void animatePac(Pacman pac)
        {
            Animasyon.DIRECTION dir = pac.CurrentDirection;
            Animasyon anim = new Animasyon();
            anim.sb.Completed += delegate
            {
                anim.sb.Stop();
                anim.sb = null;
                {
                    Grid.SetRow(pac.image, pac.next.i);
                    Grid.SetColumn(pac.image, pac.next.j);
                    pac.current = pac.next;
                }
            };
            anim.animate(pac.image, dir, Settings.HIZ_PAC);

        }
        Bolge findNextNextPac(Pacman p)
        {
            if (p.LeftRight)
            {
                if (p.CurrentDirection == Animasyon.DIRECTION.Right)
                {
                    if (p.current.Equals(p.Point2))
                    { 
                        return p.current.komsular[3].komsular[3]; // soldaki komşu
                    }
                    return p.current.komsular[0].komsular[0]; // sağdaki komşu
                }
                else if (p.CurrentDirection == Animasyon.DIRECTION.Left)
                {
                    if (p.current.Equals(p.Point1))
                    { 
                        return p.current.komsular[0].komsular[0]; // sağdaki komşu
                    }
                    return p.current.komsular[3].komsular[3]; // soldaki komşu
                }
            }
            if (p.UpDown)
            {
                if (p.CurrentDirection == Animasyon.DIRECTION.Down)
                {
                    if (p.current.Equals(p.Point2))
                    { 
                        return p.current.komsular[2].komsular[2]; // üstdaki komşu
                    }
                    return p.current.komsular[1].komsular[1]; // altdaki komşu
                }
                else if (p.CurrentDirection == Animasyon.DIRECTION.Up)
                {
                    if (p.current.Equals(p.Point1))
                    { 
                        return p.current.komsular[1].komsular[1]; // altdaki komşu
                    }
                    return p.current.komsular[2].komsular[2]; // üstdaki komşu
                }
            }
            return null;
        }
        Bolge findNextPac(Pacman p)
        {
            if (p.LeftRight)
            {
                if (p.CurrentDirection == Animasyon.DIRECTION.Right)
                {
                    if (p.current.Equals(p.Point2))
                    { // sola dön
                        p.CurrentDirection = Animasyon.DIRECTION.Left;
                        setPacImage(p);
                        return p.current.komsular[3]; // soldaki komşu
                    }
                    return p.current.komsular[0]; // sağdaki komşu
                }
                else if (p.CurrentDirection == Animasyon.DIRECTION.Left)
                {
                    if (p.current.Equals(p.Point1))
                    { // sağa dön
                        p.CurrentDirection = Animasyon.DIRECTION.Right;
                        setPacImage(p);
                        return p.current.komsular[0]; // sağdaki komşu
                    }
                    return p.current.komsular[3]; // soldaki komşu
                }
            }
            if (p.UpDown)
            {
                if (p.CurrentDirection == Animasyon.DIRECTION.Down)
                {
                    if (p.current.Equals(p.Point2))
                    { // yukarı dön
                        p.CurrentDirection = Animasyon.DIRECTION.Up;
                        return p.current.komsular[2]; // üstdaki komşu
                    }
                    return p.current.komsular[1]; // altdaki komşu
                }
                else if (p.CurrentDirection == Animasyon.DIRECTION.Up)
                {
                    if (p.current.Equals(p.Point1))
                    { // aşağı dön
                        p.CurrentDirection = Animasyon.DIRECTION.Down;
                        return p.current.komsular[1]; // altdaki komşu
                    }
                    return p.current.komsular[2]; // üstdaki komşu
                }
            }
            return null;
        }
        void setPacImage(Pacman pac)
        {
            if (pac.Equals(pac1))
            {
                if(pac.CurrentDirection == Animasyon.DIRECTION.Left)
                    pac.image.Source = new BitmapImage(new Uri(@"../../Assets/pac1-sol.png", UriKind.Relative));
                else if (pac.CurrentDirection == Animasyon.DIRECTION.Right)
                    pac.image.Source = new BitmapImage(new Uri(@"../../Assets/pac1-sag.png", UriKind.Relative));
            }
            else if (pac.Equals(pac2))
            {
                if (pac.CurrentDirection == Animasyon.DIRECTION.Left)
                    pac.image.Source = new BitmapImage(new Uri(@"../../Assets/pac2-sol.png", UriKind.Relative));
                else if (pac.CurrentDirection == Animasyon.DIRECTION.Right)
                    pac.image.Source = new BitmapImage(new Uri(@"../../Assets/pac2-sag.png", UriKind.Relative));
            }

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
            if (isAnimBegin)
                return;
            isRobotDragging = true;
            xRobot = e.GetPosition(dragGrid).X;
            yRobot = e.GetPosition(dragGrid).Y;
            SetPositionRobot();
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
                if (!controlDropOnPac(tablo.bolgeler[rr, cc]))
                    validDropRobot = false;
                else
                    validDropRobot = true;
            }
            else
            {
                // invalid
                validDropRobot = false;
            }

            if (validDropRobot)
            {
                // valid
                tablo.Start = tablo.bolgeler[rr, cc];
                if (validDropFlag)
                {
                    item2.Visibility = Visibility.Visible;
                }
                Canvas.SetLeft(robotImg, Settings.BolgeSIZE * (-tablo.column + cc) - 18);
                Canvas.SetTop(robotImg, Settings.BolgeSIZE * rr);
            }
            else
            {
                Canvas.SetLeft(robotImg,  - 18);
                Canvas.SetTop(robotImg, 0);

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
            if (isAnimBegin)
                return;
            isFlagDragging = true;
            xFlag = e.GetPosition(dragGrid).X;
            yFlag = e.GetPosition(dragGrid).Y;
            SetPositionFlag();
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
                if (!controlDropOnPac(tablo.bolgeler[rr, cc]))
                    validDropFlag = false;
                else
                    validDropFlag = true;
            }
            else{
                validDropFlag = false;
            }
            if(validDropFlag){
                 // valid
                tablo.End = tablo.bolgeler[rr, cc];
                if (validDropRobot)
                {
                    item2.Visibility = Visibility.Visible; ;
                }
                Canvas.SetLeft(flagImg, Settings.BolgeSIZE * (-tablo.column + cc) - 18);
                Canvas.SetTop(flagImg, Settings.BolgeSIZE * rr);
            }
            else{
                Canvas.SetLeft(flagImg, -18);
                Canvas.SetTop(flagImg, 42);
                item2.Visibility = Visibility.Collapsed;
            }
        }
        private void SetPositionFlag()
        {
            Canvas.SetLeft(flagImg, xFlag - flagImg.Width / 2);
            Canvas.SetTop(flagImg, yFlag - flagImg.Height / 2);
        }
        bool controlDropOnPac(Bolge bb)
        {
            foreach (var p1 in pac1.bolgesi)
            {
                if (p1.Equals(bb))
                    return false;
            }
            foreach (var p1 in pac2.bolgesi)
            {
                if (p1.Equals(bb))
                    return false;
            }
            foreach (var p1 in pac3.bolgesi)
            {
                if (p1.Equals(bb))
                    return false;
            }
            return true;
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
                if (item.sayi == 2)
                    return item;

                if (item.sayi < s && item.sayi != 0 && item.sayi != 1 && !item.isPac)
                {
                    return item;
                }
            }
            return null;
        }
        void animateCurrentToNext(Gezinti gez)
        {
            // numaralara bakarak bir sonraki yeri hesapla ve git
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
                line ll = new line(dir);
                grid1.Children.Add(ll);
                Grid.SetRow(ll, gez.current.i);
                Grid.SetColumn(ll, gez.current.j);

                anim.sb.Stop();
                anim.sb = null;
                {
                    Grid.SetRow(iRobot, gez.next.i);
                    Grid.SetColumn(iRobot, gez.next.j);
                    gez.current = gez.next;
                    //   gez.next = null;                    
                }

            };
            anim.animate(iRobot, dir, Settings.HIZ);
        }
        DispatcherTimer dt;
        Gezinti gez;
        void hareketeBasla()
        {
            dt = new DispatcherTimer();
            dt.Interval = TimeSpan.FromMilliseconds(Settings.HIZ );
            dt.Tick += delegate
            {

                /* iterate */
                Bolge next = findNext(gez);
                if (next.sayi == 2)
                {
                    dt.Stop();
                    gez.next = next;

                    // animate. current -> next
                    animateCurrentToNext(gez);
                    {
                        DispatcherTimer bit = new DispatcherTimer();
                        bit.Interval = TimeSpan.FromMilliseconds(Settings.HIZ);
                        bit.Tick += delegate
                        {
                            bit.Stop();
                            Alert a = new Alert("Robot hedefe ulaştı.", "Tebrikler");
                            a.WindowStartupLocation = Telerik.Windows.Controls.WindowStartupLocation.CenterOwner;
                            a.Show();
                        };
                        bit.Start();
                    }
                }
                else if (next != null)
                {
                    gez.next = next;

                    // animate. current -> next
                    animateCurrentToNext(gez);

                    // numaraları tekrar ata
                    BFS_Algorithm bfs = new BFS_Algorithm();
                    foreach (var bb in tablo.bolgeler)
                    {
                        if (bb.sayi != 1 && bb.sayi != 2)
                        {
                            bb.sayi = 0;
                        }
                    }
                    bfs.BFSSearch(gez);
                    
                  //  sayilariYazdir();
                }
                else if(next == null)
                {
                    // bekle ve geç


                }
            };
            dt.Start();
        }
        
        private void item2_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            isAnimBegin = true;
            item2.Visibility = Visibility.Collapsed;
            tablo.End.sayi = 2;

            gez = new Gezinti();
            gez.start = tablo.Start;
            gez.end = tablo.End;
            gez.current = tablo.Start;

            BFS_Algorithm bfs = new BFS_Algorithm();
            if (bfs.BFSSearch(gez) == null) // sayılar burada atandı
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
        // sıfırla
        private void item3_Click(object sender, Telerik.Windows.RadRoutedEventArgs e)
        {
            isAnimBegin = false;
            gez = null;
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
            dt_pac.Stop();
            dt_pac = null;
            initTable();
            pac1 = null;
            pac2 = null;
            pac3 = null;
            putPacMans();
            beginPacManAnimation();
        }
        void sayilariYazdir()
        {
            foreach (var item in tablo.bolgeler)
            {
                if (item.sayi != 1)
                {
                    item.label = new Label();
                    item.label.Content = item.sayi;
                    item.label.FontSize = 9;
                    item.label.Width = item.label.Height = 11;
                    item.label.Margin = new Thickness(2, 2, 0, 0);
                    item.label.Background = new SolidColorBrush(Colors.Yellow);
                    item.label.HorizontalAlignment = HorizontalAlignment.Left;
                    item.label.VerticalAlignment = VerticalAlignment.Top;
                    item.label.Padding = new Thickness(0);
                    if(!grid1.Children.Contains(item.label))
                        grid1.Children.Add(item.label);
                    Grid.SetRow(item.label, item.i);
                    Grid.SetColumn(item.label, item.j);
                }
            }
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
                    dt.Interval = TimeSpan.FromMilliseconds(Settings.HIZ);
            if (dt_pac != null)
            {
                dt_pac.Interval = TimeSpan.FromMilliseconds(Settings.HIZ_PAC);
            }
        }

        private void anaGrid_MouseMove(object sender, MouseEventArgs e)
        {
            if (isRobotDragging)
            {
                Point pp = e.GetPosition(dragGrid);
                xRobot = pp.X;
                yRobot = pp.Y;
                SetPositionRobot();
            }
            else if (isFlagDragging)
            {
                Point pp = e.GetPosition(dragGrid);
                xFlag = pp.X;
                yFlag = pp.Y;
                SetPositionFlag();
            }
        }
    }
}
