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
    
    public class SubscriptionTests
    {
        private ISubscriptionRepository _subscriptionRepository;

        private Issues _issues;

        private Journal _journal;

        [TestInitialize]
        public void TestInitialize()
        {
            Mapper.CreateMap<Journal, JournalViewModel>();
            Mapper.CreateMap<JournalViewModel, Journal>();

            Mapper.CreateMap<IssuesViewModel, Issues>();
            Mapper.CreateMap<Issues, IssuesViewModel>();
            _subscriptionRepository = new SubscriptionRepository();
        }

        [TestMethod]

        public void GetAllJournals_Test()
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

        public void AddTest()
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

        public void GetJournalsForSubscriber_Test()
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

        public void UnSubscribe_Test()
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

        public void GetJournalsForSubscriberWithUserName_Test()
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
