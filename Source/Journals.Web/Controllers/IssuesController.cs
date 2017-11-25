using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Journals.Model;
using Journals.Repository;
using Journals.Web.Filters;
using Medico.Web.Helpers;

namespace Journals.Web.Controllers
{
    [AuthorizeRedirect(Roles = "Publisher")]
    public class IssuesController : Controller
    {
        private readonly IIssuesRepository _issuesRepository;        

        public IssuesController(IIssuesRepository issuesRepository)
        {                    
            _issuesRepository = issuesRepository;
        }

        /// <summary>
        /// Index - This method is used to get journal by journal Id.
        /// </summary>
        /// <param name="journalId"></param>
        /// <returns></returns>
        public ActionResult Index(int journalId)
        {
            if (Session != null)
            {
                Session["journalId"] = journalId;
            }

            List<Issue> allIssues = _issuesRepository.GetAllIssues(journalId);

            List<IssueViewModel> issues = Mapper.Map<List<Issue>, List<IssueViewModel>>(allIssues);

            return View(issues);
        }   

        /// <summary>
        /// This method will be used to get the file contents by id.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ActionResult GetFile(int Id)
        {
            Issue issue = _issuesRepository.GetIssueById(Id);
            if (issue == null)
                throw new System.Web.Http.HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));

            return File(issue.Content, issue.ContentType);
        }

        /// <summary>
        /// This method will be used to return journal bby journal id.
        /// </summary>
        /// <param name="journalId"></param>
        /// <returns></returns>
        public ActionResult Create(int journalId)
        {   
            int runningVolume = _issuesRepository.GetRunningVolumeId(journalId);

            int runningIssueId = _issuesRepository.GetRunningIssueId(journalId, runningVolume);
        
            if (runningIssueId == 0)
            {
                ModelState.AddModelError(string.Empty, "Issue for current month is already added.");
            }
            
            IssueViewModel issueViewModel = new IssueViewModel
            {
                VolumeNo = runningVolume,
                IssueNo = runningIssueId,
                JournalId = journalId,
            };

            return View(issueViewModel);            
        }

        /// <summary>
        /// This method will be used to create new issues for journal.
        /// </summary>
        /// <param name="newIssueModel"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IssueViewModel newIssueModel)
        {
            if (ModelState.IsValid)
            {
                var newIssue = Mapper.Map<IssueViewModel, Issue>(newIssueModel);

                IssuesHelper.PopulateFile(newIssueModel.File, newIssue);

                var opStatus = _issuesRepository.AddIssue(newIssue);

                if (!opStatus.Status)
                {
                    throw new System.Web.Http.HttpResponseException(
                        new HttpResponseMessage(HttpStatusCode.InternalServerError));
                }

                return RedirectToAction("Index",new { journalId = Session["journalId"] });
            }

            return View(newIssueModel);
        }
    }
}
