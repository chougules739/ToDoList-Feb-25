using System;

namespace DataModels
{
    public partial class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string EmailId { get; set; }
        public string ContactNo { get; set; }
        public int RoleId { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
