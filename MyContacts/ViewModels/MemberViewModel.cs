using System;
using System.ComponentModel.DataAnnotations;

namespace MyContacts.ViewModels
{
    public class MemberViewModel
    {
        public Guid ConferentionId { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Номер телефона")]
        public string PhoneNum { get; set; }
    }
}
