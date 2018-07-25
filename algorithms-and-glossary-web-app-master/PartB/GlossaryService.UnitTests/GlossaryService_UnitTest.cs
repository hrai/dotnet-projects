using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using EntityFrameworkRepository;
using EntityFrameworkRepository.Models;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace GlossaryService.UnitTests
{
    [TestFixture]
    public class GlossaryService_UnitTest
    {
        private CustomGlossaryService _customGlossaryService;

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GetAll_ReturnsCorrectGlossaries()
        {
            var db = new Mock<GlossaryDbContext>();
            var dbSet = CreateDbSetMock(GetGlossaries());
            _customGlossaryService = new CustomGlossaryService(db.Object);
            db.Setup(ctx => ctx.Glossaries).Returns(dbSet.Object);
            var glossaries = _customGlossaryService.GetAll();

            glossaries.Should().HaveCount(3);
            glossaries.First().Term.Should().Be("ball");
        }
        
        [Test]
        public void Add_AddsToGlossaries()
        {
            var db = new Mock<GlossaryDbContext>();

            var inputGlossaries = GetGlossaries().ToList();
            var dbSet = CreateDbSetMock(inputGlossaries);
            dbSet.Setup(set => set.Add(It.IsAny<Glossary>())).Callback((Glossary glossary) => inputGlossaries.Add(glossary));
            _customGlossaryService = new CustomGlossaryService(db.Object);
            db.Setup(ctx => ctx.Glossaries).Returns(dbSet.Object);

            _customGlossaryService.Add(new Glossary { Term = "pasta", Definition = "it is a food"});
            var glossaries = _customGlossaryService.GetAll();

            glossaries.Should().HaveCount(4);
            glossaries.First().Term.Should().Be("ball");
            glossaries.Any(glossary => glossary.Term == "pasta").Should().BeTrue();
        }

        [Test]
        public void Delete_RemovesFromGlossaries()
        {
            var db = new Mock<GlossaryDbContext>();

            var inputGlossaries = GetGlossaries().ToList();
            var dbSet = CreateDbSetMock(inputGlossaries);
            dbSet.Setup(set => set.Remove(It.IsAny<Glossary>())).Callback((Glossary glossary) =>
            {
                var glossaryToRemove = inputGlossaries.First(g => g.ID == glossary.ID);
                inputGlossaries.Remove(glossaryToRemove);
            });
            _customGlossaryService = new CustomGlossaryService(db.Object);
            db.Setup(ctx => ctx.Glossaries).Returns(dbSet.Object);
            dbSet.Setup(set => set.Find(It.IsAny<int>())).Returns(GetGlossaries().First(g => g.ID == 1));

            var glossaries = _customGlossaryService.GetAll();
            glossaries.Should().HaveCount(3);
            _customGlossaryService.Delete(1);
            glossaries = _customGlossaryService.GetAll();
            glossaries.Should().HaveCount(2);
        }

        private IEnumerable<Glossary> GetGlossaries()
        {
            return new[]
            {
                new Glossary {ID= 1, Term = "cat", Definition = "it is an animal"},
                new Glossary {ID= 2, Term = "ball", Definition = "it is an object"},
                new Glossary {ID= 3, Term = "cuckoo", Definition = "it is a bird"},
            };
        }

        private Mock<DbSet<T>> CreateDbSetMock<T>(IEnumerable<T> elements) where T : class
        {
            var elementsAsQueryable = elements.AsQueryable();
            var dbSetMock = new Mock<DbSet<T>>();

            dbSetMock.As<IQueryable<T>>().Setup(m => m.Provider).Returns(elementsAsQueryable.Provider);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.Expression).Returns(elementsAsQueryable.Expression);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(elementsAsQueryable.ElementType);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(elementsAsQueryable.GetEnumerator());

            return dbSetMock;
        }
    }
}
