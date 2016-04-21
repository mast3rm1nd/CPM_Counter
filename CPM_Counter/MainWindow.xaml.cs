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

using System.Diagnostics;
using System.Timers;

namespace CPM_Counter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static ulong clicksPerSecond = 0;
        static ulong clicksMade = 0;
        static double maxCPS = double.MinValue;
        static bool isGameStarted = false;

        static Stopwatch timer = new Stopwatch();

        Timer checkForTime = new Timer(1000);

        public MainWindow()
        {
            InitializeComponent();

            checkForTime.Elapsed += new ElapsedEventHandler(RefreshValues);
        }

        private void RefreshValues(object sender, ElapsedEventArgs e)
        {
            var test = timer.ElapsedMilliseconds;
            //var currentCPS = clicksPerSecond / (timer.ElapsedMilliseconds / (double)1000);
            var currentCPS = clicksPerSecond;

            //timer.Restart();
            clicksPerSecond = 0;

            //if (timer.ElapsedMilliseconds > 1000)
            Dispatcher.BeginInvoke(new Action(delegate ()
            {
                Label_CPS.Content = String.Format("{0:F1}", currentCPS);

                if(currentCPS > maxCPS)
                {
                    maxCPS = currentCPS;
                    Label_CPS_Max.Content = String.Format("{0:F1}", currentCPS);
                }
            }));
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            clicksPerSecond++;
            clicksMade++;

            Label_Clicks.Content = clicksMade.ToString();

            if(!isGameStarted)
            {
                isGameStarted = true;

                checkForTime.Enabled = true;
                timer.Start();

                Button_Refresh.IsEnabled = true;
            }
            
        }

        private void Button_Refresh_Click(object sender, RoutedEventArgs e)
        {
            isGameStarted = false;

            checkForTime.Enabled = false;

            timer.Reset();
            timer.Stop();

            clicksPerSecond = 0;
            clicksMade = 0;
            maxCPS = double.MinValue;

            Label_Clicks.Content = "";
            Label_CPS.Content = "";
            Label_CPS_Max.Content = "";


            Button_Refresh.IsEnabled = false;
        }
    }
}
