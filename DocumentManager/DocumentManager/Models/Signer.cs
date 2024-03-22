using System;
using System.Collections.Generic;

namespace DocumentManager.Models
{
    public partial class Signer
    {
        public Signer()
        {
            Documents = new HashSet<Document>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public int? Pid { get; set; }
        public string? userId { get; set; }

        public virtual Users? Users { get; set; }
        public virtual Position? PidNavigation { get; set; }
        public virtual ICollection<Document> Documents { get; set; }
    }
}
