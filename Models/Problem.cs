using System;

namespace Project_66_bit.Models
{
    public class Problem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Hours { get; set; }
        public DateTime Date { get; set; }
        public int ModuleId { get; set; }
        public Module Module { get; set; }
    }
}