using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vezeeta.Models
{
    public class City
    {
        [Key]
        public int CityID { get; set; }
        public string CityName { get; set; }
        public virtual List<Doctor> Doctor { get; set; } = new List<Doctor>();
        public virtual List<Area> Areas { get; set; } = new List<Area>();


    }
}