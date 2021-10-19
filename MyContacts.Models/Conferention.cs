using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyContacts.Models
{
    public class Conferention
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        public List<PhoneNumber> Members { get; set; }
    }
}
