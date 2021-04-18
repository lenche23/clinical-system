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
    public partial class OrderAppointmentTimeView : Window
    {
        public static ObservableCollection<Doctor> Doctors { get; set; }

        public OrderAppointmentTimeView()
        {
            InitializeComponent();
            this.DataContext = this;
            DoctorStorage ps = new DoctorStorage();
            List<Doctor> temp = ps.GetAll();
            Doctors = new ObservableCollection<Doctor>(temp);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (datePicker.SelectedDate != null && (comboBox.Text != null && !comboBox.Text.Equals("")) && doctorsTable.SelectedItems.Count > 0)
            {
                Doctor selectedDoctor = (Doctor)doctorsTable.SelectedItem;
                DateTime selectedDate = datePicker.SelectedDate.Value.Date;
                
                selectedDate.ToString("MM/dd/yyyy");
                String selectedTime = comboBox.Text;
                DateTime dateTime = DateTime.ParseExact(selectedTime, "HH:mm", CultureInfo.InvariantCulture);
                DateTime dateTimeFinal = selectedDate.Date.Add(dateTime.TimeOfDay);
                PatientStorage pps = new PatientStorage();
                Patient patient = pps.GetOne("1008985563244");

                AppointmentStorage storage = new AppointmentStorage();
                int id = storage.generateNextId();
                Appointment a = new Appointment(selectedDoctor, dateTimeFinal, patient);
                a.AppointentId = id;
                storage.Save(a);
                PatientView.Apps.Add(a);

                int diff = (selectedDate - DateTime.Now.Date).Days;
                if (diff <= 0)
                {
                    MessageBox.Show("Unet datum nije validan!");
                    this.Close();
                }
                else {
                    MessageBox.Show("Uspešno ste zakazali pregled.");
                    this.Close();
                }                
            }
            else
            {
                MessageBox.Show("Molimo Vas popunite sva potrebna polja!");
            }
        }
    }
}
