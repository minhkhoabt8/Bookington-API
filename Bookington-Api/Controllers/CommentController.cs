using Bookington.Core.Enums;
using Bookington.Infrastructure.DTOs.Account;
using Bookington.Infrastructure.DTOs.ApiResponse;
using Bookington.Infrastructure.DTOs.Comment;
using Bookington.Infrastructure.Services.Implementations;
using Bookington.Infrastructure.Services.Interfaces;
using Bookington_Api.Authorizers;
using Microsoft.AspNetCore.Mvc;

namespace Bookington_Api.Controllers
{
    [Route("comments")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        /// <summary>
        /// Create a new comment
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost("post")]
        [RoleAuthorize(AccountRole.customer)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiUnauthorizedResponse))]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(ApiOkResponse<CommentReadDTO>))]
        public async Task<IActionResult> CreateAsync(CommentWriteDTO dto)
        {
            var created = await _commentService.CreateAsync(dto);
            return ResponseFactory.Created(created);
        }

        /// <summary>
        /// Delete account
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [RoleAuthorize(AccountRole.customer)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ApiNotFoundResponse))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized, Type = typeof(ApiUnauthorizedResponse))]
        public async Task<IActionResult> DeleteComment(string id)
        {
            await _commentService.DeleteAsync(id);

            return ResponseFactory.NoContent();
        }


        /// <summary>
        /// Query comments
        /// </summary>
        /// <returns></returns>
        [HttpGet("query")]
        [RoleAuthorize(AccountRole.customer)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiPaginatedOkResponse<PaginatedResponse<CommentReadDTO>>))]
        public async Task<IActionResult> QueryComments([FromQuery] CommentQuery query)
        {
            var comments = await _commentService.GetAllCommentsOfCourt(query);

            return ResponseFactory.PaginatedOk(comments);
        }
    }

}
