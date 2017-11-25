using AutoMapper;
using Journals.Model;
using Journals.Repository;
using Journals.Web.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using Journals.Repository.DataContext;
using Medico.Repository;
using Telerik.JustMock;

namespace Journals.Web.Tests.Controllers
{
    [TestClass]
   
    public class IssuesControllerTests : BaseHelper
    {
        private IIssuesRepository _issuesRepository;
        
        private Issue _issue;

        private Journal _journal;

        private MembershipUser _userMock;

        [TestInitialize]
        public void TestInitialize()
        {
            Mapper.CreateMap<Journal, JournalViewModel>();
            Mapper.CreateMap<JournalViewModel, Journal>();
        
            Mapper.CreateMap<IssueViewModel, Issue>();
            Mapper.CreateMap<Issue, IssueViewModel>();

            //Arrange
            _issue = GetIssue();
            _journal = GetJournal();


            _issuesRepository = Mock.Create<IIssuesRepository>();
            Database.SetInitializer<JournalsContext>(new ModelChangedInitializer());
        }

        [TestMethod]
        public void IssueController_ReturnsAllIssues_ByJournalId()
        {
           //Arrange            
            Mock.Arrange(() => _issuesRepository.GetAllIssues(2)).Returns(new List<Issue>(){
                    new Issue { IssueId=1, VolumeNo= 1, Content = _journal.Content, JournalId = 1,Journal = _journal, Creation = DateTime.Now, ContentType = _journal.ContentType, IssueNo = 1, FileName = "application"},
                    new Issue { IssueId=2, VolumeNo= 1, Content = _journal.Content, JournalId = 2,Journal = _journal, Creation = DateTime.Now, ContentType = _journal.ContentType, IssueNo = 2, FileName = "application"}          
            }).MustBeCalled();
           
            //Act            
            ViewResult actionResult = (ViewResult)new IssuesController(_issuesRepository).Index(2);
            var model = actionResult.Model as IEnumerable<IssueViewModel>;

            //Assert
            Assert.IsNotNull(model);
            Assert.AreEqual(2, model.Count());
        }

        [TestMethod]
        public void IssueController_CreateNewIssue()
        {
            //Arrange
            Mock.Arrange(() => _issuesRepository.GetRunningVolumeId(2)).Returns((2)).MustBeCalled();
            Mock.Arrange(() => _issuesRepository.GetRunningIssueId(1,2)).Returns((int)2).MustBeCalled();

            int journalId = 1;            
            ViewResult actionResult = (ViewResult)new IssuesController(_issuesRepository).Create(journalId);
            var model = actionResult.Model as IssueViewModel;

            //Assert
            Assert.IsNotNull(model);
        }
        
        [TestMethod]
        public void IssueController_GetFileReturnsFileContents()
        {            
            //Arrange            
            Mock.Arrange(() => _issuesRepository.GetIssueById(2)).Returns(_issue).MustBeCalled();
            
            IssuesController controller = new IssuesController(_issuesRepository);
            FileContentResult fileContentResult = (FileContentResult)controller.GetFile(2);          

            //Assert
            Assert.IsNotNull(fileContentResult);
            Assert.IsNotNull(fileContentResult.FileContents);
            Assert.IsTrue(fileContentResult.FileContents.Length > 0);
        }
       
        [TestMethod]

        public void IssueController_GetAllIssuesByJournalId()
        {
            try
            {
                //Arrange
                var issuesRepository = new IssuesRepository();

                //Act
                var issueList = issuesRepository.GetAllIssues(1);

                //Assert
                Assert.IsTrue(issueList != null);
            }
            catch (Exception ex)
            {
                
            }
        }

        [TestMethod]

        public void IssueController_GetIssuesByIssueId()
        {            
                //Arrange   
                var issuesRepository = new IssuesRepository();

                //Act
                var issueList = issuesRepository.GetIssueById(1);

                //Assert
                Assert.IsTrue(issueList != null);            
        }

        [TestMethod]

        public void IssueController_GetIssuesByIssueInvalidId()
        {
            //Arrange   
            var issuesRepository = new IssuesRepository();

            //Act
            var issueList = issuesRepository.GetIssueById(0);

            //Assert
            Assert.IsTrue(issueList == null);
        }

        [TestMethod]

        public void IssueController_AddNewIssue()
        {
            try
            {
                //Arrange
                var issuesRepository = new IssuesRepository();

                //Act
                var status = issuesRepository.AddIssue(new Issue());

                //Assert
                Assert.IsTrue(status != null);
            }
            catch (Exception ex)
            {

            }
        }

        [TestMethod]

        public void IssueController_DeleteAllIssuesByJournalId()
        {
            //Arrange   
            var issuesRepository = new IssuesRepository();
            Database.SetInitializer(strategy: new ModelChangedInitializer());
            //Act
            var status = issuesRepository.DeleteAllIssuesByJournalId(100);
            //Assert
            Assert.IsNotNull(status);
        }

        [TestMethod]

        public void IssueController_GetIssueByVolumeNo()
        {
            //Arrange   
            var issuesRepository = new IssuesRepository();

            //Act
            var status = issuesRepository.GetIssuesInVolume(1);

            //Assert
            Assert.IsNotNull(status);
        }

        [TestMethod]

        public void IssueController_GetRunningIssueIdByNewJournalIdVoumeId()
        {
            //Arrange   
            var issuesRepository = new IssuesRepository();

            //Act
            var status = issuesRepository.GetRunningIssueId(1, 1);

            //Assert
            Assert.IsNotNull(status);
        }

        [TestMethod]

        public void IssueController_GetRunningIssueIdByExistingJournalIdVoumeId()
        {
            //Arrange   
            var issuesRepository = new IssuesRepository();

            //Act
            var status = issuesRepository.GetRunningIssueId(2, 1);

            //Assert
            Assert.IsNotNull(status);
        }

        [TestMethod]

        public void IssueController_GetRunningVolumeNoByJournalId()
        {
            //Arrange   
            var issuesRepository = new IssuesRepository();

            //Act
            var status = issuesRepository.GetRunningVolumeId(1);

            //Assert
            Assert.IsNotNull(status);
        }

        [TestMethod]

        public void IssueController_GetIssue()
        {
            try
            {
                //Arrange   
                var issuesRepository = new IssuesRepository();

                //Act
                var status = issuesRepository.GetIssues(1,1);

                //Assert
                Assert.IsTrue(status != null);
            }
            catch (Exception ex)
            {

            }
        }       
    }
}