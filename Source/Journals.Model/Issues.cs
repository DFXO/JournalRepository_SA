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
    public class Issues
    {
        public int Id { get; set; }

        [ForeignKey("JournalId")]
        public Journal Journal { get; set; }

        public int JournalId { get; set; }

        public int Volume { get; set; }

        public int Issue { get; set; }

        public string FileName { get; set; }

        public string ContentType { get; set; }

        public byte[] Content { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
