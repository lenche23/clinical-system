using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    
    public partial class OverlapingAppointments : Window
    {
        public static ObservableCollection<AppointmentForReschedulingDTO> Appointments { get; set; }
        public OverlapingAppointments()
        {
            InitializeComponent();
            this.DataContext = this;
            AppointmentStorage s = new AppointmentStorage();
            List<Appointment> temp = s.GetAll();
            /*Appointments = new ObservableCollection<AppointmentForReschedulingDTO>();
            foreach (Appointment a in temp)
            {
                AppointmentForReschedulingDTO ad = new AppointmentForReschedulingDTO(a);
                Appointments.Add(ad);
            }*/
        }
    }
}
