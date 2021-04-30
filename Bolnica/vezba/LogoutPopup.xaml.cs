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
using System.Windows.Shapes;

namespace vezba
{
    public partial class LogoutPopup : Window
    {
        public LogoutPopup()
        {
            InitializeComponent();
        }

        private void btnDa_Click(object sender, RoutedEventArgs e)
        {
            var s = new MainWindow();
            s.Show();
            this.Close();
        }

        private void btnNe_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
