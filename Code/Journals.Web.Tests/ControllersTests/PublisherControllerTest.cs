using AutoMapper;
using Journals.Model;
using Journals.Repository;
using Journals.Web.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Security;
using Journals.Repository.DataContext;
using Telerik.JustMock;

namespace Journals.Web.Tests.Controllers
{
    [TestClass]
  
    public class PublisherControllerTest : BaseHelper
    {
        private IStaticMembershipService _membershipRepository;

        private IIssuesRepository _issuesRepository;
        private IJournalRepository _journalRepository;

        private Journal _journal;

        public MembershipUser UserMock { get; set; }

        [TestInitialize]
        public void TestInitialize()
        {
            Mapper.CreateMap<Journal, JournalViewModel>();
            Mapper.CreateMap<JournalViewModel, Journal>();
            Mapper.CreateMap<Journal, JournalUpdateViewModel>();
            Mapper.CreateMap<JournalUpdateViewModel, Journal>();

            //Arrange
            _membershipRepository = Mock.Create<IStaticMembershipService>();
            _issuesRepository = Mock.Create<IIssuesRepository>();
            _journalRepository = Mock.Create<IJournalRepository>();
            UserMock = Mock.Create<MembershipUser>();
            Mock.Arrange(() => UserMock.ProviderUserKey).Returns(1);
            Mock.Arrange(() => _membershipRepository.GetUser()).Returns(UserMock);

            _journal = GetJournal();
            Database.SetInitializer(new ModelChangedInitializer());

        }

        [TestMethod]
        public void PublisherController_ReturnsAllJournalsOfPublisher()
        {
            //Arrange
            var journalRepository = Mock.Create<IJournalRepository>();
            Mock.Arrange(() => journalRepository.GetAllJournals((int)UserMock.ProviderUserKey)).Returns(new List<Journal>(){
                    new Journal{ Id=1, Description="TestDesc", FileName="TestFilename.pdf", Title="Tester", UserId=1, ModifiedDate= DateTime.Now},
                    new Journal{ Id=1, Description="TestDesc2", FileName="TestFilename2.pdf", Title="Tester2", UserId=1, ModifiedDate = DateTime.Now}
            }).MustBeCalled();

            //Act
            PublisherController controller = new PublisherController(journalRepository, _issuesRepository, _membershipRepository);
            ViewResult actionResult = (ViewResult)controller.Index();
            var model = actionResult.Model as IEnumerable<JournalViewModel>;

            //Assert
            Assert.AreEqual(2, model.Count());
        }

        [TestMethod]
        public void PublisherController_ReturnsDEfaultModel()
        {            
            //Act
            PublisherController controller = new PublisherController(_journalRepository, _issuesRepository, _membershipRepository);
            ViewResult actionResult = (ViewResult)controller.Create();
            var model = actionResult.Model as IEnumerable<JournalViewModel>;

            //Assert
            Assert.AreEqual(null, model);
        }        

        [TestMethod]
        public void PublisherController_ReturnsFileContent()
        {            
            //Arrange            
            Mock.Arrange(() => _journalRepository.GetJournalById(2)).Returns(GetJournal).MustBeCalled();

            //Act
            PublisherController controller = new PublisherController(_journalRepository, _issuesRepository, _membershipRepository);
            FileContentResult fileContentResult = (FileContentResult)controller.GetFile(2);            

            //Assert
            Assert.IsNotNull(fileContentResult);
            Assert.IsNotNull(fileContentResult.FileContents);
            Assert.IsTrue(fileContentResult.FileContents.Length>0);
        }

