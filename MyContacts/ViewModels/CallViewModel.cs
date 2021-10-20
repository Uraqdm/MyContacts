using System.ComponentModel.DataAnnotations;

namespace MyContacts.ViewModels
{
    public class CallViewModel
    {
        [Required]
        [Phone]
        public string PhoneNum { get; set; }
    }
}
