using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyContacts.Models
{
    public class Conferention
    {
        [Required]
        public Guid Id { get; set; }

        public List<Phone> Members { get; set; }
    }
}
