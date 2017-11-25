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
    public class IssuesViewModel
    {
        public int Id { get; set; }

        [Required]
        public int JournalId { get; set; }
       
        [Range(1, int.MaxValue, ErrorMessage = "The value must be greater than 0")]
        public int Volume { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "The value must be greater than 0")]       
        public int Issue { get; set; }

        public string ContentType { get; set; }

        public string FileName { get; set; }

        public byte[] Content { get; set; }

        [Required, ValidateFile]
        public HttpPostedFileBase File { get; set; }

        public DateTime CreatedDate { get; set; }
    }   
}
