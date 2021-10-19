using Microsoft.EntityFrameworkCore;
using MyContacts.Models;

namespace MyContacts.DatabaseLayer
{
    public class Context : DbContext
    {
        public Context(DbContextOptions opts) : base(opts) { }

        public DbSet<Contact> Contacts { get; set; }

        public DbSet<Call> Calls { get; set; }

        public DbSet<Conferention> Conferentions { get; set; }

        public DbSet<Phone> PhoneNumbers { get; set; }
    }
}
