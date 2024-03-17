using System;
using System.Collections.Generic;

namespace DocumentManager.Models
{
    public partial class Sercurity
    {
        public Sercurity()
        {
            Documents = new HashSet<Document>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int? Level { get; set; }

        public virtual ICollection<Document> Documents { get; set; }
    }
}
