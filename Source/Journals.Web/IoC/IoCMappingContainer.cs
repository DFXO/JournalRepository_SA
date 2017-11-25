using System.Diagnostics.CodeAnalysis;
using Journals.Repository;
using Journals.Web.Controllers;
using Medico.Repository;
using Microsoft.Practices.Unity;

namespace Journals.Web.IoC
{
    [ExcludeFromCodeCoverage]
    public static class IoCMappingContainer
    {
        private static IUnityContainer _Instance = new UnityContainer();

        static IoCMappingContainer()
        {
        }

        public static IUnityContainer GetInstance()
        {
            _Instance.RegisterType<HomeController>();
            _Instance.RegisterType<PublisherController>();
            _Instance.RegisterType<SubscriberController>();
            _Instance.RegisterType<IssuesController>();

            _Instance.RegisterType<IJournalRepository, JournalRepository>(new HierarchicalLifetimeManager());
            _Instance.RegisterType<ISubscriptionRepository, SubscriptionRepository>(new HierarchicalLifetimeManager());
            _Instance.RegisterType<IStaticMembershipService, StaticMembershipService>(new HierarchicalLifetimeManager());
            _Instance.RegisterType<IIssuesRepository, IssuesRepository>(new HierarchicalLifetimeManager());
            return _Instance;
        }
    }
}