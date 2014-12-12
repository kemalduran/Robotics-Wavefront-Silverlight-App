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
using Telerik.Windows.Controls;
using Robotik_Proje.App_Codes;

namespace Robotik_Silverlight
{
    public partial class Ayarlar : RadWindow
    {
        public Ayarlar()
        {
            InitializeComponent();
            slider.Value = 15 - Settings.HIZ / 100;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Settings.HIZ = 1500 - 100 * (int)slider.Value; // 500 - 1500
            this.Close();

        }

    }
}
