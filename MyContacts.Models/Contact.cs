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
        public string Name { get; set; }

        public string MiddleName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public List<Phone> PhoneNumbers { get; set; }
    }
}
