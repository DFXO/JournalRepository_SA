using System.Diagnostics.CodeAnalysis;

namespace Journals.Repository
{
    public interface IDisposedTracker
    {
        bool IsDisposed { get; set; }
    }
}