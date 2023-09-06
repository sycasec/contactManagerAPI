using contactManagerAPI.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace contactManagerAPI.Data
{
    public class DataContext : DbContext
    {
        protected readonly IConfiguration Config;

        public DataContext(IConfiguration config)
        {
            Config = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(Config.GetConnectionString("ContactManagerDatabase"));
        }

        public DbSet<User> Users => Set<User>();
        public DbSet<Contact> Contacts => Set<Contact>();
        public DbSet<Address> Addresses => Set<Address>();
        public DbSet<ContactNumber> ContactNumbers => Set<ContactNumber>();
        public DbSet<Organization> Organizations => Set<Organization>();
        public DbSet<Note> Notes => Set<Note>();
        public DbSet<AuditLog> AuditLogs => Set<AuditLog>();
        public DbSet<NoteContent> NoteContents => Set<NoteContent>();
        public DbSet<ContactActivity> contactActivities => Set<ContactActivity>();
    }
}
