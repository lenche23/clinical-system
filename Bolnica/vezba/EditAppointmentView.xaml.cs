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
using vezba;

namespace Bolnica
{
    /// <summary>
    /// Interaction logic for EditAppointmentView.xaml
    /// </summary>
    public partial class EditAppointmentView : Window
    {
        public Appointment selected;
        private DoctorView dw;

        public EditAppointmentView(Appointment selected, DoctorView dw)
        {
            InitializeComponent();
            this.selected = selected;
            this.DataContext = selected;
            this.dw = dw;
        }

        private void OkButtonClick(object sender, RoutedEventArgs e)
        {
            var AppointmentID = int.Parse(idTB.Text);
            var format = "MM/dd/yyyy HH:mm";
            CultureInfo provider = CultureInfo.InvariantCulture;
            var StartTime = DateTime.ParseExact(startTB.Text, format, provider);
            var DurationInMinutes = int.Parse(DurationTB.Text);
            var ApointmentDescription = DescriptionTB.Text;
            var appointment = new Appointment {AppointentId = AppointmentID, StartTime = StartTime, DurationInMunutes = DurationInMinutes, ApointmentDescription = ApointmentDescription};
            AppointmentStorage aps = new AppointmentStorage();
            aps.Update(appointment);
            selected.StartTime = StartTime;
            selected.DurationInMunutes = DurationInMinutes;
            selected.ApointmentDescription = ApointmentDescription;
            dw.listViewAppointments.Items.Refresh();
            this.Close();
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
