using System;
using System.Collections.Generic;

namespace DocumentManager.Models
{
    public partial class Group
    {
        public Group()
        {
            Aids = new HashSet<Account>();
            Fids = new HashSet<Feture>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<Account> Aids { get; set; }
        public virtual ICollection<Feture> Fids { get; set; }
    }
}
