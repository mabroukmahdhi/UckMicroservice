using System;
using System.Linq;
using System.Threading.Tasks;
using Talks.Models.Talks;
using Talks.Models.Talks.Exceptions;
using Talks.Services.Foundations.Talks;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;

namespace Talks.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TalksController : RESTFulController
    {
        private readonly ITalkService talkService;

        public TalksController(ITalkService talkService) =>
            this.talkService = talkService;

        [HttpPost]
        public async ValueTask<ActionResult<Talk>> PostTalkAsync(Talk talk)
        {
            try
            {
                Talk addedTalk =
                    await this.talkService.AddTalkAsync(talk);

                return Created(addedTalk);
            }
            catch (TalkValidationException talkValidationException)
            {
                return BadRequest(talkValidationException.InnerException);
            }
            catch (TalkDependencyValidationException talkValidationException)
                when (talkValidationException.InnerException is InvalidTalkReferenceException)
            {
                return FailedDependency(talkValidationException.InnerException);
            }
            catch (TalkDependencyValidationException TalkDependencyValidationException)
               when (TalkDependencyValidationException.InnerException is AlreadyExistsTalkException)
            {
                return Conflict(TalkDependencyValidationException.InnerException);
            }
            catch (TalkDependencyException talkDependencyException)
            {
                return InternalServerError(talkDependencyException);
            }
            catch (TalkServiceException talkServiceException)
            {
                return InternalServerError(talkServiceException);
            }
        }

        [HttpGet]
        public ActionResult<IQueryable<Talk>> GetAllTalks()
        {
            try
            {
                IQueryable<Talk> retrievedTalks =
                    this.talkService.RetrieveAllTalks();

                return Ok(retrievedTalks);
            }
            catch (TalkDependencyException talkDependencyException)
            {
                return InternalServerError(talkDependencyException);
            }
            catch (TalkServiceException talkServiceException)
            {
                return InternalServerError(talkServiceException);
            }
        }

        [HttpGet("{talkId}")]
        public async ValueTask<ActionResult<Talk>> GetTalkByIdAsync(Guid talkId)
        {
            try
            {
                Talk talk = await this.talkService.RetrieveTalkByIdAsync(talkId);

                return Ok(talk);
            }
            catch (TalkValidationException talkValidationException)
                when (talkValidationException.InnerException is NotFoundTalkException)
            {
                return NotFound(talkValidationException.InnerException);
            }
            catch (TalkValidationException talkValidationException)
            {
                return BadRequest(talkValidationException.InnerException);
            }
            catch (TalkDependencyException talkDependencyException)
            {
                return InternalServerError(talkDependencyException);
            }
            catch (TalkServiceException talkServiceException)
            {
                return InternalServerError(talkServiceException);
            }
        }

        [HttpPut]
        public async ValueTask<ActionResult<Talk>> PutTalkAsync(Talk talk)
        {
            try
            {
                Talk modifiedTalk =
                    await this.talkService.ModifyTalkAsync(talk);

                return Ok(modifiedTalk);
            }
            catch (TalkValidationException talkValidationException)
                when (talkValidationException.InnerException is NotFoundTalkException)
            {
                return NotFound(talkValidationException.InnerException);
            }
            catch (TalkValidationException talkValidationException)
            {
                return BadRequest(talkValidationException.InnerException);
            }
            catch (TalkDependencyValidationException talkValidationException)
                when (talkValidationException.InnerException is InvalidTalkReferenceException)
            {
                return FailedDependency(talkValidationException.InnerException);
            }
            catch (TalkDependencyValidationException talkDependencyValidationException)
               when (talkDependencyValidationException.InnerException is AlreadyExistsTalkException)
            {
                return Conflict(talkDependencyValidationException.InnerException);
            }
            catch (TalkDependencyException talkDependencyException)
            {
                return InternalServerError(talkDependencyException);
            }
            catch (TalkServiceException talkServiceException)
            {
                return InternalServerError(talkServiceException);
            }
        }

        [HttpDelete("{talkId}")]
        public async ValueTask<ActionResult<Talk>> DeleteTalkByIdAsync(Guid talkId)
        {
            try
            {
                Talk deletedTalk =
                    await this.talkService.RemoveTalkByIdAsync(talkId);

                return Ok(deletedTalk);
            }
            catch (TalkValidationException talkValidationException)
                when (talkValidationException.InnerException is NotFoundTalkException)
            {
                return NotFound(talkValidationException.InnerException);
            }
            catch (TalkValidationException talkValidationException)
            {
                return BadRequest(talkValidationException.InnerException);
            }
            catch (TalkDependencyValidationException talkDependencyValidationException)
                when (talkDependencyValidationException.InnerException is LockedTalkException)
            {
                return Locked(talkDependencyValidationException.InnerException);
            }
            catch (TalkDependencyValidationException talkDependencyValidationException)
            {
                return BadRequest(talkDependencyValidationException);
            }
            catch (TalkDependencyException talkDependencyException)
            {
                return InternalServerError(talkDependencyException);
            }
            catch (TalkServiceException talkServiceException)
            {
                return InternalServerError(talkServiceException);
            }
        }
    }
}