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
