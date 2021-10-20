using System;

namespace MyContacts.Models
{
    public class ConferentionsPhones
    {
        public Guid Id { get; set; }

        public Guid ConferentionId { get; set; }
        public Conferention Conferention { get; set; }

        public Guid PhoneId { get; set; }
        public PhoneNumber PhoneNumber { get; set; }
    }
}
