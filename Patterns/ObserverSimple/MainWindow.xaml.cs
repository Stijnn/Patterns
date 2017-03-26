using ObserverSimple.Observer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
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
        Random rnd = new Random();
        Timer nextBid = new Timer();

        public MainWindow()
        {
            InitializeComponent();
            nextBid = new Timer(2000);
            nextBid.Elapsed += OnNewBid;
            AuctionRoom.rnd = this.rnd;
        }

        private void OnNewBid(object sender, ElapsedEventArgs e)
        {
            if (AuctionRoom.CheckBiddersLeft() <= 1)
            {
                AuctionRoom.UpdateBid();
                //if (AuctionRoom.CheckBiddersLeft() == 1)
                //{
                //    MessageBox.Show($"AND SOLD TO {AuctionRoom.SelectBidder().Name}");
                //}
                //else
                //{
                //    MessageBox.Show($"AND SOLD TO {AuctionRoom.LastBidder.Name}");
                //}

                nextBid.Stop();
                this.Dispatcher.Invoke(() =>
                {
                    bttnStart.IsEnabled = bttnReset.IsEnabled = bttnGenerate.IsEnabled = true;
                });
            }

            AuctionRoom.CurrentBid += 50;
        }

        private void OnClickStart(object sender, RoutedEventArgs e)
        {
            bttnStart.IsEnabled = bttnReset.IsEnabled = bttnGenerate.IsEnabled = false;
            AuctionRoom.Start();
            nextBid.Start();
        }

        private void OnClickGenerate(object sender, RoutedEventArgs e)
        {
            int parsed;
            if (int.TryParse(txtAmount.Text, out parsed))
                if (AuctionRoom.panelBidders.Children.Count == 0 && parsed < 1001)
                    for (int i = 1; i <= parsed; i++)
                    {
                        Bidder bidder;
                        if (i == 1)
                            bidder = new Bidder(rnd.Next(1, 2), i.ToString());
                        else
                            bidder = new Bidder(rnd.Next(1, 20000), i.ToString());

                        bidder.Margin = new Thickness(1);
                        bidder.Width = 50;
                        bidder.Height = 45;
                        AuctionRoom.Add(bidder);
                    }
                else
                    MessageBox.Show("Oops! The room still contains bidders make sure to press reset before adding bidders");
            else
                MessageBox.Show("Invalid Number: Make sure that the textbox does not contain characters or symbols.");
        }

        private void OnClickReset(object sender, RoutedEventArgs e)
        {
            MessageBoxResult res = MessageBox.Show("Do you want to clear the current list of bidders aswell?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            switch (res)
            {
                case MessageBoxResult.Yes:
                    AuctionRoom.Reset();
                    AuctionRoom.RemoveAll();
                break;

                case MessageBoxResult.No:
                    AuctionRoom.Reset();
                break;

                default:
                    MessageBox.Show("NO MORE!", "Baby dont hurt me!");
                break;
            }
        }

        private void OnValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            nextBid.Interval = sliderInterval.Value;
        }
    }
}
