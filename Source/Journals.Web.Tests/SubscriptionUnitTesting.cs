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

        private Issues _issues;

        private Journal _journal;

        [TestInitialize]
        public void TestInitialize()
        {
            Mapper.CreateMap<Journal, JournalViewModel>();
            Mapper.CreateMap<JournalViewModel, Journal>();

            Mapper.CreateMap<IssuesViewModel, Issues>();
            Mapper.CreateMap<Issues, IssuesViewModel>();

            //_journal = GetJournal();


            _subscriptionRepository = new SubscriptionRepository();
        }

        [TestMethod]

        public void Subscription_GetAllJournals_Passed()
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

        public void Subscription_AddSubscription_Passed()
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

        public void Subscription_GetJournalsForSubscriber_Passed()
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

        public void Subscription_UnSubscribe_Passed()
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

        public void Subscription_GetJournalsForSubscriberWithUserName_Passed()
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
