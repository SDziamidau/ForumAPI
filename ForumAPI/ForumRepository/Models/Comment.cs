using System.ComponentModel.DataAnnotations;

namespace ForumRepository
{
    public class Comment
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Comment postId reference is empty")]
        public int PostId { get; set; }
        [Required(ErrorMessage = "Comment body is empty")]
        public string Body { get; set; }
    }
}
