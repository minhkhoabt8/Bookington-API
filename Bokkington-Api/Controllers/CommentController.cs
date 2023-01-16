using Bookington.Infrastructure.DTOs.ApiResponse;
using Bookington.Infrastructure.DTOs.Comment;
using Bookington.Infrastructure.DTOs.Court;
using Bookington.Infrastructure.Services.Interfaces;
using Bookington_Api.Filters;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Bookington_Api.Controllers
{
    /// <summary> 
    /// Comment Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        /// <summary>        
        /// </summary>
        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        /// <summary>
        /// Get All Comments
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CommentReadDTO>))]
        public async Task<IActionResult> GetAllAsync()
        {
            var comments = await _commentService.GetAllAsync();
            return ResponseFactory.Ok(comments);
        }

        /// <summary>
        /// Get A Comment Details
        /// </summary>
        /// <returns></returns>
        /// <param name="id"></param>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CommentReadDTO))]
        public async Task<IActionResult> GetDetailsAsync(string id)
        {
            var comment = await _commentService.GetByIdAsync(id);
            return ResponseFactory.Ok(comment);
        }

        /// <summary>
        /// Create A New Comment
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [ServiceFilter(typeof(AutoValidateModelState))]
        public async Task<IActionResult> CreateAsync(CommentWriteDTO dto)
        {
            var newComment = await _commentService.CreateAsync(dto);
            return ResponseFactory.Created(newComment);
        }


        /// <summary>
        /// Update A Comment
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ServiceFilter(typeof(AutoValidateModelState))]
        public async Task<IActionResult> UpdateAsync(string id, CommentWriteDTO dto)
        {
            var updatedComment = await _commentService.UpdateAsync(id, dto);
            return ResponseFactory.Ok(updatedComment);
        }

        /// <summary>
        /// Delete A Comment
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            await _commentService.DeleteAsync(id);
            return ResponseFactory.NoContent();
        }
    }
}
