using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using ForumRepository;

namespace ForumAPI.Controllers
{
    [Produces("application/json")]
    [Route("v1/[controller]")]
    public class PostsController : Controller
    {
        private IForumRepository forumRepo;

        public PostsController(IForumRepository rep)
        {
            this.forumRepo = rep;
        }
        // GET v1/Posts
        [HttpGet]
        public async Task<IEnumerable<Post>> GetPosts()
        {
            return await forumRepo.GetAllPosts();
        }

        // GET v1/Posts/3
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPost([FromRoute] int id)
        {
            var post = await forumRepo.GetPost(id);

            if (post == null)
            {
                return NotFound();
            }

            return Ok(post);
        }

        // POST v1/Posts
        [HttpPost]
        public async Task<IActionResult> AddPost([FromBody]Post post)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            int added = await forumRepo.AddPost(post);

            if (added == 0)
                return BadRequest();

            return CreatedAtAction("GetComment", new { id = post.Id }, post);
        }

        // DELETE v1/Posts/3
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            int deleted = await forumRepo.DeletePost(id);

            if (deleted == 0)
            {
                return NotFound();
            }


            return Ok();
        }
    }
}
