using System.Data.Entity;
using System.Diagnostics.CodeAnalysis;

namespace Journals.Repository.DataContext
{
    [ExcludeFromCodeCoverage]
    public class DropCreateDatabaseAlwaysInitializer : DropCreateDatabaseAlways<JournalsContext>
    {
        protected override void Seed(JournalsContext context)
        {
            DataInitializer.Initialize(context);
            base.Seed(context);
        }
    }
}