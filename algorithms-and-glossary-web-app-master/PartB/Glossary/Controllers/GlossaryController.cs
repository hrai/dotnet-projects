using System.Net;
using System.Web.Mvc;
using EntityFrameworkRepository;
using GlossaryService;

namespace Glossary.Controllers
{
    public class GlossaryController : Controller
    {
        private ICustomGlossaryService _glossaryService;

        public GlossaryController()
        {
            // Todo - Replace with DI
            _glossaryService = new CustomGlossaryService(new GlossaryDbContext());
        }

        // GET: Glossaries
        public ActionResult Index()
        {
            return View(_glossaryService.GetAll());
        }

        // GET: Glossaries/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Glossaries/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Term,Definition")] EntityFrameworkRepository.Models.Glossary glossary)
        {
            if (ModelState.IsValid)
            {
                _glossaryService.Add(glossary);
                return RedirectToAction("Index");
            }

            return View(glossary);
        }

        // GET: Glossaries/Edit/5
        public ActionResult Edit(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var glossary = _glossaryService.Get(id.Value);
            if (glossary == null)
            {
                return HttpNotFound();
            }

            return View(glossary);
        }

        // POST: Glossaries/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Term,Definition")] EntityFrameworkRepository.Models.Glossary glossary)
        {
            if (ModelState.IsValid)
            {
                _glossaryService.Update(glossary);
                return RedirectToAction("Index");
            }
            return View(glossary);
        }

        // GET: Glossaries/Delete/5
        public ActionResult Delete(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var glossary = _glossaryService.Get(id.Value);
            if (glossary == null)
            {
                return HttpNotFound();
            }

            return View(glossary);
        }

        // POST: Glossaries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _glossaryService.Delete(id);
            return RedirectToAction("Index");
        }

    }
}
