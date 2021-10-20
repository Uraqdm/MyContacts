using System;
using System.ComponentModel.DataAnnotations;

namespace MyContacts.Models
{
    public class Conferention
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Название")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Дата проведения")]
        public DateTime PlaningDate { get; set; }
    }
}
