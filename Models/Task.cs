using System;

namespace ASPNET_MVC.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Hours { get; set; }
        public DateTime Date { get; set; }
        public Module module { get; set; }
    }
}