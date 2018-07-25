using System.Collections.Generic;
using EntityFrameworkRepository.Models;

namespace GlossaryService
{
    public interface ICustomGlossaryService
    {
        IEnumerable<Glossary> GetAll();
        Glossary Get(int id);
        void Add(Glossary glossary);
        void Update(Glossary glossary);
        void Delete(int id);
    }
}