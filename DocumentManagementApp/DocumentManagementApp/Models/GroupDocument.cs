using System;
using System.Collections.Generic;

namespace DocumentManagementApp.Models
{
    public partial class GroupDocument
    {
        public GroupDocument()
        {
            Documents = new HashSet<Document>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime? ExpirationYear { get; set; }
        public string? Category { get; set; }
        public bool? Status { get; set; }

        public virtual ICollection<Document> Documents { get; set; }
    }
}
