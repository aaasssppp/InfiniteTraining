using System.Data.Entity;

namespace Assignment1.Models
{
    public class ContactContext : DbContext
    {
        public DbSet<Contact> Contacts { get; set; }

        public ContactContext() : base("DefaultConnection") { }
    }
}