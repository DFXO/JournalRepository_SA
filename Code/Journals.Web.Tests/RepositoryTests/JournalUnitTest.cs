using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Journals.Model;
using Journals.Repository;
using Journals.Web.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Telerik.JustMock;

namespace Medico.Web.Tests.Controllers
{
    [TestClass]
  
    public class JournalUnitTest : BaseHelper
    {
        private IJournalRepository _journalRepository;

        public Issue Issue { get; }

        public Journal Journal { get; private set; }        

        [TestInitialize]
        public void TestInitialize()
        {
            Mapper.CreateMap<Journal, JournalViewModel>();
            Mapper.CreateMap<JournalViewModel, Journal>();

            Mapper.CreateMap<IssueViewModel, Issue>();
            Mapper.CreateMap<Issue, IssueViewModel>();

            Journal = GetJournal();

            _journalRepository = new JournalRepository();
        }

        [TestMethod]

        public void Journal_AddNewJournal()
        {
            //Act
            var status = _journalRepository.AddJournal(GetJournal());
            //Assert
            Assert.IsTrue(status != null);
        }

        [TestMethod]
        public void Journal_DeleteExistingJournal()
        {
            //Act
            var status = _journalRepository.DeleteJournal(GetJournal());
            //Assert
            Assert.IsTrue(status != null);
        }

        [TestMethod]
        public void Journal_GetAllJournals()
        {
            //Act
            var journalList = _journalRepository.GetAllJournals(1);

            //Assert
            Assert.IsTrue(journalList != null);
        }

        [TestMethod]

        public void Journal_GetJournalById()
        {
            //Act
            var journalList = _journalRepository.GetJournalById(1);

            //Assert
            Assert.IsTrue(journalList == null);
        }

        public void Journal_GetJournalByInvalidId()
        {
            //Act
            var journalList = _journalRepository.GetJournalById(0);

            //Assert
            Assert.IsTrue(journalList == null);
        }

        [TestMethod]

        public void Journal_UpdateJournal()
        {
            //Act
            var journalList = _journalRepository.UpdateJournal(GetJournal());
            //Assert
            Assert.IsTrue(journalList != null);
        }
    }
}
