using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ForumRepository;

namespace UnitTests
{
    [TestClass]
    public class EFRepositoryTests
    {
        private IForumRepository efRepo;

        private string postBody1 = "First Post Body";
        private string postBody2 = "Second Post Body";
        private string postBody3 = "Third Post Body";

        private string commentBody1 = "First Comment Body";
        private string commentBody2 = "Second Comment Body";

        public EFRepositoryTests()
        {

            DbContextOptionsBuilder<ForumContext> optionsBuilder = new DbContextOptionsBuilder<ForumContext>();

            string con = "Server=(localdb)\\mssqllocaldb; Database=forumdb; Trusted_Connection=True; MultipleActiveResultSets=true";
            optionsBuilder.UseSqlServer(con, b => b.MigrationsAssembly("ForumRepository"));

            efRepo = new ForumEFRepository(new ForumContext(optionsBuilder.Options));
        }

        [TestMethod]
        public void RepoConstructorWithValidatorTest()
        {
            Assert.IsNotNull(efRepo);
        }

        [TestMethod]
        public async Task AddPostTest()
        {


            int res = await efRepo.AddPost(new Post { Body = postBody1 });
            Assert.AreEqual(res, 1);

            res = await efRepo.AddPost(new Post { Body = postBody2 });
            Assert.AreEqual(res, 1);
            
        }

        [TestMethod]
        public async Task GetPostTest()
        {
            Post post = await efRepo.GetPost(1);
            Post post2 = await efRepo.GetPost(2);
            Post post3 = await efRepo.GetPost(3);

            Assert.IsNotNull(post);
            Assert.AreEqual<string>(post.Body, postBody1);
            Assert.AreEqual<string>(post2.Body, postBody2);
            Assert.IsNull(post3);

        }

        [TestMethod]
        public async Task GetAllPostsTest()
        {           
            IEnumerable<Post> posts = await efRepo.GetAllPosts();

            Assert.IsNotNull(posts);
            Assert.AreEqual(posts.ToList<Post>().Count(), 2);
        }

        [TestMethod]
        public async Task DeletePostTest()
        {
            
            Assert.AreEqual(1, await efRepo.DeletePost(1));

            IEnumerable<Post> posts = await efRepo.GetAllPosts();
            Assert.AreEqual(posts.ToList<Post>().Count, 1);

            Assert.IsNotNull(await efRepo.GetPost(2));
            Assert.IsNull(efRepo.GetPost(1));
        }


        [TestMethod]
        public async Task AddCommentTest()
        {
            int res = await efRepo.AddComment(new Comment { PostId = 2, Body = commentBody1 });
            Assert.AreEqual(res, 1);

            res = await efRepo.AddComment(new Comment { PostId = 2, Body = commentBody2 });
            Assert.AreEqual(res, 1);
        }

        [TestMethod]
        public async Task GetCommentTest()
        {
            Comment comment = await efRepo.GetComment(1);
            Comment comment2 = await efRepo.GetComment(2);
            Comment comment3 = await efRepo.GetComment(3);

            Assert.IsNotNull(comment);
            Assert.AreEqual<string>(comment.Body, commentBody1);
            Assert.AreEqual<string>(comment2.Body, commentBody2);
            Assert.IsNull(comment3);
        }

        [TestMethod]
        public async Task GetAllCommentsTest()
        {
            IEnumerable<Comment> comments = await efRepo.GetAllComments();

            Assert.IsNotNull(comments);
            Assert.AreEqual(comments.ToList<Comment>().Count(), 2);
        }

        [TestMethod]
        public async Task DeleteCommentTest()
        {
            Assert.AreEqual(1, await efRepo.DeleteComment(1));

            IEnumerable<Comment> comments = await efRepo.GetAllComments();
            Assert.AreEqual(comments.ToList<Comment>().Count, 1);

            Assert.IsNotNull(await efRepo.GetComment(2));
            Assert.IsNull(efRepo.GetComment(1));
        }
    }
}
