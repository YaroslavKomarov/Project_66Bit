using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Project_66_bit.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ProjectStatus Status { get; set; }
        public string Type { get; set; }
        public int Cost { get; set; }
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public List<Module> Modules { get; set; } = new List<Module>();
    }
}