namespace CatanAPI.Models
{
    public class User : Microsoft.AspNetCore.Identity.IdentityUser
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public short Roles { get; set; }
        public int SessionId { get; set; }
    }
}