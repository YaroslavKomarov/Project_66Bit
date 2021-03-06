using System.Collections.Generic;

namespace Project_66_bit.Models
{
    public class Module
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Hours { get; set; }
        public Project Project { get; set; }
        public int ProjectId { get; set; }

        public List<Problem> Problems { get; set; } = new List<Problem>();
    }
}