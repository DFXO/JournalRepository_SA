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
        public List<Issues> GetAllIssues(int journalId)
        {
            using (DataContext)
            {
                return DataContext.Issues.Where(issue=>issue.JournalId == journalId).ToList();
            }
        }

        public OperationStatus AddIssue(Issues issue)
        {
            var opStatus = new OperationStatus { Status = true };
            try
            {
                using (DataContext)
                {
                    issue.CreatedDate = DateTime.Now;
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

        public List<Issues> GetIssuesInVolume(int volumeId)
        {
            using (DataContext)
            {
                return DataContext.Issues.Where(issue=>issue.Volume == volumeId).ToList();
            }
        }

        public int GetRunningVolumeId(int journalId)
        {
            using (DataContext)
            {
                var latestIssue = DataContext.Issues.Where(issue => issue.JournalId == journalId).
                             OrderBy(issue => issue.CreatedDate)
                            .FirstOrDefault();

                // If no volume created then retun 1 as first volume
                if (latestIssue == null)
                {
                    return 1;
                }
                else
                {
                    // if any volume found for current year then retun current volume.
                    if (latestIssue.CreatedDate.Year == DateTime.Now.Year)
                    {
                        return latestIssue.Volume;
                    }

                    //if no volume found for current year.
                    return latestIssue.Volume + 1;
                }
            }
        }

        public int GetRunningIssueId(int journalId, int volumeId)
        {
            using (DataContext)
            {
                var issues = DataContext.Issues.Where(issue => issue.JournalId == journalId && issue.Volume == volumeId);                            

                if (issues.Any())
                {
                    Issues recentIssue = issues.OrderBy(issue => issue.CreatedDate).FirstOrDefault();

                    if (recentIssue != null)
                    {
                        if (recentIssue.CreatedDate.Month != DateTime.Now.Month)
                        {
                            return recentIssue.Issue + 1;
                        }

                        return 0;
                    }
                }
            }

            return 1;
        }

        public Issues GetIssues(int volumeId, int issueId)
        {
            using (DataContext)
            {               
               return DataContext.Issues.SingleOrDefault(issue => issue.Volume == volumeId && issue.Issue == issueId);
            }
        }

        public Issues GetIssueById(int Id)
        {
            using (DataContext)
            {
                return DataContext.Issues.FirstOrDefault(issue => issue.Issue == Id);
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
