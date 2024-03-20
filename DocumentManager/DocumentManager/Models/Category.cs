using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DocumentManager.Models
{
    public partial class Category
    {
        public Category()
        {
            Documents = new HashSet<Document>();
        }

        public int Id { get; set; }
        [MaxLength(100)]
        public string? Name { get; set; }

        public virtual ICollection<Document> Documents { get; set; }
    }
}
