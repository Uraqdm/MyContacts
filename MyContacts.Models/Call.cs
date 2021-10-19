using System;
using System.ComponentModel.DataAnnotations;

namespace MyContacts.Models
{
    public class Call
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Номер телефона")]
        public PhoneNumber To { get; set; }

        [Required]
        [Display(Name = "Дата")]
        public DateTime Date { get; set; }
    }
}
