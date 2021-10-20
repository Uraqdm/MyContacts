using System.ComponentModel.DataAnnotations;

namespace MyContacts.ViewModels
{
    public class PhoneNUmberViewModel
    {
        [Required]
        [Phone]
        [Display(Name = "Номер телефона")]
        public string PhoneNum { get; set; }
    }
}
