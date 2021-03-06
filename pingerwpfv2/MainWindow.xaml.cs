﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net.NetworkInformation;
using System.Threading;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;

namespace pingerwpfv2
{

    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static List<int> pings = new List<int>();
        static List<int> highpings = new List<int>();
        DateTime begin = DateTime.Now;
        TimeSpan duration = new TimeSpan();

        decimal pingcount = 0;
        long pingadder = 0;
        int pingmedian = 0;
        int pingbest = 0;
        int pingworst = 0;
        long pingjumps=0;
        long pinghigh = 0;
        double highpingPercentage = 0;
        int lastping =1337420;
        int timeouts=0;
        int highpingmedian=0;
        float pingjumpseverysecond = 0;
        float timeoutseveryminute = 0;


        bool protocol=false;
        
        public MainWindow()
        {
            this.WindowState = WindowState.Minimized;

            InitializeComponent();

            DispatcherTimer timer2 = new DispatcherTimer();
            timer2.Interval = TimeSpan.FromSeconds(15);
            timer2.Tick += timer2_Tick;
            timer2.Start();

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(800);
            timer.Tick += timer_Tick;
            timer.Start();

            DispatcherTimer timer3 = new DispatcherTimer();
            timer3.Interval = TimeSpan.FromSeconds(1);
            timer3.Tick += timer3_Tick;
            timer3.Start();
            
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            try
            {
                double ping = PingTime("8.8.8.8");
                pings.Add((int)ping);
                if (ping != 1337420) lPing.Content = ping;
                else
                {
                    lPing.Content = "Connection timeout";
                    timeouts++;
                    lPingTimeouts.Content = timeouts;
                }

                if (ping>lastping+50) 
                {
                    pingjumps++;
                }
                lPingJumps.Content = pingjumps;

                if (ping > 90)
                {
                    pinghigh++;
                    if (ping != 1337420) highpings.Add((int)ping);
                }
                //Console.WriteLine(pinghigh);
                
                if(ping!=1337420)pingadder += (long)ping;
                pingcount++;
                pingmedian = (int)(pingadder / pingcount);
                lPingMedian.Content = pingmedian;

                ChangeColorOfLabel(lPing, ping);
                ChangeColorOfLabel(lPingMedian, pingmedian);
                lastping = (int)ping;
            }
            catch (Exception end)
            {
                MessageBox.Show(end.Message);
                
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            List<int> pinglist = pings;

            while (pinglist.IndexOf(1337420) != -1) pinglist.Remove(1337420);

            pinglist.Sort();
            lPingBest.Content = pinglist[0];
            lPingWorst.Content = pinglist[pinglist.Count - 1];            

            pingbest= pinglist[0];
            pingworst= pinglist[pinglist.Count - 1];
            
            ChangeColorOfLabel(lPingBest, pinglist[0]);
            ChangeColorOfLabel(lPingWorst, pinglist[pinglist.Count - 1]);
                        
            if (highpings.Count > 0)
            {
                for (int i = 0; i < highpings.Count; i++)
                {
                    highpingmedian = highpingmedian + highpings[i];
                }
                highpingmedian = highpingmedian / highpings.Count;
                lPingHighPingMedian.Content = highpingmedian;
            }
            
            if(timeouts>0)
            {
                timeoutseveryminute = (float)(duration.TotalMinutes / timeouts);
                lPingTimeoutsMinute.Content = "Timeout every "+ timeoutseveryminute.ToString("F1") + " minutes";
            }          
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            duration = DateTime.Now.Subtract(begin);
            lDuration.Content = duration.Hours + ":" + duration.Minutes + ":" + duration.Seconds;
            
            pingjumpseverysecond = (float)duration.TotalSeconds / pingjumps;
            if (duration.TotalSeconds > 0 && pingjumps>0) lPingJumpsSecond.Content = "jump every " + pingjumpseverysecond.ToString("F1") + " seconds";
            if (pingjumpseverysecond > 2700) lPingJumpsSecond.Foreground = Brushes.Green;
            else lPingJumpsSecond.Foreground = Brushes.Yellow;
            if(pingjumpseverysecond < 600) lPingJumpsSecond.Foreground = Brushes.Red;


            highpingPercentage =(double)(100 / pingcount * pinghigh);
            lPingHigh.Content = highpingPercentage.ToString("F1") + "%";

            if (highpingPercentage < 0.6) lPingHigh.Foreground = Brushes.Green;
            else if (highpingPercentage > 8.3) lPingHigh.Foreground = Brushes.Red;
            else lPingHigh.Foreground = Brushes.Yellow;
        }

        private void bDocumentation_Click(object sender, RoutedEventArgs e)
        {
            if (protocol)
            {
                protocol = false;
                bDocumentation.Content = "Documenting pings: off";
            }
            else
            {
                protocol = true;
                bDocumentation.Content = "Documenting pings: on";
            }
        }


        private void ChangeColorOfLabel(System.Windows.Controls.Label lab, double ping)
        {
            if (ping < 50) lab.Foreground = Brushes.Green;
            else if (ping < 70) lab.Foreground = Brushes.White;
            else if (ping > 70 && ping < 90) lab.Foreground = Brushes.Yellow;
            else if (ping > 90) lab.Foreground = Brushes.Red;

        }

        public static double PingTime(string host)
        {
            long totalTime = 0;
            Ping pingSender = new Ping();

           
            PingReply reply = pingSender.Send(host);
            if (reply.Status == IPStatus.Success)
            {
                totalTime += reply.RoundtripTime;
            }
            else totalTime = 1337420;

            return totalTime;
        }
        
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            if(protocol)
            {
                string filename = DateTime.Today.Year + "-" + DateTime.Today.Month + "-" + DateTime.Today.Day + "-" + DateTime.Now.Hour + DateTime.Now.Minute + DateTime.Now.Second + ".txt";
                StreamWriter sw = File.AppendText(filename);

                TimeSpan duration = DateTime.Now.Subtract(begin);

                sw.WriteLine("Duration(d:h:m:s): " + duration.Days + ":"+duration.Hours + ":" + duration.Minutes + ":" + duration.Seconds);
                sw.WriteLine("Pings: " + pings.Count);
                sw.WriteLine("Best: " + pingbest);
                sw.WriteLine("Worst: " + pingworst);
                sw.WriteLine("Average: " + pingmedian);
                sw.WriteLine("Jumps: " + pingjumps);
                sw.WriteLine("Jump every " + pingjumpseverysecond + " seconds");
                sw.WriteLine("Highping Percentage: " + highpingPercentage);
                if(highpings.Count>0)sw.WriteLine("Average Highping: " + highpingmedian);
                else sw.WriteLine("Average Highping: -");
                sw.WriteLine("Timeouts: " + timeouts);
                sw.WriteLine("Timeout every "+timeoutseveryminute.ToString("F1") + " minute/s");
                sw.WriteLine("------------------------------");

                for (int i = 0; i < pings.Count; i++)
                {
                    if(pings[i]!=1337420)sw.WriteLine(pings[i]);
                    else sw.WriteLine("Connection timeout");
                }

                sw.Flush();
            }
            base.OnClosing(e);
        }

        private void bCopyBrief_Click(object sender, RoutedEventArgs e)
        {
            string brief = "Average: " + pingmedian + ", Worst: " + pingworst + ", Highping Percentage: " + highpingPercentage.ToString("F1") + "%, Average Highping: " + highpingmedian + ", ping jump every " + pingjumpseverysecond.ToString("F1") + " second/s, timeout every " + timeoutseveryminute.ToString("F1") + " minute/s";
            Clipboard.SetText(brief);
        }
    }
}
