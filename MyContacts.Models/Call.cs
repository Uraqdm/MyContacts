using System;
using System.ComponentModel.DataAnnotations;

namespace MyContacts.Models
{
    public class Call
    {
        [Required]
        public Guid Id { get; set; }

        [Display(Name = "От")]
        public PhoneNumber From { get; set; }

        [Required]
        [Display(Name = "Кому")]
        public PhoneNumber To { get; set; }

        [Display(Name = "Дата")]
        public DateTime Date { get; set; }
    }
}
