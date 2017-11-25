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

        public ActionResult Index(int journalId)
        {
            if (Session != null)
            {
                Session["journalId"] = journalId;
            }

            List<Issues> allIssues = _issuesRepository.GetAllIssues(journalId);

            List<IssuesViewModel> issues = Mapper.Map<List<Issues>, List<IssuesViewModel>>(allIssues);

            return View(issues);
        }   

        public ActionResult GetFile(int Id)
        {
            Issues issue = _issuesRepository.GetIssueById(Id);
            if (issue == null)
                throw new System.Web.Http.HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));

            return File(issue.Content, issue.ContentType);
        }

        public ActionResult Create(int journalId)
        {   
            int runningVolume = _issuesRepository.GetRunningVolumeId(journalId);

            int runningIssueId = _issuesRepository.GetRunningIssueId(journalId, runningVolume);
        
            if (runningIssueId == 0)
            {
                ModelState.AddModelError("", "Issue for current month is already added.");
            }
            
            IssuesViewModel issueViewModel = new IssuesViewModel
            {
                Volume = runningVolume,
                Issue = runningIssueId,
                JournalId = journalId,
            };

            return View(issueViewModel);            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IssuesViewModel newIssueModel)
        {
            if (ModelState.IsValid)
            {
                var newIssue = Mapper.Map<IssuesViewModel, Issues>(newIssueModel);

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
