using Model;
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

namespace vezba
{
    /// <summary>
    /// Interaction logic for Calendar.xaml
    /// </summary>
    public partial class Calendar : Page
    {
        public Grid dynamicGrid;
        private DoctorView doctorView;
        private DateTime startOfWeek;
        private DateTime endOfWeek;
        private List<Appointment> appointments;
        private DockPanel timeDockPanel;

        public Calendar(DoctorView doctorView)
        {
            InitializeComponent();
            this.doctorView = doctorView;

            startOfWeek = DateTime.Today.AddDays(-(int)DateTime.Today.DayOfWeek + 1);
            endOfWeek = startOfWeek.AddDays(7);

            CreateSchedule();

            timeDockPanel = new DockPanel();

            Grid timeGrid = new Grid();
            timeGrid.Height = 3000;
            timeGrid.Width = 50;
            for (int i = 0; i < 24; i++)
            {
                timeGrid.RowDefinitions.Add(new RowDefinition());
            }

            for (int i = 0; i < timeGrid.RowDefinitions.Count; i++)
            {
                TextBlock timeBlock = new TextBlock();
                timeBlock.Text = "" + i;
                timeBlock.FontSize = 14;
                timeBlock.HorizontalAlignment = HorizontalAlignment.Center;
                Grid.SetRow(timeBlock, i);
                timeGrid.Children.Add(timeBlock);
            }


            DockPanel.SetDock(timeGrid, Dock.Left);
            timeDockPanel.Children.Add(timeGrid);
            timeDockPanel.Children.Add(dynamicGrid);

            ScrollViewer scrollViewer = new ScrollViewer();
            scrollViewer.Content = timeDockPanel;
            var earliestTime = appointments[0].StartTime.TimeOfDay;
            foreach (var appointment in appointments)
            {
                if (TimeSpan.Compare(appointment.StartTime.TimeOfDay, earliestTime) < 0)
                {
                    earliestTime = appointment.StartTime.TimeOfDay;
                }
            }

            var scrollOffset = (earliestTime.Hours + earliestTime.Minutes / 60) * dynamicGrid.Height / 24;
            scrollViewer.ScrollToVerticalOffset(scrollOffset);

            DockPanel daysOfWeekDockPanel = new DockPanel();

            Grid daysOfWeekGrid = new Grid();
            daysOfWeekGrid.Height = 50;

            Border daysOfWeekBorder = new Border();
            daysOfWeekBorder.BorderThickness = new Thickness(0, 0, 0, 1);
            daysOfWeekBorder.BorderBrush = Brushes.Gray;

            daysOfWeekGrid.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(50) });