        [TestMethod]
        [ExpectedException(typeof (HttpResponseException))]
        public void PublisherController_CreatesNewJournalInPublisherAccount()
        {
            Mapper.CreateMap<Journal, JournalViewModel>();
            Mapper.CreateMap<JournalViewModel, Journal>();          

            JournalViewModel journalViewModel = Mapper.Map<Journal, JournalViewModel>(_journal);

            //Arrange         
            var journalRepository = Mock.Create<IJournalRepository>();
            
            Mock.Arrange(() => journalRepository.AddJournal(_journal)).Returns(new OperationStatus { Status = false, RecordsAffected = 1 }).MustBeCalled();

            //Act
            PublisherController controller = new PublisherController(journalRepository, _issuesRepository, _membershipRepository);
            ViewResult actionResult = (ViewResult)controller.Create(journalViewModel);
            var model = actionResult.Model as IEnumerable<JournalViewModel>;

            //Assert
            Assert.IsNotNull(model);
            Assert.AreEqual(2, model.Count());
        }

        [TestMethod]
        public void PublisherController_DeletesJournalsFromPublishersAccount()
        {           
            //Arrange
                      
            Mock.Arrange(() => _journalRepository.GetJournalById(2)).Returns(GetJournal()).MustBeCalled();

            //Act
            PublisherController controller = new PublisherController(_journalRepository, _issuesRepository, _membershipRepository);
            ViewResult actionResult = (ViewResult)controller.Delete(2);
            var model = actionResult.Model as JournalViewModel;

            //Assert
            Assert.IsNotNull(model);
            Assert.AreEqual(1, model.Id);
            Assert.AreEqual("TestDesc", model.Description);
        }


        [TestMethod]
        [ExpectedException(typeof(HttpResponseException))]
        public void PublisherController_DeletesJournalAndItsIssues()
        {
            JournalViewModel journalViewModel = Mapper.Map<Journal, JournalViewModel>(_journal);

            //Arrange                                  
            Mock.Arrange(() => _journalRepository.DeleteJournal(_journal)).Returns(new OperationStatus { Status = true, RecordsAffected = 1 });
            Mock.Arrange(() => _issuesRepository.DeleteAllIssuesByJournalId(_journal.Id)).Returns(new OperationStatus { Status = true, RecordsAffected = 1 });

            //Act
            PublisherController controller = new PublisherController(_journalRepository, _issuesRepository, _membershipRepository);
            ViewResult actionResult = (ViewResult)controller.Delete(journalViewModel);
            var model = actionResult.Model as JournalViewModel;

            //Assert
            Assert.AreEqual(2, model);
        }

        [TestMethod]
        public void PublisherController_EditsJournal()
        {            
            //Arrange       
            Mock.Arrange(() => _journalRepository.GetJournalById(2)).Returns(GetJournal()).MustBeCalled();

            //Act
            PublisherController controller = new PublisherController(_journalRepository, _issuesRepository, _membershipRepository);
            ViewResult actionResult = (ViewResult)controller.Edit(2);
            var model = actionResult.Model as JournalUpdateViewModel;

            //Assert
            Assert.IsNotNull(model);
            Assert.AreEqual(1, model.Id);
            Assert.AreEqual("TestDesc", model.Description);
        }

        [TestMethod]
        [ExpectedException(typeof(HttpResponseException))]
        public void PublisherController_UpdateJournal()
        {
           JournalUpdateViewModel journalUpdateViewModel = GetUpdateViewModel();

            OperationStatus os = new OperationStatus
            {
                Status = true,
                RecordsAffected = 1,
                ExceptionInnerMessage = "",
                ExceptionInnerStackTrace = "",
                ExceptionMessage = "",
                ExceptionStackTrace = "",
                Message = "",
                OperationID = "23456"
            };
            //Arrange                      
            Mock.Arrange(() => _journalRepository.UpdateJournal(_journal))
                .Returns(os)
                .MustBeCalled();

            //Act
            PublisherController controller = new PublisherController(_journalRepository, _issuesRepository, _membershipRepository);
            ViewResult actionResult = (ViewResult) controller.Edit(journalUpdateViewModel);
            var model = actionResult.Model as JournalUpdateViewModel;

            //Assert
            Assert.IsNotNull(model);
            Assert.AreEqual(1, model.Id);
            Assert.AreEqual("TestDesc", model.Description);
        }        
    }
}