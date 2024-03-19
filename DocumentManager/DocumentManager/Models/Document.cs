using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DocumentManager.Models
{
    public partial class Document
    {
        public int Id { get; set; }
        public string? Symbol { get; set; }//
        public string? Receiver { get; set; }//
        public string? Describe { get; set; }//
        public DateTime? DateSign { get; set; }//
        public DateTime? DateTo { get; set; }//
        public DateTime? DateOut { get; set; }//
        public int? Page { get; set; }
        public double? Quantity { get; set; }
        public bool? Status { get; set; }
        public int? SignerId { get; set; }//
        public int? SecurityId { get; set; }//
        public int? EmergencyId { get; set; }//
        public int? GroupDocumentId { get; set; }//
        public int? NameDocumentId { get; set; }//
        public int? SpecializedId { get; set; }//
        public int? CategoryId { get; set; }
        [MaxLength(450)]
        public string? Aid { get; set; }//
        public int? AgencyIssuesId { get; set; }//

        public virtual AgencyIssue? AgencyIssues { get; set; }
        public virtual Category? Category { get; set; }
        public virtual Emergency? Emergency { get; set; }
        public virtual GroupDocument? GroupDocument { get; set; }
        public virtual NameDocument? NameDocument { get; set; }
        public virtual Sercurity? Security { get; set; }
        public virtual Signer? Signer { get; set; }
        public virtual Specialize? Specialized { get; set; }
        public virtual Users? Users { get; set; }
    }
}
