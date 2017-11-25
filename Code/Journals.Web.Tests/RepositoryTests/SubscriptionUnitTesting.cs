using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Journals.Model;
using Journals.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Medico.Web.Tests
{
    [TestClass]
    
    public class SubscriptionUnitTesting
    {
        private ISubscriptionRepository _subscriptionRepository;

        public Issue Issue { get; }

        public Journal Journal { get; }      

        [TestInitialize]
        public void TestInitialize()
        {
            Mapper.CreateMap<Journal, JournalViewModel>();
            Mapper.CreateMap<JournalViewModel, Journal>();

            Mapper.CreateMap<IssueViewModel, Issue>();
            Mapper.CreateMap<Issue, IssueViewModel>();
         
            _subscriptionRepository = new SubscriptionRepository();
        }

        [TestMethod]
        public void SubscriptionReository_GetAllJournals()
        {
            //Act
            var journalList = _subscriptionRepository.GetAllJournals();

            //Assert
            Assert.IsTrue(journalList != null);
        }

        [TestMethod]

        public void SubscriptionReository_AddSubscriptionToJournal()
        {
            //Act
            var journalList = _subscriptionRepository.AddSubscription(1, 1);

            //Assert
            Assert.IsTrue(journalList != null);
        }

        [TestMethod]

        public void SubscriptionReository_GetJournalForSubscriberByUserId()
        {
            //Act
            var journalList = _subscriptionRepository.GetJournalsForSubscriber(1);
            //Assert
            Assert.IsTrue(journalList != null);
        }

        [TestMethod]

        public void SubscriptionReository_UnSubscribe()
        {
            //Act
            var journalList = _subscriptionRepository.UnSubscribe(1, 1);

            //Assert
            Assert.IsTrue(journalList != null);
        }

        [TestMethod]

        public void SubscriptionReository_GetJournalsForSubscriberByUserName()
        {
            //Act
            var journalList = _subscriptionRepository.GetJournalsForSubscriber("pappu");
            //Assert
            Assert.IsTrue(journalList != null);
        }
    }
}
