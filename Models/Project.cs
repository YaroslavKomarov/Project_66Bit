using System;

namespace ASPNET_MVC.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ProjectStatus Status { get; set; }
        public string Type { get; set; }
        public int Cost { get; set; }
        public DateTime Date { get; set; }
    }
}