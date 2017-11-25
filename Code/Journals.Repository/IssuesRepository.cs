using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Journals.Model;
using Journals.Repository;
using Journals.Repository.DataContext;

namespace Medico.Repository
{
    
    public class IssuesRepository : RepositoryBase<JournalsContext>, IIssuesRepository
    {
        public List<Issue> GetAllIssues(int journalId)
        {
            using (DataContext)
            {
                return DataContext.Issues.Where(issue=>issue.JournalId == journalId).ToList();
            }
        }

        public OperationStatus AddIssue(Issue issue)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                using (DataContext)
                {
                    issue.Creation = DateTime.Now;
                    issue.Updated = DateTime.Now;
                    var j = DataContext.Issues.Add(issue);
                    DataContext.SaveChanges();
                }
            }
            catch (Exception e)
            {
                opStatus = OperationStatus.CreateFromException("Error adding issue: ", e);
            }

            return opStatus;
        }

        public List<Issue> GetIssuesInVolume(int volumeId)
        {
            using (DataContext)
            {
                return DataContext.Issues.Where(issue=>issue.VolumeNo == volumeId).ToList();
            }
        }

        public int GetRunningVolumeId(int journalId)
        {
            using (DataContext)
            {
                var latestIssue = DataContext.Issues.Where(issue => issue.JournalId == journalId).
                             OrderBy(issue => issue.Creation)
                            .FirstOrDefault();

                // If no volume created then retun 1 as first volume
                if (latestIssue == null)
                {
                    return 1;
                }
                else
                {
                    // if any volume found for current year then retun current volume.
                    if (latestIssue.Creation.Year == DateTime.Now.Year)
                    {
                        return latestIssue.VolumeNo;
                    }

                    //if no volume found for current year.
                    return latestIssue.VolumeNo + 1;
                }
            }
        }

        public int GetRunningIssueId(int journalId, int volumeId)
        {
            using (DataContext)
            {
                var issues = DataContext.Issues.Where(issue => issue.JournalId == journalId && issue.VolumeNo == volumeId);                            

                if (issues.Any())
                {
                    Issue recentIssue = issues.OrderBy(issue => issue.Creation).FirstOrDefault();

                    if (recentIssue != null)
                    {
                        if (recentIssue.Creation.Month != DateTime.Now.Month)
                        {
                            return recentIssue.IssueNo + 1;
                        }

                        return 0;
                    }
                }
            }

            return 1;
        }

        public Issue GetIssues(int volumeId, int issueId)
        {
            using (DataContext)
            {               
               return DataContext.Issues.SingleOrDefault(issue => issue.VolumeNo == volumeId && issue.IssueNo == issueId);
            }
        }

        public Issue GetIssueById(int Id)
        {
            using (DataContext)
            {
                return DataContext.Issues.FirstOrDefault(issue => issue.IssueNo == Id);
            }
        }

        public OperationStatus DeleteAllIssuesByJournalId(int journalId)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                using (DataContext)
                {
                    var issues = DataContext.Issues.Where(j => j.JournalId == journalId);

                    foreach (var issue in issues)
                    {
                        DataContext.Issues.Remove(issue);
                    }
                   
                    DataContext.SaveChanges();
                }
            }
            catch (Exception e)
            {
                opStatus = OperationStatus.CreateFromException("Error deleting issue: ", e);
            }

            return opStatus;
        }
    }
}