            for (int i = 0; i < 7; i++)
            {
                daysOfWeekGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            TextBlock mondayBlock = new TextBlock() { Text = "Ponedeljak" };
            mondayBlock.HorizontalAlignment = HorizontalAlignment.Center;
            mondayBlock.VerticalAlignment = VerticalAlignment.Bottom;
            mondayBlock.FontSize = 14;
            Grid.SetColumn(mondayBlock, 1);
            daysOfWeekGrid.Children.Add(mondayBlock);

            TextBlock tuesdayBlock = new TextBlock() { Text = "Utorak" };
            tuesdayBlock.HorizontalAlignment = HorizontalAlignment.Center;
            tuesdayBlock.VerticalAlignment = VerticalAlignment.Bottom;
            tuesdayBlock.FontSize = 14;
            Grid.SetColumn(tuesdayBlock, 2);
            daysOfWeekGrid.Children.Add(tuesdayBlock);

            TextBlock wednesdayBlock = new TextBlock() { Text = "Sreda" };
            wednesdayBlock.HorizontalAlignment = HorizontalAlignment.Center;
            wednesdayBlock.VerticalAlignment = VerticalAlignment.Bottom;
            wednesdayBlock.FontSize = 14;
            Grid.SetColumn(wednesdayBlock, 3);
            daysOfWeekGrid.Children.Add(wednesdayBlock);

            TextBlock thursdayBlock = new TextBlock() { Text = "Četvrtak" };
            thursdayBlock.HorizontalAlignment = HorizontalAlignment.Center;
            thursdayBlock.VerticalAlignment = VerticalAlignment.Bottom;
            thursdayBlock.FontSize = 14;
            Grid.SetColumn(thursdayBlock, 4);
            daysOfWeekGrid.Children.Add(thursdayBlock);

            TextBlock fridayBlock = new TextBlock() { Text = "Petak" };
            fridayBlock.HorizontalAlignment = HorizontalAlignment.Center;
            fridayBlock.VerticalAlignment = VerticalAlignment.Bottom;
            fridayBlock.FontSize = 14;
            Grid.SetColumn(fridayBlock, 5);
            daysOfWeekGrid.Children.Add(fridayBlock);

            TextBlock saturdayBlock = new TextBlock() { Text = "Subota" };
            saturdayBlock.HorizontalAlignment = HorizontalAlignment.Center;
            saturdayBlock.VerticalAlignment = VerticalAlignment.Bottom;
            saturdayBlock.FontSize = 14;
            Grid.SetColumn(saturdayBlock, 6);
            daysOfWeekGrid.Children.Add(saturdayBlock);

            TextBlock sundayBlock = new TextBlock() { Text = "Nedelja" };
            sundayBlock.HorizontalAlignment = HorizontalAlignment.Center;
            sundayBlock.VerticalAlignment = VerticalAlignment.Bottom;
            sundayBlock.FontSize = 14;
            Grid.SetColumn(sundayBlock, 7);
            daysOfWeekGrid.Children.Add(sundayBlock);

            daysOfWeekBorder.Child = daysOfWeekGrid;

            DockPanel.SetDock(daysOfWeekBorder, Dock.Top);
            daysOfWeekDockPanel.Children.Add(daysOfWeekBorder);
            daysOfWeekDockPanel.Children.Add(scrollViewer);
            CalendarGrid.Children.Add(daysOfWeekDockPanel);
        }

        private void CreateSchedule()
        {
            dynamicGrid = new Grid();
            dynamicGrid.Height = 3000;
            for (int i = 0; i < 24; i++)
            {
                dynamicGrid.RowDefinitions.Add(new RowDefinition());
            }

            for (int i = 0; i < 7; i++)
            {
                dynamicGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }

            for (int i = 0; i < dynamicGrid.RowDefinitions.Count; i++)
            {
                for (int j = 0; j < dynamicGrid.ColumnDefinitions.Count; j++)
                {
                    Border innerGridBorder = new Border();
                    innerGridBorder.BorderBrush = Brushes.Gray;
                    innerGridBorder.BorderThickness = new Thickness(0.3);
                    Grid innerGrid = new Grid();
                    innerGrid.Background = Brushes.Transparent;
                    var generatedStartTime = startOfWeek.AddDays(j);
                    generatedStartTime = generatedStartTime.AddHours(i);
                    innerGrid.MouseLeftButtonDown += (sen, evg) =>
                    {
                        doctorView.Main.Content = new CreateAppointment(doctorView, this, generatedStartTime);
                    };
                    innerGridBorder.Child = innerGrid;
                    Grid.SetRow(innerGridBorder, i);
                    Grid.SetColumn(innerGridBorder, j);
                    dynamicGrid.Children.Add(innerGridBorder);
                }
            }

            GenerateAppointmentsForSelectedWeek();

            foreach (var appointment in appointments)
            {
                ShowAppointment(appointment);
            }
        }

        private void GenerateAppointmentsForSelectedWeek()
        {
            AppointmentStorage appointmentStorage = new AppointmentStorage();
            appointments = new List<Appointment>();

            foreach (var appointment in appointmentStorage.GetAll())
            {
                if (DateTime.Compare(appointment.StartTime, startOfWeek) > 0 &&
                    DateTime.Compare(appointment.StartTime, endOfWeek) < 0)
                {
                    appointments.Add(appointment);
                }
            }
        }

