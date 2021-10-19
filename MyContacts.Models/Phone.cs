using System;
using System.ComponentModel.DataAnnotations;

namespace MyContacts.Models
{
    public class Phone
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

    }
}