﻿using System.Windows;

namespace vezba
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Username.Text.Equals("sekretar") && Password.Password.Equals("bolnica"))
            {
                var s = new SecretaryView();
                s.Show();

                this.Close();
            }
            else if (Username.Text.Equals("upravnik") && Password.Password.Equals("bolnica"))
            {
                var s = new ManagerMainView();
                s.Show();

                this.Close();
            }
            else if (Username.Text.Equals("lekar") && Password.Password.Equals("bolnica"))
            {
                var s = new DoctorView();
                s.Show();

                this.Close();
            }
            else if (Username.Text.Equals("pacijent") && Password.Password.Equals("bolnica"))
            {
                var s = new PatientView();
                s.Show();

                this.Close();
            }
            else
            {
                MessageBox.Show("Korisničko ime ili lozinka nisu dobro uneseni!");
            }


        }
    }
}
