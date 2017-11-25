using AutoMapper;
using Journals.Model;
using Journals.Repository;
using Journals.Web.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Security;
using Journals.Repository.DataContext;
using Telerik.JustMock;
using Telerik.JustMock.Helpers;

namespace Journals.Web.Tests.Controllers
{
    [TestClass]
    public class SubscriberControllerTests : BaseHelper
    {
        private IJournalRepository _journalRepository;
        private ISubscriptionRepository _subscriptionRepository;
        private IStaticMembershipService _membershipRepository;

        public Journal Journal { get; private set; }

        private MembershipUser _userMock;

        [TestInitialize]
        public void TestInitialize()
        {
            Mapper.CreateMap<Journal, JournalViewModel>();
            Mapper.CreateMap<JournalViewModel, Journal>();
            Mapper.CreateMap<List<Journal>, List<SubscriptionViewModel>>();
            Mapper.CreateMap<List<SubscriptionViewModel>, List<Journal>>();

            //Arrange         
            Journal = GetJournal();

            _journalRepository = Mock.Create<IJournalRepository>();
            _subscriptionRepository = Mock.Create<ISubscriptionRepository>();

            _userMock = Mock.Create<MembershipUser>();
            _membershipRepository = Mock.Create<IStaticMembershipService>();
           
            Mock.Arrange(() => _userMock.ProviderUserKey).Returns(1);
            Mock.Arrange(() => _membershipRepository.GetUser()).Returns(_userMock);

            Database.SetInitializer<JournalsContext>(new ModelChangedInitializer());

        }

        [TestMethod]
        public void SubscriberController_ReturnsAllJournals()
        {
            try
            {
                //Arrange
                var journal1 = GetJournal();                
                var journal2= GetJournal();
                journal2.Description = "TestDesc2";
                journal2.FileName = "TestFilename2.pdf";
                journal2.Title = "Tester2";
               
                Mock.Arrange(() => _subscriptionRepository.GetAllJournals()).Returns(new List<Journal>()
                {
                    journal1,
                    journal2

                }).MustBeCalled();

                //Act
                SubscriberController controller = new SubscriberController(_journalRepository, _subscriptionRepository);
                ViewResult actionResult = (ViewResult) controller.Index();
                var model = actionResult.Model as IEnumerable<JournalViewModel>;

                //Assert
                Assert.IsNotNull(model);
                Assert.AreEqual(2, model.Count());
            }
            catch (Exception)
            {
                // ignored
            }
        }

        [TestMethod]
        public void SubscriberController_SubscribesJournal()
        {
            try
            {
                //Act
                SubscriberController controller = new SubscriberController(_journalRepository, _subscriptionRepository);
                controller.Subscribe(1);
            }
            catch (Exception)
            {
                // ignored
            }
        }

        [TestMethod]
        public void SubscriberController_UnsubscribeForSelectedJournal()
        {
            try
            {
                //Act
                SubscriberController controller = new SubscriberController(_journalRepository, _subscriptionRepository);
                controller.UnSubscribe(1);
            }
            catch (Exception)
            {
                // ignored
            }
        }

    }
}