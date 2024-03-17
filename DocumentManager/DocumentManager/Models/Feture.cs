using System;
using System.Collections.Generic;

namespace DocumentManager.Models
{
    public partial class Feture
    {
        public Feture()
        {
            Gids = new HashSet<Group>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<Group> Gids { get; set; }
    }
}
