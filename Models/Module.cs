namespace ASPNET_MVC.Models
{
    public class Module
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Hours { get; set; }
        public Project Project { get; set; }
    }
}