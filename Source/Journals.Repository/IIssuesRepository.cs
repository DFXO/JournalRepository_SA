using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Journals.Model;

namespace Journals.Repository
{
    public interface IIssuesRepository
    {
        List<Issues> GetAllIssues(int journalId);

        OperationStatus AddIssue(Issues issue);

        List<Issues> GetIssuesInVolume(int volumeId);

        int GetRunningVolumeId(int journalId);

        int GetRunningIssueId(int journalId, int volumeId);

        Issues GetIssues(int volumeId,int issueId);

        Issues GetIssueById(int Id);

        OperationStatus DeleteAllIssuesByJournalId(int journalId);
    }
}