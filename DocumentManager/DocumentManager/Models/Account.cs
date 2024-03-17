using System;
using System.Collections.Generic;

namespace DocumentManager.Models
{
    public partial class Account
    {
        public Account()
        {
            Documents = new HashSet<Document>();
            Gids = new HashSet<Group>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Password { get; set; }
        public string? DisplayName { get; set; }
        public bool? Status { get; set; }

        public virtual ICollection<Document> Documents { get; set; }

        public virtual ICollection<Group> Gids { get; set; }
    }
}
