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

namespace ObserverSimple.Subject
{
    /// <summary>
    /// Interaction logic for Auctioneer.xaml
    /// </summary>
    public partial class Auctioneer : UserControl
    {
        private LinkedList<>

        private int currentBid;
        public int CurrentBid
        {
            get => currentBid;
            set
            {
                currentBid = value;
                Notify();
            }
        }

        public Auctioneer()
        {
            InitializeComponent();
        }

        private void Notify()
        {

        }
    }
}
