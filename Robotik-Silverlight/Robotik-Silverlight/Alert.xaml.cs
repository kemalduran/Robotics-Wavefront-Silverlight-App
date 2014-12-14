using System.Windows;
using Telerik.Windows.Controls;

namespace Robotik_Silverlight
{
    public partial class Alert : RadWindow
    {
        public Alert( string message )
        {
            InitializeComponent();
            mesaj.Content = message;
        }
        public Alert(string message, string title)
        {
            InitializeComponent();
            this.Header = title;
            mesaj.Content = message;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
