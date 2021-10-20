using Microsoft.EntityFrameworkCore;
using MyContacts.Models;
using System;

namespace MyContacts.DatabaseLayer
{
    public class Context : DbContext
    {
        public Context(DbContextOptions opts) : base(opts) { }

        public DbSet<Contact> Contacts { get; set; }

        public DbSet<Call> Calls { get; set; }

        public DbSet<Conferention> Conferentions { get; set; }

        public DbSet<PhoneNumber> PhoneNumbers { get; set; }

        public DbSet<ConferentionsPhones> ConferentionsPhones { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Creating phone numbers
            modelBuilder.Entity<PhoneNumber>().HasData(new PhoneNumber
            {
                Id = new Guid("e7aaa2b2-207d-4626-a74d-a8af8d41b239"),
                PhoneNum = "88005553535"
            },
            new PhoneNumber
            {
                Id = new Guid("6d8dae9f-de21-4dda-a952-dcc0103351d1"),
                PhoneNum = "89054122233"
            });
        }
    }
}
