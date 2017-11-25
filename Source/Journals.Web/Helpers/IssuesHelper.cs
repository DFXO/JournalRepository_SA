using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web;
using Journals.Model;

namespace Medico.Web.Helpers
{
    [ExcludeFromCodeCoverage]
    public static class IssuesHelper
    {
        public static void PopulateFile(HttpPostedFileBase file, Issues journal)
        {
            if (file != null && file.ContentLength > 0)
            {
                journal.FileName = System.IO.Path.GetFileName(file.FileName);
                journal.ContentType = file.ContentType;

                using (var reader = new System.IO.BinaryReader(file.InputStream))
                {
                    journal.Content = reader.ReadBytes(file.ContentLength);
                }
            }
        }
    }
}