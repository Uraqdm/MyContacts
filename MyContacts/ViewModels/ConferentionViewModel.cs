using MyContacts.Models;
using System.Collections.Generic;

namespace MyContacts.ViewModels
{
    public class ConferentionViewModel
    {
        public Conferention Conferention { get; set; }

        public IEnumerable<string> MembersPhoneNumbers { get; set; }
    }
}
