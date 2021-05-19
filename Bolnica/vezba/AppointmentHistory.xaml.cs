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
    public partial class AppointmentHistory : Window
    {
        public static ObservableCollection<Appointment> Appointments { get; set; }
        public AppointmentHistory()
        {
            InitializeComponent();
            this.DataContext = this;
            AppointmentFileRepository ps = new AppointmentFileRepository();
            List<Appointment> temp = new List<Appointment>();
            foreach (Appointment appointment in ps.GetAll())
            {
                if (appointment.StartTime.Date < DateTime.Now && appointment.Patient.Jmbg.Equals("1008985563244"))
                {
                    temp.Add(appointment);
                   
                }
            }
            Appointments = new ObservableCollection<Appointment>(temp);
        }
    }
}
