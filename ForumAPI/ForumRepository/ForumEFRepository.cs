using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace ForumRepository
{
    public class ForumEFRepository : IForumRepository
    {
        private ForumContext forumDB;

        public ForumEFRepository(ForumContext dbContext)
        {
            this.forumDB = dbContext;            
        }
        public async Task<int> AddPost(Post post)
        {
            forumDB.Posts.Add(post);
            return await forumDB.SaveChangesAsync();
        }
        public async Task<Post> GetPost(int id)
        {
            return await forumDB.Posts.SingleOrDefaultAsync(p => p.Id == id);
        }
        public async Task<IEnumerable<Post>> GetAllPosts()
        {
            return await forumDB.Posts.ToListAsync<Post>();
        }
        public async Task<int> DeletePost(int id)
        {
            var post = await this.GetPost(id);

            forumDB.Posts.Remove(post);
            return await forumDB.SaveChangesAsync();
        }

        public async Task<int> AddComment(Comment comment)
        {    

            forumDB.Posts.Include(p => p.Comments).Where(p => p.Id == comment.PostId).FirstOrDefault().Comments.Add(comment);
            return await forumDB.SaveChangesAsync();
            
        }
        public async Task<Comment> GetComment(int id)
        {
            return await forumDB.Comments.SingleOrDefaultAsync(c => c.Id == id);
        }
        public async Task<IEnumerable<Comment>> GetAllComments()
        {
            return await forumDB.Comments.ToListAsync<Comment>();
        }
        public async Task<int> DeleteComment(int id)
        {
            var comment = await this.GetComment(id);
           
            forumDB.Comments.Remove(comment);
            return await forumDB.SaveChangesAsync();
        }
    }
}
