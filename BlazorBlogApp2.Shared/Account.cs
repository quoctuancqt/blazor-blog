using System.ComponentModel.DataAnnotations;

namespace BlazorBlogApp2.Shared
{
    public class Account
    {
        [Key]
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
