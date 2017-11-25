using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Journals.Model;

namespace Journals.Model
{
    [ExcludeFromCodeCoverage]
    public class IssueViewModel
    {
        public int IssueId { get; set; }

        [Required]
        public int JournalId { get; set; }
       
        [Range(1, int.MaxValue, ErrorMessage = "Please enter value greater than zero.")]
        public int VolumeNo { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Please enter value greater than zero.")]       
        public int IssueNo { get; set; }

        public string ContentType { get; set; }

        public string FileName { get; set; }

        public byte[] Content { get; set; }

        [Required, ValidateFile]
        public HttpPostedFileBase File { get; set; }

        public DateTime Creation { get; set; }

        public DateTime Updated { get; set; }
    }   
}
