using System;
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
using Model;
using Service;

namespace vezba.DoctorPages
{
    /// <summary>
    /// Interaction logic for CreateHospitalTreatment.xaml
    /// </summary>
    public partial class CreateHospitalTreatment : Page
    {
        public List<Room> Rooms { get; set; }
        private readonly Patient _patient;
        private readonly DoctorView _doctorView;

        public CreateHospitalTreatment(Patient patient, DoctorView doctorView)
        {
            InitializeComponent();
            _patient = patient;
            _doctorView = doctorView;
            DataContext = this;
            var roomService = new RoomService();
            Rooms = roomService.GetAll();
            CmbRooms.SelectedIndex = 0;
        }

        private void OkButtonClick(object sender, RoutedEventArgs e)
        {
            var newHospitalTreatment = NewHospitalTreatment();
            var patientService = new PatientService();
            patientService.AddHospitalTreatmentToPatient(_patient, newHospitalTreatment);
            _doctorView.Main.GoBack();
        }

        private HospitalTreatment NewHospitalTreatment()
        {
            var startDate = (DateTime) DpStartDate.SelectedDate;
            var durationInDays = int.Parse(TbDuration.Text);
            var room = (Room) CmbRooms.SelectedItem;
            var newHospitalTreatment = new HospitalTreatment(startDate, durationInDays, room);
            return newHospitalTreatment;
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            _doctorView.Main.GoBack();
        }
    }
}
