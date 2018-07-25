using System.Collections.Generic;
using EntityFrameworkRepository.Models;

namespace EntityFrameworkRepository
{
    public class GlossaryInitialiser : System.Data.Entity.DropCreateDatabaseIfModelChanges<GlossaryDbContext>
    {
        protected override void Seed(GlossaryDbContext context)
        {
            var glossaries = new List<Glossary>
            {
                new Glossary{Term = "abyssal plain", Definition = "The ocean floor offshore from the continental margin, usually very flat with a slight slope."},
                new Glossary{Term = "accrete", Definition = "v. To add terranes (small land masses or pieces of crust) to another, usually larger, land mass."},
                new Glossary{Term = "alkaline",  Definition = "Term pertaining to a highly basic, as opposed to acidic, subtance. For example, hydroxide or carbonate of sodium or potassium."}
            };

            glossaries.ForEach(glossary => context.Glossaries.Add(glossary));
            context.SaveChanges();
        }
    }
}
