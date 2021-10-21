using System;

namespace MyContacts.Models
{
    public class ConferentionMember
    {
        public Guid Id { get; set; }

        public Conferention Conferention { get; set; }

        public PhoneNumber PhoneNumber { get; set; }
    }
}
