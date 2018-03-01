using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ForumRepository
{
    public class Post
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Post body is empty")]
        public string Body { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
