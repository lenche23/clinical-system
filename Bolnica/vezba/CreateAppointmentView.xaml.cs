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
using Mapster.Adapters;
using Model;
using vezba;

namespace Bolnica
{
    /// <summary>
    /// Interaction logic for CreateAppointmentView.xaml
    /// </summary>
    public partial class CreateAppointmentView : Window
    {
        private DoctorView dw;

        public CreateAppointmentView()
        {
            InitializeComponent();
        }

        private void OkButtonClick(object sender, RoutedEventArgs e)
        {
            var AppointmentID = int.Parse(idTB.Text);
            var format = "MM/dd/yyyy HH:mm";
            CultureInfo provider = CultureInfo.InvariantCulture;
            var StartTime = DateTime.ParseExact(startTB.Text, format, provider);
            var DurationInMinutes = int.Parse(DurationTB.Text);
            var ApointmentDescription = DescriptionTB.Text;
            var appointment1 = new Appointment {AppointentId = AppointmentID, StartTime = StartTime, DurationInMunutes = DurationInMinutes, ApointmentDescription = ApointmentDescription, IsDeleted=false };
            AppointmentStorage aps = new AppointmentStorage();
            aps.Save(appointment1);
            DoctorView.Appointments.Add(appointment1);
            this.Close();
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
