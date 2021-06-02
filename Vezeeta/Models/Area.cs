using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Vezeeta.Models
{
    public class Area
    {
        [Key]
        public int AreaID { get; set; }
        public string AreaName { get; set; }
        [ForeignKey("City")]
        public int CityID { get; set; }
        public City City { get; set; }
        public List<Doctor> Doctor { get; set; } = new List<Doctor>();
    }
}