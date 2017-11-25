using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Journals.Model;

namespace Journals.Model
{
    [ExcludeFromCodeCoverage]
    public class Issue
    {
        public int IssueId { get; set; }

        [ForeignKey("JournalId")]
        public Journal Journal { get; set; }

        public int JournalId { get; set; }

        public int VolumeNo { get; set; }

        public int IssueNo { get; set; }

        public string FileName { get; set; }

        public byte[] Content { get; set; }

        public string ContentType { get; set; }        

        public DateTime Creation { get; set; }

        public DateTime Updated { get; set; }

        public bool Active { get; set; }
    }
}
