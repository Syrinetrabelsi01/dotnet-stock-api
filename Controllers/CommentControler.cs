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

        [HttpGet("get")]
        public IActionResult GetAll()
        {
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

        [HttpGet("get-by-stock/{stockId}")]
        public IActionResult GetByStockId(int stockId)
        {
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

        [HttpPost("create")]
        public IActionResult Create([FromBody] CommentDto dto)
        {
            var newComment = new Comment
            {
                Id = _comments.Max(c => c.Id) + 1,
                Title = dto.Title,
                Content = dto.Content,
                CreatedOn = DateTime.Now,
                StockId = dto.StockId
            };

            _comments.Add(newComment);

            return CreatedAtAction(nameof(GetAll), new { id = newComment.Id }, dto);
        }
    }
}
