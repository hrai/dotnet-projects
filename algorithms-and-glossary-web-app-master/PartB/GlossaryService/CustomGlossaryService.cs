using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using EntityFrameworkRepository;
using EntityFrameworkRepository.Models;

namespace GlossaryService
{
    public class CustomGlossaryService : ICustomGlossaryService
    {
        private GlossaryDbContext db;

        public CustomGlossaryService(GlossaryDbContext glossaryDbContext)
        {
            db = glossaryDbContext;
        }

        public IEnumerable<Glossary> GetAll()
        {
            return db.Glossaries.OrderBy(glossary => glossary.Term).ToList();
        }

        public Glossary Get(int id)
        {
            return db.Glossaries.Find(id);
        }

        public void Add(Glossary glossary)
        {
            db.Glossaries.Add(glossary);
            db.SaveChanges();
        }

        public void Update(Glossary glossary)
        {
            db.Entry(glossary).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var glossary = Get(id);
            db.Glossaries.Remove(glossary);
            db.SaveChanges();
        }
    }
}
