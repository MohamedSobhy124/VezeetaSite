using System.Collections.Generic;
using Vezeeta.Models;

namespace Vezeeta.ViewModel
{
    public class DoctorDetailViewModel
    {


        public Doctor Doctor { get; set; }
        //public IEnumerable<Appointment> UpcomingAppointments { get; set; }
        public IEnumerable<Appointment> Appointments { get; set; }
    }
}