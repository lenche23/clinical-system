using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using Model;
using Newtonsoft.Json;

namespace vezba.Repository
{
   public class HospitalEvaluationFileRepository
   {
        public String FileName { get; set; }

        public HospitalEvaluationFileRepository()
        {
            this.FileName = "../../hospital_evaluation.json";
        }

        public List<HospitalEvaluation> GetAll()
        {
            List<HospitalEvaluation> hospitalEvaluations = new List<HospitalEvaluation>();
            try
            {
                String jsonFromFile = File.ReadAllText(this.FileName);
                List<DoctorEvaluation> evaluations = JsonConvert.DeserializeObject<List<DoctorEvaluation>>(jsonFromFile);

                for (int i = 0; i < evaluations.Count; i++)
                {
                    if (evaluations[i].IsDeleted == false)
                    {
                        hospitalEvaluations.Add(hospitalEvaluations[i]);
                    }
                }
                return hospitalEvaluations;
            }
            catch
            {

            }
            MessageBox.Show("Neuspesno ucitavanje iz fajla " + this.FileName + "!");
            return hospitalEvaluations;
        }
      
        public Boolean Save(HospitalEvaluation evaluation)
        {
            List<HospitalEvaluation> evaluations = Load();

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
      
        public Boolean Update(HospitalEvaluation evaluation)
        {
            List<HospitalEvaluation> evaluations = Load();
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
      
        public HospitalEvaluation GetOne(int id)
        {
            List<HospitalEvaluation> evaluations = GetAll();
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
            List<HospitalEvaluation> evaluations = Load();
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
      
        public List<HospitalEvaluation> Load()
        {
            List<HospitalEvaluation> hospitalEvaluations = new List<HospitalEvaluation>();
            try
            {
                String jsonFromFile = File.ReadAllText(this.FileName);
                List<HospitalEvaluation> evaluations = JsonConvert.DeserializeObject<List<HospitalEvaluation>>(jsonFromFile);
                return evaluations;
            }
            catch
            {

            }
            MessageBox.Show("Neuspesno ucitavanje iz fajla " + this.FileName + "!");
            return hospitalEvaluations;
        }
      
        public int GenerateNextId()
        {
            List<HospitalEvaluation> list = Load();
            return list.Count;
        }
    }
}