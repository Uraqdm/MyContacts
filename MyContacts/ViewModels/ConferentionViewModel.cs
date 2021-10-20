using MyContacts.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyContacts.ViewModels
{
    public class ConferentionViewModel
    {
        public Conferention Conferention { get; set; }

        [Display(Name = "Участники")]
        public IEnumerable<string> MembersPhoneNumbers { get; set; }
    }
}
