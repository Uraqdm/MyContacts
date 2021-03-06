using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyContacts.Models
{
    public class Contact
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Имя")]
        public string Name { get; set; }

        [Display(Name = "Отчество")]
        public string MiddleName { get; set; }

        [Required]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        public PhoneNumber PhoneNumber { get; set; }

        public PhoneNumber Owner { get; set; }
    }
}
