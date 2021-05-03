﻿using Model;
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

namespace vezba.PatientPages
{
    public partial class GradeSelectedDoctorPage : Page
    {
        public static Doctor Doctor { get; set; }
        public GradeSelectedDoctorPage(Doctor doctor)
        {
            InitializeComponent();
            this.DataContext = this;
            Doctor = doctor;
        }

        private void ButtonConfirmGradeDoctor_Click(object sender, RoutedEventArgs e)
        {
            DoctorEvaluationStorage storage = new DoctorEvaluationStorage();
            int gradeDoctor = grade.SelectedIndex;
            int rating = 0;
            switch (gradeDoctor)
            {
                case 0:
                    rating = 1;
                    break;
                case 1:
                    rating = 2;
                    break;
                case 2:
                    rating = 3;
                    break;
                case 3:
                    rating = 4;
                    break;
                case 4:
                    rating = 5;
                    break;
                default:
                    rating = 1;
                    break;
            }
            String comm = comment.Text;
            int id = storage.GenerateNextId();
            Boolean deleted = false;
            Doctor doctor = Doctor;
            DoctorEvaluation doctorEvaluation = new DoctorEvaluation(rating, comm, id, deleted, doctor);
            storage.Save(doctorEvaluation);
            var s = new SuccessfulGradeDoctor();
            s.Show();
        }
    }
}
