using System.Collections.Generic;
using System.Threading.Tasks;


namespace ForumRepository
{
    public interface IForumRepository
    {
        Task<int> AddPost(Post post);
        Task<Post> GetPost(int id);
        Task<IEnumerable <Post>> GetAllPosts();
        Task<int> DeletePost(int id);

        Task<int> AddComment(Comment comment);
        Task<Comment> GetComment(int id);
        Task<IEnumerable<Comment>> GetAllComments();
        Task<int> DeleteComment(int id);
    }
}
