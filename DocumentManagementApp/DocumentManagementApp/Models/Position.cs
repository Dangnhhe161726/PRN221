using System;
using System.Collections.Generic;

namespace DocumentManagementApp.Models
{
    public partial class Position
    {
        public Position()
        {
            Signers = new HashSet<Signer>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<Signer> Signers { get; set; }
    }
}
