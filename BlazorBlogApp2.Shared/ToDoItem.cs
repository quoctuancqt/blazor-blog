using System.ComponentModel.DataAnnotations;

namespace BlazorBlogApp2.Shared
{
    public class ToDoItem
    {
        [Key]
        public int Id { get; set; }

        public string Item { get; set; }

        public int UserId { get; set; }

        public virtual Account Owner { get; set; }
    }
}
