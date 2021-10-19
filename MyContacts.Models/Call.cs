using System;
using System.ComponentModel.DataAnnotations;

namespace MyContacts.Models
{
    public class Call
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public Phone To { get; set; }

        [Required]
        public DateTime Date { get; set; }
    }
}
