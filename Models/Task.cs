using System;
using System.ComponentModel.DataAnnotations;

namespace Project_66_bit.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Hours { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        public int ModuleId { get; set; }
        public Module Module { get; set; }
    }
}