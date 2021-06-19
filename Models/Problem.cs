using System;
using System.ComponentModel.DataAnnotations;

namespace Project_66_bit.Models
{
    public class Problem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Hours { get; set; }
<<<<<<<< HEAD:Models/Problem.cs
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        public int ModuleId { get; set; }
        public Module Module { get; set; }
    }
}