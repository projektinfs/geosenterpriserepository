﻿using GeosEnterprise.ViewModel;
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
using WpfScheduler;

namespace GeosEnterprise.Views
{
    /// <summary>
    /// Interaction logic for SchedulerPanel.xaml
    /// </summary>
    public partial class SchedulerPanel : UserControl
    {
        public SchedulerPanel()
        {
            InitializeComponent();
            DataContext = new SchedulerPanelViewModel();
        }


        private void scheduler1_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                scheduler1.SelectedDate = DateTime.Now;
                scheduler1.Mode = Mode.Day;
                scheduler1.StartJourney = new TimeSpan(9, 0, 0);
                scheduler1.EndJourney = new TimeSpan(19, 0, 0);
                scheduler1.OnEventDoubleClick += scheduler1_OnEventDoubleClick;
                scheduler1.OnScheduleDoubleClick += scheduler1_OnScheduleDoubleClick;
                scheduler1.Loaded += scheduler1_Loaded;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        void scheduler1_OnScheduleDoubleClick(object sender, DateTime e)
        {
            Console.WriteLine(e.ToShortDateString() + ((FrameworkElement)sender).Name);
            NewEvent window = new NewEvent(e);
            window.ShowDialog();

            if (window.Event != null)
                scheduler1.Events.Add(window.Event);
        }

        void scheduler1_OnEventDoubleClick(object sender, Event e)
        {
            Console.WriteLine(e.Subject);
        }

        private void prevBtn_Click(object sender, RoutedEventArgs e)
        {
            scheduler1.PrevPage();
        }

        private void nextBtn_Click(object sender, RoutedEventArgs e)
        {
            scheduler1.NextPage();
        }

        private void modeMonthBtn_Click(object sender, RoutedEventArgs e)
        {
            scheduler1.Mode = Mode.Month;
        }

        private void modeWeekBtn_Click(object sender, RoutedEventArgs e)
        {
            scheduler1.Mode = Mode.Week;
        }

        private void modeDayBtn_Click(object sender, RoutedEventArgs e)
        {
            scheduler1.Mode = Mode.Day;
        }
    }
}