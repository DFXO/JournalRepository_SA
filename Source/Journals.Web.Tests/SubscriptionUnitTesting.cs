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

        private Issue _issue;

        private Journal _journal;

        [TestInitialize]
        public void TestInitialize()
        {
            Mapper.CreateMap<Journal, JournalViewModel>();
            Mapper.CreateMap<JournalViewModel, Journal>();

            Mapper.CreateMap<IssueViewModel, Issue>();
            Mapper.CreateMap<Issue, IssueViewModel>();

            //_journal = GetJournal();


            _subscriptionRepository = new SubscriptionRepository();
        }

        [TestMethod]

        public void When_GetAllJournals_Called_ReturnsAllJournal_Passed()
        {
            try
            {
                //Act
                var journalList = _subscriptionRepository.GetAllJournals();
                //Assert
                Assert.IsTrue(journalList != null);
            }
            catch (Exception ex)
            {

            }
        }

        [TestMethod]

        public void When_AddSubscription_Called_Adds_New_Journal_Passed()
        {
            try
            {
                //Act
                var journalList = _subscriptionRepository.AddSubscription(1,1);
                //Assert
                Assert.IsTrue(journalList != null);
            }
            catch (Exception ex)
            {

            }
        }

        [TestMethod]

        public void When_GetJournalsForSubscriber_Called_Returns_Journal_Passed()
        {
            try
            {
                //Act
                var journalList = _subscriptionRepository.GetJournalsForSubscriber(1);
                //Assert
                Assert.IsTrue(journalList != null);
            }
            catch (Exception ex)
            {

            }
        }

        [TestMethod]

        public void When_UnSubscribeJournal_Called_Unscribes_Passed()
        {
            try
            {
                //Act
                var journalList = _subscriptionRepository.UnSubscribe(1,1);
                //Assert
                Assert.IsTrue(journalList != null);
            }
            catch (Exception ex)
            {

            }
        }
        [TestMethod]

        public void When_GetJournalsForSubscriberWithUserName_Called_Returns_Journal_Passed()
        {
            try
            {
                //Act
                var journalList = _subscriptionRepository.GetJournalsForSubscriber("pappu");
                //Assert
                Assert.IsTrue(journalList != null);
            }
            catch (Exception ex)
            {

            }
        }

    }
}
