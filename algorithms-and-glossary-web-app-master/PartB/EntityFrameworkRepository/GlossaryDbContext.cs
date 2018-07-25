using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using EntityFrameworkRepository.Models;

namespace EntityFrameworkRepository
{
    public class GlossaryDbContext : DbContext
    {

        public GlossaryDbContext() : base("GlossaryContext")
        {
        }

        public virtual DbSet<Glossary> Glossaries { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