        public void ShowAppointment(Appointment appointment)
        {
            var row = appointment.StartTime.Hour;
                var rowSpan = (appointment.StartTime.Minute + appointment.DurationInMunutes) / (int)60 + 1;
                var topMargin = appointment.StartTime.Minute * dynamicGrid.Height / 1440 + 1;
                var bottomMargin = rowSpan * dynamicGrid.Height / 24 - topMargin - appointment.DurationInMunutes * dynamicGrid.Height / 1440 + 1;
                var col = (6 + (int)appointment.StartTime.DayOfWeek) % 7;
                Grid appointmentGrid = new Grid();
                appointmentGrid.Margin = new Thickness(1, topMargin, 1, bottomMargin);
                appointmentGrid.Background = Brushes.CornflowerBlue;
                appointmentGrid.MouseLeftButtonDown += (sen, evg) =>
                {
                    doctorView.Main.Content = new ViewAppointmentPage(appointment, doctorView, this,appointmentGrid);
                };
                Grid.SetRow(appointmentGrid, row);
                Grid.SetColumn(appointmentGrid, col);
                Grid.SetRowSpan(appointmentGrid, rowSpan);

                Viewbox viewBox = new Viewbox();
                viewBox.StretchDirection = StretchDirection.DownOnly;
                viewBox.Stretch = Stretch.Uniform;
                viewBox.VerticalAlignment = VerticalAlignment.Top;
                viewBox.HorizontalAlignment = HorizontalAlignment.Left;
                viewBox.Margin = new Thickness(2);

                Grid viewGrid = new Grid();
                viewGrid.Background = Brushes.Transparent;
                viewGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
                viewGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
                viewGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Auto });
                viewGrid.RowDefinitions.Add(new RowDefinition());

                TextBlock appointmentDescriptionBlock = new TextBlock();
                appointmentDescriptionBlock.Text = appointment.ApointmentDescription;
                appointmentDescriptionBlock.Foreground = Brushes.White;
                appointmentDescriptionBlock.FontSize = 16;
                Grid.SetRow(appointmentDescriptionBlock, 0);
                viewGrid.Children.Add(appointmentDescriptionBlock);

                TextBlock doctorBlock = new TextBlock();
                doctorBlock.Text = "Lekar: " + appointment.Doctor.NameAndSurname;
                doctorBlock.Foreground = Brushes.White;
                Grid.SetRow(doctorBlock, 1);
                viewGrid.Children.Add(doctorBlock);


                TextBlock patientBlock = new TextBlock();
                patientBlock.Text = "Pacijent: " + appointment.Patient.NameAndSurname;
                patientBlock.Foreground = Brushes.White;
                Grid.SetRow(patientBlock, 2);
                viewGrid.Children.Add(patientBlock);

                TextBlock roomBlock = new TextBlock();
                roomBlock.Text = "Soba: " + appointment.Room.RoomNumber;
                roomBlock.Foreground = Brushes.White;
                Grid.SetRow(roomBlock, 3);
                viewGrid.Children.Add(roomBlock);

                viewBox.Child = viewGrid;
                appointmentGrid.Children.Add(viewBox);

                dynamicGrid.Children.Add(appointmentGrid);
        }

        public void RemoveAppointment(Grid appointmentGrid)
        {
            dynamicGrid.Children.Remove(appointmentGrid);
        }

        private void NewAppointmentClick(object sender, RoutedEventArgs e)
        {
            doctorView.Main.Content = new CreateAppointment(doctorView, this);
        }

        private void PreviousWeekClick(object sender, RoutedEventArgs e)
        {
            timeDockPanel.Children.Remove(dynamicGrid);
            startOfWeek = startOfWeek.AddDays(-7);
            endOfWeek = endOfWeek.AddDays(-7);
            GenerateAppointmentsForSelectedWeek();
            CreateSchedule();
            timeDockPanel.Children.Add(dynamicGrid);
        }

        private void NextWeekClick(object sender, RoutedEventArgs e)
        {
            timeDockPanel.Children.Remove(dynamicGrid);
            startOfWeek = startOfWeek.AddDays(7);
            endOfWeek = endOfWeek.AddDays(7);
            GenerateAppointmentsForSelectedWeek();
            CreateSchedule();
            timeDockPanel.Children.Add(dynamicGrid);
        }
    }
}
