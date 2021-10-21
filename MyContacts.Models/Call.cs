using System;
using System.ComponentModel.DataAnnotations;

namespace MyContacts.Models
{
    public class Call
    {
        [Required]
        public Guid Id { get; set; }

        public Guid FromId { get; set; }
        public PhoneNumber From { get; set; }

        public Guid ToId { get; set; }
        public PhoneNumber To { get; set; }

        [Required]
        [Display(Name = "Дата")]
        public DateTime Date { get; set; }
    }
}
