using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vezeeta.Models;

namespace Vezeeta.Services
{
    public interface IPatientService
    {
        Patient Create(Patient patient);
    }
    public class PatientService : IPatientService
    {
        private readonly MyModel myModel;
        public PatientService()
        {
            myModel = new MyModel();
        }
        public Patient Create(Patient patient)
        {
            myModel.Patients.Add(patient);
            var savingResult= myModel.SaveChanges();
            if(savingResult > 0)
            {
                return patient;

            }
            return null;
        }
    }
}