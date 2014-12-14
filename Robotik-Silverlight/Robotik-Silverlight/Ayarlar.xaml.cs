using System.Windows;
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
            slider2.Value = 15 - Settings.HIZ_PAC / 100;            
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Settings.HIZ = 1500 - 100 * (int)slider.Value; // 500 - 1500
            Settings.HIZ_PAC = 1500 - 100 * (int)slider2.Value; // 500 - 1500
            this.Close();

        }

    }
}
