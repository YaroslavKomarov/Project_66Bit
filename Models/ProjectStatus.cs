using System.ComponentModel.DataAnnotations;

namespace Project_66_bit.Models
{
    public enum ProjectStatus
    {
        [Display(Name = "Планирование")]
        Planning,
        [Display(Name = "Пред. продажа")]
        PreSale,
        [Display(Name = "В разработке")]
        InDevelopment,
        [Display(Name = "Завершен")]
        Complete
    }
}
