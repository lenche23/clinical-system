using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using Model;
using Service;

namespace vezba.ManagerGUI
{
    public partial class RenovationMergeRoomsPage : Page, INotifyPropertyChanged
    {
        private MainManagerWindow mainManagerWindow;
        public List<Room> roomList { get; set; }
        private Room selected;


        public event PropertyChangedEventHandler PropertyChanged;

        private int trajanjeRenovacije;
        public int TrajanjeRenovacije
        {
            get
            {
                return trajanjeRenovacije;
            }
            set
            {
                if (value != trajanjeRenovacije)
                {
                    trajanjeRenovacije = value;
                    OnPropertyChanged("TrajanjeRenovacije");
                }
            }
        }

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        public RenovationMergeRoomsPage(MainManagerWindow mainManagerWindow, Room selected)
        {
            InitializeComponent();
            DataContext = this;
            this.mainManagerWindow = mainManagerWindow;
            this.selected = selected;

            RoomService roomService = new RoomService();
            roomList = roomService.GetAllRooms();
            RoomToMerge.ItemsSource = roomList;
            BrojProstorije.Text = BrojProstorije.Text + " " + selected.RoomNumber;
            DatePicker.SelectedDate = DateTime.Now;
        }

        private void CancelButtonClick(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void OkButtonClick(object sender, RoutedEventArgs e)
        {
            if(!ValidateEntries()) return;
            CultureInfo provider = CultureInfo.InvariantCulture;
            var startDate = DatePicker.SelectedDate;
            var startTime = new DateTime(startDate.Value.Year, startDate.Value.Month, startDate.Value.Day, 0, 0, 0);
            var durationInDays = int.Parse(Trajanje.Text);
            var endTime = startTime.AddDays(durationInDays);
            var id = selected.renovation.Count + 1;
            int number = selected.RoomNumber;

            AppointmentService appointmentService = new AppointmentService();

            if (DateTime.Compare(startTime, DateTime.Now) < 0)
            {
                MessageBox.Show("Izabrani datum je već prošao!");
                return;
            }

            if (appointmentService.Overlap(number, startTime, endTime))
            {
                MessageBox.Show("Datum renovacije se poklapa sa već zakazanim pregledima");
                return;
            }

            Room roomToMerge = (Room)RoomToMerge.SelectedItem;
            var newRenovation = new Renovation(startTime, durationInDays, id);
            roomToMerge.AddRenovation(newRenovation);
            selected.AddRenovation(newRenovation);
            roomToMerge.EndDateTime = endTime;
            RoomService roomService = new RoomService();
            roomService.UpdateRoom(roomToMerge);
            roomService.UpdateRoom(selected);
            RenovationsPage.RenovationList.Add(newRenovation);

            NavigationService.GoBack();
        }

        private void ButtonRoomsClick(object sender, RoutedEventArgs e)
        {
            mainManagerWindow.MainManagerView.Content = new RoomsPage(mainManagerWindow);
        }

        private void ButtonInventoryClick(object sender, RoutedEventArgs e)
        {
            mainManagerWindow.MainManagerView.Content = new InventoryPage(mainManagerWindow);
        }

        private void ButtonMedicineClick(object sender, RoutedEventArgs e)
        {
            mainManagerWindow.MainManagerView.Content = new MedicinePage(mainManagerWindow);
        }

        private void ButtonMainClick(object sender, RoutedEventArgs e)
        {
            mainManagerWindow.MainManagerView.Content = new MainManagerPage(mainManagerWindow);
        }
        public Boolean ValidateEntries()
        {
            Trajanje.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            if (Validation.GetHasError(Trajanje))
            {
                return false;
            }
            return true;
        }

        private void Trajanje_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Trajanje.Text == "" || RoomToMerge.SelectedIndex == -1)
            {
                OkButton.IsEnabled = false;
            }

            else OkButton.IsEnabled = true;
        }

        private void RoomToMerge_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Trajanje.Text == "" ||  RoomToMerge.SelectedIndex == -1)
            {
                OkButton.IsEnabled = false;
            }

            else OkButton.IsEnabled = true;
        }
    }
}
