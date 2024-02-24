using System;
using System.Collections.Generic;

namespace DocumentManagementApp.Models
{
    public partial class Category
    {
        public Category()
        {
            Documents = new HashSet<Document>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<Document> Documents { get; set; }
    }
}
