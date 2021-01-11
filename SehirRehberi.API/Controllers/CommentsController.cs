using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SehirRehberi.API.Data;
using SehirRehberi.API.Dtos;
using SehirRehberi.API.Models;

namespace SehirRehberi.API.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
   // [ApiController]
    public class CommentsController : ControllerBase
    {

        IAppRepository _appRepository;
        public CommentsController(IAppRepository appRepository)
        {
            _appRepository = appRepository;
        }

        [HttpGet("GetComments")]
        public ActionResult GetCommentsByPhotoId(int photoId)
        {
            var commentsToReturn = _appRepository.GetCommentsByPhoto(photoId);
            return Ok(commentsToReturn);
        }

        [HttpPost("AddComment")]
        public ActionResult AddComment([FromBody]Comments comment)
        {
            _appRepository.Add(comment);
            _appRepository.SaveAll();
            return Ok(comment);
        }

        [HttpPost("UpVote")]
        public ActionResult UpVoteComment(int commentId,int upDown)
        {
            _appRepository.CommentUpvote(commentId, upDown);
            _appRepository.SaveAll();
            return Ok();
        }

    }
}
