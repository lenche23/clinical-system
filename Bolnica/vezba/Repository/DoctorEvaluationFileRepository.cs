using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using Model;
using Newtonsoft.Json;

namespace vezba.Repository
{
   public class DoctorEvaluationFileRepository
   {
        public String FileName { get; set; }

        public DoctorEvaluationFileRepository()
        {
            this.FileName = "../../doctor_evaluation.json";
        }

        public List<DoctorEvaluation> GetAll()
        {
            List<DoctorEvaluation> doctorEvaluations = new List<DoctorEvaluation>();
            try
            {
                String jsonFromFile = File.ReadAllText(this.FileName);
                List<DoctorEvaluation> evaluations = JsonConvert.DeserializeObject<List<DoctorEvaluation>>(jsonFromFile);

                for (int i = 0; i < evaluations.Count; i++)
                {
                    if (evaluations[i].IsDeleted == false)
                    {
                        doctorEvaluations.Add(evaluations[i]);
                    }
                }
                return doctorEvaluations;
            }
            catch
            {

            }
            MessageBox.Show("Neuspesno ucitavanje iz fajla " + this.FileName + "!");
            return doctorEvaluations;
        }
      
        public Boolean Save(DoctorEvaluation evaluation)
        {
            List<DoctorEvaluation> evaluations = Load();

            for (int i = 0; i < evaluations.Count; i++)
            {
                if (evaluations[i].Id.Equals(evaluation.Id) && evaluations[i].IsDeleted == false)
                {
                    return false;
                }
            }
            evaluations.Add(evaluation);
            try
            {
                var jsonToFile = JsonConvert.SerializeObject(evaluations, Formatting.Indented);
                using (StreamWriter writer = new StreamWriter(this.FileName))
                {
                    writer.Write(jsonToFile);
                }
            }
            catch (Exception e)
            {

            }
            return true;
        }
      
        public Boolean Update(DoctorEvaluation evaluation)
        {
            List<DoctorEvaluation> evaluations = Load();
            for (int i = 0; i < evaluations.Count; i++)
            {
                if (evaluations[i].Id.Equals(evaluation.Id))
                {
                    evaluations[i].Comment = evaluation.Comment;
                    evaluations[i].Id = evaluation.Id;
                    evaluations[i].IsDeleted = evaluation.IsDeleted;
                    evaluations[i].Rating = evaluation.Rating;

                    try
                    {
                        var jsonToFile = JsonConvert.SerializeObject(evaluations, Formatting.Indented);
                        using (StreamWriter writer = new StreamWriter(this.FileName))
                        {
                            writer.Write(jsonToFile);
                        }
                    }
                    catch (Exception e)
                    {

                    }
                }
            }
            return false;
        }
      
        public DoctorEvaluation GetOne(int id)
        {
            List<DoctorEvaluation> evaluations = GetAll();
            for (int i = 0; i < evaluations.Count; i++)
            {
                if (evaluations[i].Id.Equals(id))
                {
                    return evaluations[i];
                }
            }
            return null;
        }
      
        public Boolean Delete(int id)
        {
            List<DoctorEvaluation> evaluations = Load();
            for (int i = 0; i < evaluations.Count; i++)
            {
                if (evaluations[i].Id.Equals(id))
                {
                    evaluations[i].IsDeleted = true;

                    try
                    {
                        var jsonToFile = JsonConvert.SerializeObject(evaluations, Formatting.Indented);
                        using (StreamWriter writer = new StreamWriter(this.FileName))
                        {
                            writer.Write(jsonToFile);
                        }
                    }
                    catch (Exception e)
                    {

                    }
                    return true;
                }
            }
            return false;
        }
      
        public List<DoctorEvaluation> Load()
        {
            List<DoctorEvaluation> doctorEvaluations = new List<DoctorEvaluation>();
            try
            {
                String jsonFromFile = File.ReadAllText(this.FileName);
                List<DoctorEvaluation> evaluations = JsonConvert.DeserializeObject<List<DoctorEvaluation>>(jsonFromFile);
                return evaluations;
            }
            catch (Exception e)
            {

            }
            MessageBox.Show("Neuspesno ucitavanje iz fajla " + this.FileName + "!");
            return doctorEvaluations;
        }
      
        public int GenerateNextId()
        {
            List<DoctorEvaluation> list = Load();
            return list.Count;
        }
    }
}