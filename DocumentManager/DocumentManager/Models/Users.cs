using Microsoft.AspNetCore.Identity;

namespace DocumentManager.Models
{
    public partial class Users : IdentityUser
    {
        public string FristName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public DateTime CreatedAt { get; set; }
        public virtual ICollection<Document> Documents { get; set; }
    }
}
