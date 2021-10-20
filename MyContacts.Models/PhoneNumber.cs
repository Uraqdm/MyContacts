using System;
using System.ComponentModel.DataAnnotations;

namespace MyContacts.Models
{
    public class PhoneNumber
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [Display(Name = "Номер телефона")]
        [Phone]
        public string PhoneNum { get; set; }

    }
}