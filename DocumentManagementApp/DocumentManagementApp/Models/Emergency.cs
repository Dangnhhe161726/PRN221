using System;
using System.Collections.Generic;

namespace DocumentManagementApp.Models
{
    public partial class Emergency
    {
        public Emergency()
        {
            Documents = new HashSet<Document>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public int? Level { get; set; }

        public virtual ICollection<Document> Documents { get; set; }
    }
}
