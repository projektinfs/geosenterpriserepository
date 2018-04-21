﻿using System;
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
using GeosEnterprise.DBO;
using GeosEnterprise.DTO;
using GeosEnterprise.Repositories;
using GeosEnterprise.ViewModels;

namespace GeosEnterprise.Views
{
    /// <summary>
    /// Interaction logic for ComputersList.xaml
    /// </summary>
    public partial class ComputersList : UserControl
    {
        public ComputersList()
        {
            InitializeComponent();
        }

        private void SearchBar_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox SearchBar = (TextBox)sender;
            SearchBar.Text = string.Empty;
            SearchBar.GotFocus -= SearchBar_GotFocus;
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            TimeFrom.Text = null;
            TimeTo.Text = null;
        }

        private void DateTimeNowButton_Click(object sender, RoutedEventArgs e)
        {
            TimeTo.Value = DateTime.Now;
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            if (SearchBar.Text == "Wpisz tekst...")
            {
                if (TimeFrom.Text == null || TimeTo.Text == null)
                {
                    RepairsList.ItemsSource = RepairsRepository.GetAllCurrent();
                }
                else
                {
                    RepairsList.ItemsSource = RepairsRepository.GetByTime(TimeFrom.Value, TimeTo.Value);
                }
            }
            else
            {
                if (TimeFrom.Text == null || TimeTo.Text == null)
                {
                    RepairsList.ItemsSource = RepairsRepository.GetByDescription(SearchBar.Text);
                }
                else
                {
                    RepairsList.ItemsSource = RepairsRepository.GetByTimeAndDescription(SearchBar.Text, TimeFrom.Value, TimeTo.Value);
                }
            }

        }
    }
}