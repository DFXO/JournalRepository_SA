using Journals.Model;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Diagnostics.CodeAnalysis;

namespace Journals.Repository.DataContext
{
    [ExcludeFromCodeCoverage]
    public class JournalsContext : DbContext, IDisposedTracker
    {
        public JournalsContext()
            : base("name=JournalsDB")
        {
        }

        public DbSet<Journal> Journals { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<Issue> Issues { get; set; }

        public bool IsDisposed { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            base.Configuration.LazyLoadingEnabled = false;
            modelBuilder.Entity<Journal>().ToTable("Journals");
            modelBuilder.Entity<Subscription>().ToTable("Subscriptions");
            modelBuilder.Entity<Issue>().ToTable("Issues");
            base.OnModelCreating(modelBuilder);
        }

        protected override void Dispose(bool disposing)
        {
            IsDisposed = true;
            base.Dispose(disposing);
        }
    }
}