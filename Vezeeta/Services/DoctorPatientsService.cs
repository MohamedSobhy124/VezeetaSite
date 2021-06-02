using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vezeeta.Models;

namespace Vezeeta.Services
{
    public interface IDoctorPatientsService
    {
        IEnumerable<Doctor> GetDoctors(int PatienID);
    }
    public class DoctorPatientsService :IDoctorPatientsService
    {
        private readonly Patient patient;
        public DoctorPatientsService()
        {
            patient = new Patient(); 
        }

        public IEnumerable<Patient> GetDoctors(int PatienID)
        {
           return (IEnumerable<Patient>)patient.PatientApointments.Where(t => t.PatientID == PatienID);
        }

        IEnumerable<Doctor> IDoctorPatientsService.GetDoctors(int PatienID)
        {
            throw new NotImplementedException();
        }
    }
}