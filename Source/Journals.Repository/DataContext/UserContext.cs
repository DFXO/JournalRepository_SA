using Journals.Model;
using System.Data.Entity;
using System.Diagnostics.CodeAnalysis;

namespace Journals.Repository.DataContext
{
    [ExcludeFromCodeCoverage]
    public class UsersContext : DbContext
    {
        public UsersContext()
            : base("JournalsDB")
        {
        }

        public DbSet<UserProfile> UserProfiles { get; set; }
    }
}