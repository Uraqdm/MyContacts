using System;
using System.ComponentModel.DataAnnotations;

namespace MyContacts.Models
{
    public class Call
    {
        [Required]
        public Guid MyProperty { get; set; }

        [Required]
        public Phone To { get; set; }
    }
}
