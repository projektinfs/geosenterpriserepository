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
using GeosEnterprise.ViewModel;
using GeosEnterprise.Repositories;
using System.ComponentModel;

namespace GeosEnterprise.Views
{

    public partial class LogsList : UserControl
    {
        public LogsList()
        {
            InitializeComponent();
            this.DataContext = new LogsListViewModel();

        }
    }
}

