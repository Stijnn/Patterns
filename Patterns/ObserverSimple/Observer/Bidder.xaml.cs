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

namespace ObserverSimple.Observer
{
    /// <summary>
    /// Interaction logic for Bidder.xaml
    /// </summary>
    public partial class Bidder : UserControl
    {
        private int cashAvailable;
        public int CashAvailable { get => cashAvailable; private set => cashAvailable = value; }

        public HandState HandState { get; private set; }

        private string name;
        public string Name { get => name; private set => name = value; }

        public Bidder(int cashAvailable, string name)
        {
            InitializeComponent();
            this.cashAvailable = cashAvailable;
            Name = name;
            lblNumber.Content = name;
        }

        public void Update(int currentBid)
        {
            if (cashAvailable < currentBid)
            {
                ellipseStatus.Dispatcher.Invoke(() =>
                {
                    ellipseStatus.Fill = Brushes.Red;
                    HandState = HandState.Lowered;
                });
            }
            else
            {
                ellipseStatus.Dispatcher.Invoke(() =>
                {
                    ellipseStatus.Fill = Brushes.Green;
                    HandState = HandState.Raised;
                });
            }
        }
    }

    public enum HandState
    {
        Lowered,
        Raised,
    }
}
