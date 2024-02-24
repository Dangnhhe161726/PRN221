﻿using System;
using System.Collections.Generic;

namespace DocumentManagementApp.Models
{
    public partial class Specialize
    {
        public Specialize()
        {
            Documents = new HashSet<Document>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<Document> Documents { get; set; }
    }
}
