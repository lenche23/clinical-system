using Model;
using System;
using System.Collections.Generic;
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
            Doctor selectedDoctor = (Doctor)lvUsers.SelectedItems[0];
            DateTime selectedDate = datePicker.SelectedDate.Value.Date;
            String selectedTime = time.Text;
            DateTime dateTime = DateTime.ParseExact(selectedTime, "HH:mm", CultureInfo.InvariantCulture);
            DateTime dateTimeFinal = selectedDate.Date.Add(dateTime.TimeOfDay);

            Appointment order = new Appointment { Doctor = selectedDoctor, StartTime = dateTimeFinal};
            AppointmentStorage storage = new AppointmentStorage();
            storage.Save(order);
            
        }
    }
}
