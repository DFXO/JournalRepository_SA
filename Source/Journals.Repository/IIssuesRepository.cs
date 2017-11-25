using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Journals.Model;

namespace Journals.Repository
{
    public interface IIssuesRepository
    {
        List<Issue> GetAllIssues(int journalId);

        OperationStatus AddIssue(Issue issue);

        List<Issue> GetIssuesInVolume(int volumeId);

        int GetRunningVolumeId(int journalId);

        int GetRunningIssueId(int journalId, int volumeId);

        Issue GetIssues(int volumeId,int issueId);

        Issue GetIssueById(int Id);

        OperationStatus DeleteAllIssuesByJournalId(int journalId);
    }
}