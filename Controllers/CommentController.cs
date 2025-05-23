using Microsoft.AspNetCore.Mvc;
using api.Models;
using api.Dtos.Comment;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentController : ControllerBase
    {
        private static List<Comment> _comments = new List<Comment>
        {
            new Comment { Id = 1, Title = "Good stock", Content = "Great performance", StockId = 1 },
            new Comment { Id = 2, Title = "Risky move", Content = "High volatility", StockId = 2 }
        };

        // This controller handles all comment-related operations
        // It uses an in-memory list to simulate a database
        [HttpGet("get-all/{id:int}")]
        public IActionResult GetAll()
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var commentDtos = _comments.Select(c => new CommentDto
            {
                Id = c.Id,
                Title = c.Title,
                Content = c.Content,
                CreatedOn = c.CreatedOn,
                StockId = c.StockId
            }).ToList();

            return Ok(commentDtos);
        }

        // This method retrieves comments by stock ID
        // It filters the comments based on the provided stock ID
        // and returns a list of comments associated with that stock
        [HttpGet("get-by-stock/{stockId:int}")]
        public IActionResult GetByStockId(int stockId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var comments = _comments
                .Where(c => c.StockId == stockId)
                .Select(c => new CommentDto
                {
                    Id = c.Id,
                    Title = c.Title,
                    Content = c.Content,
                    CreatedOn = c.CreatedOn,
                    StockId = c.StockId
                }).ToList();

            return Ok(comments);
        }

        // This method creates a new comment
        [HttpPost("create/{stockId:int}")]
        public IActionResult Create(string stockId, [FromBody] CommentDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var newComment = new Comment
            {
                Id = _comments.Max(c => c.Id) + 1,
                Title = dto.Title,
                Content = dto.Content,
                CreatedOn = DateTime.Now, // auto timestamp
                StockId = dto.StockId
            };

            _comments.Add(newComment);

            // Return the actual comment data
            return CreatedAtAction(nameof(GetAll), new { id = newComment.Id }, new CommentDto
            {
                Id = newComment.Id,
                Title = newComment.Title,
                Content = newComment.Content,
                CreatedOn = newComment.CreatedOn,
                StockId = newComment.StockId
            });
        }

        // This method updates an existing comment
        [HttpPut("update/{id:int}")]
        public IActionResult Update(int id, [FromBody] CommentDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var comment = _comments.FirstOrDefault(c => c.Id == id);
            if (comment == null)
                return NotFound();

            comment.Title = dto.Title;
            comment.Content = dto.Content;

            return Ok(new CommentDto
            {
                Id = comment.Id,
                Title = comment.Title,
                Content = comment.Content,
                CreatedOn = comment.CreatedOn,
                StockId = comment.StockId
            });
        }

        // This method deletes a comment by ID
        [HttpDelete("delete/{id:int}")]
        public Task<IActionResult> Delete(int id)
        {
            if (!ModelState.IsValid)
                return Task.FromResult<IActionResult>(BadRequest(ModelState));
            
            var comment = _comments.FirstOrDefault(c => c.Id == id);
            if (comment == null)
                return Task.FromResult<IActionResult>(NotFound());

            _comments.Remove(comment);

            return Task.FromResult<IActionResult>(NoContent());
        }
    } 
}
