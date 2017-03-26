using ObserverSimple.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ObserverSimple
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Bidder> bidders;

        public MainWindow()
        {
            InitializeComponent();
            bidders = new List<Bidder>();
        }

        private void OnClickStart(object sender, RoutedEventArgs e)
        {

        }

        private void OnClickGenerate(object sender, RoutedEventArgs e)
        {

        }
        private void OnClickReset(object sender, RoutedEventArgs e)
        {

        }
    }
}
