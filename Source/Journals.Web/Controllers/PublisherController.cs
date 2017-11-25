using System;
using AutoMapper;
using Journals.Model;
using Journals.Repository;
using Journals.Web.Filters;
using Journals.Web.Helpers;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Transactions;
using System.Web.Mvc;

namespace Journals.Web.Controllers
{
    [AuthorizeRedirect(Roles = "Publisher")]
    public class PublisherController : Controller
    {
        private readonly IJournalRepository _journalRepository;     
        private readonly IStaticMembershipService _membershipService;
        private readonly IIssuesRepository _issuesRepository;

        public PublisherController(IJournalRepository journalRepo, IIssuesRepository issuesRepository, IStaticMembershipService membershipService)
        {
            _journalRepository = journalRepo;
            _membershipService = membershipService;
            _issuesRepository = issuesRepository;
        }

        public ActionResult Index()
        {
            var userId = (int)_membershipService.GetUser().ProviderUserKey;

            List<Journal> allJournals = _journalRepository.GetAllJournals(userId);

            var journals = Mapper.Map<List<Journal>, List<JournalViewModel>>(allJournals);

            return View(journals);
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult GetFile(int Id)
        {
            Journal j = _journalRepository.GetJournalById(Id);
            if (j == null)
                throw new System.Web.Http.HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));

            return File(j.Content, j.ContentType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(JournalViewModel journal)
        {
            if (ModelState.IsValid)
            {
                var newJournal = Mapper.Map<JournalViewModel, Journal>(journal);
                JournalHelper.PopulateFile(journal.File, newJournal);

                newJournal.UserId = (int)_membershipService.GetUser().ProviderUserKey;

                var opStatus = _journalRepository.AddJournal(newJournal);
                
                if (!opStatus.Status)
                    throw new System.Web.Http.HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError));            

                return RedirectToAction("Index");
            }
            else
                return View(journal);
        }     

        public ActionResult Delete(int Id)
        {
            var selectedJournal = _journalRepository.GetJournalById(Id);
            var journal = Mapper.Map<Journal, JournalViewModel>(selectedJournal);

            return View(journal);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(JournalViewModel journal)
        {
            var selectedJournal = Mapper.Map<JournalViewModel, Journal>(journal);

            using (TransactionScope scope = new TransactionScope())
            {
                var deleteOpsStatus = _issuesRepository.DeleteAllIssuesByJournalId(journal.Id);

                var opStatus = _journalRepository.DeleteJournal(selectedJournal);

                if (!opStatus.Status || !deleteOpsStatus.Status)
                {
                    throw new System.Web.Http.HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
                }

                scope.Complete();
            }
                        
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int Id)
        {
            var journal = _journalRepository.GetJournalById(Id);

            var selectedJournal = Mapper.Map<Journal, JournalUpdateViewModel>(journal);

            return View(selectedJournal);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(JournalUpdateViewModel journal)
        {
            if (ModelState.IsValid)
            {
                var selectedJournal = Mapper.Map<JournalUpdateViewModel, Journal>(journal);
                JournalHelper.PopulateFile(journal.File, selectedJournal);

                var opStatus = _journalRepository.UpdateJournal(selectedJournal);
                if (!opStatus.Status)
                    throw new System.Web.Http.HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));

                return RedirectToAction("Index");
            }
            else
                return View(journal);
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            base.OnException(filterContext);
        }
    }
}