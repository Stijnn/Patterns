using ObserverSimple.Observer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for AuctionRoom.xaml
    /// </summary>
    public partial class AuctionRoom : UserControl
    {
        public Random rnd { get; set; }
        public Bidder LastBidder { get; private set; }

        private List<Bidder> bidders = new List<Bidder>();

        private int currentBid;
        public int CurrentBid
        {
            get => currentBid;
            set
            {
                if (value > currentBid)
                {
                    Notify();
                }

                UpdateBid();

                currentBid = value;
            }
        }

        public AuctionRoom()
        {
            InitializeComponent();
            panelBidders.DataContext = bidders;
        }

        public void Start()
        {
            bidders.ForEach(b => b.ellipseStatus.Fill = Brushes.Green);
        }

        public void Add(Bidder bidder)
        {
            bidders.Add(bidder);
            RefreshGrid();
        }

        public void Remove(Bidder bidder)
        {
            bidders.Remove(bidder);
            RefreshGrid();
        }

        public void RemoveAll()
        {
            bidders.Clear();
            RefreshGrid();
        }

        public void Reset()
        {
            currentBid = 0;
            RefreshGrid();
        }

        public int CheckBiddersLeft()
        {
            return bidders.Where(b => b.CashAvailable > currentBid).Count();
        }

        public Bidder SelectBidder()
        {
            List<Bidder> tempBidders = new List<Bidder>();
            tempBidders = bidders.Where(b => b.HandState == HandState.Raised && b.CashAvailable > currentBid).ToList();
            LastBidder = bidders[rnd.Next(0, tempBidders.Count)];

            return LastBidder;
        }

        public void UpdateBid()
        {
            Dispatcher.Invoke(() =>
            {
                if (bidders.Select(b => b.CashAvailable > currentBid).Count() == 1)
                {
                    lblCurrentBid.Content = $"Current Bid of: ${currentBid},-  Go's to {bidders.First(b => b.CashAvailable > currentBid).Name}";
                }
                else
                    lblCurrentBid.Content = $"Current Bid of: ${currentBid},-  Go's to {SelectBidder().Name}";
            });
        }

        private void RefreshGrid()
        {
            bidders.ForEach(b => 
            {
                if (!panelBidders.Children.Contains(b))
                {
                    panelBidders.Children.Add(b);
                }
            });
        }

        private void Notify()
        {
            bidders.ForEach(b => b.Update(currentBid));
        }
    }
}
