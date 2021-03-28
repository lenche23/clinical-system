using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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
    public partial class OrderAppointmentView : Window
    {
        public OrderAppointmentView()
        {
            InitializeComponent();
            UserStorage storage = new UserStorage();
            lvUsers.ItemsSource = storage.GetAll();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (lvUsers.SelectedItems.Count > 0 && datePicker.SelectedDate != null && (time.Text != null && !time.Text.Equals("")))
            {
                Doctor selectedDoctor = (Doctor)lvUsers.SelectedItem;
                DateTime selectedDate = datePicker.SelectedDate.Value.Date;
                selectedDate.ToString("MM/dd/yyyy");
                String selectedTime = time.Text;
                DateTime dateTime = DateTime.ParseExact(selectedTime, "HH:mm", CultureInfo.InvariantCulture);
                DateTime dateTimeFinal = selectedDate.Date.Add(dateTime.TimeOfDay);
                String exam = "pregled kod lekara opste prakse";

                Appointment a = new Appointment { Doctor = selectedDoctor, StartTime = dateTimeFinal, ApointmentDescription = exam };
                AppointmentStorage storage = new AppointmentStorage();
                storage.Save(a);
                PatientView.Apps.Add(a);

                MessageBox.Show("Your order for appointment has been received. You will be notified about confirmation.");
                this.Close();
            }
            else
            {
                MessageBox.Show("Please input all data required!");
            }
        }
    }
}
