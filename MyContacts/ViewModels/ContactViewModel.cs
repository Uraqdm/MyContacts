using MyContacts.Models;
using System.ComponentModel.DataAnnotations;

namespace MyContacts.ViewModels
{
    public class ContactViewModel
    {
        public Contact Contact { get; set; }

        [Required]
        [Phone]
        public string PhoneNum { get; set; }
    }
}
