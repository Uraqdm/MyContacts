using System.ComponentModel.DataAnnotations;

namespace MyContacts.ViewModels
{
    public class PhoneNumberViewModel
    {
        [Required]
        [Phone]
        [Display(Name = "Номер телефона")]
        public string PhoneNum { get; set; }
    }
}
