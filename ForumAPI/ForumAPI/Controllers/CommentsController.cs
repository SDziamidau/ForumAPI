using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ForumRepository;

namespace ForumAPI.Controllers
{
    [Produces("application/json")]
    [Route("v1/[controller]")]
    public class CommentsController : Controller
    {
        private IForumRepository forumRepo;

        public CommentsController(IForumRepository rep)
        {
            this.forumRepo = rep;
        }

        // GET: api/Comments
        [HttpGet]
        public async Task<IEnumerable<Comment>> GetComments()
        {
            return await forumRepo.GetAllComments();
        }

        // GET: api/Comments/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetComment([FromRoute] int id)
        {
            var comment = await forumRepo.GetComment(id);

            if (comment == null)
            {
                return NotFound();
            }

            return Ok(comment);
        }

        // POST: api/Comments
        [HttpPost]
        public async Task<IActionResult> AddComment([FromBody] Comment comment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            int added = await forumRepo.AddComment(comment);

            if (added == 0)
                return BadRequest();

            return Ok();
        }

        // DELETE: api/Comments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }            

            int deleted = await forumRepo.DeleteComment(id);

            if (deleted == 0)
            {
                return NotFound();
            }


            return Ok();
        }
    }
}