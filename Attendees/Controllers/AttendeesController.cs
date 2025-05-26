using System;
using System.Linq;
using System.Threading.Tasks;
using Attendees.Models.Attendees;
using Attendees.Models.Attendees.Exceptions;
using Attendees.Services.Foundations.Attendees;
using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;

namespace Attendees.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AttendeesController : RESTFulController
    {
        private readonly IAttendeeService attendeeService;

        public AttendeesController(IAttendeeService attendeeService) =>
            this.attendeeService = attendeeService;

        [HttpPost]
        public async ValueTask<ActionResult<Attendee>> PostAttendeeAsync(Attendee attendee)
        {
            try
            {
                Attendee addedAttendee =
                    await this.attendeeService.AddAttendeeAsync(attendee);

                return Created(addedAttendee);
            }
            catch (AttendeeValidationException attendeeValidationException)
            {
                return BadRequest(attendeeValidationException.InnerException);
            }
            catch (AttendeeDependencyValidationException attendeeValidationException)
                when (attendeeValidationException.InnerException is InvalidAttendeeReferenceException)
            {
                return FailedDependency(attendeeValidationException.InnerException);
            }
            catch (AttendeeDependencyValidationException AttendeeDependencyValidationException)
               when (AttendeeDependencyValidationException.InnerException is AlreadyExistsAttendeeException)
            {
                return Conflict(AttendeeDependencyValidationException.InnerException);
            }
            catch (AttendeeDependencyException attendeeDependencyException)
            {
                return InternalServerError(attendeeDependencyException);
            }
            catch (AttendeeServiceException attendeeServiceException)
            {
                return InternalServerError(attendeeServiceException);
            }
        }

        [HttpGet]
        public ActionResult<IQueryable<Attendee>> GetAllAttendees()
        {
            try
            {
                IQueryable<Attendee> retrievedAttendees =
                    this.attendeeService.RetrieveAllAttendees();

                return Ok(retrievedAttendees);
            }
            catch (AttendeeDependencyException attendeeDependencyException)
            {
                return InternalServerError(attendeeDependencyException);
            }
            catch (AttendeeServiceException attendeeServiceException)
            {
                return InternalServerError(attendeeServiceException);
            }
        }

        [HttpGet("{attendeeId}")]
        public async ValueTask<ActionResult<Attendee>> GetAttendeeByIdAsync(Guid attendeeId)
        {
            try
            {
                Attendee attendee = await this.attendeeService.RetrieveAttendeeByIdAsync(attendeeId);

                return Ok(attendee);
            }
            catch (AttendeeValidationException attendeeValidationException)
                when (attendeeValidationException.InnerException is NotFoundAttendeeException)
            {
                return NotFound(attendeeValidationException.InnerException);
            }
            catch (AttendeeValidationException attendeeValidationException)
            {
                return BadRequest(attendeeValidationException.InnerException);
            }
            catch (AttendeeDependencyException attendeeDependencyException)
            {
                return InternalServerError(attendeeDependencyException);
            }
            catch (AttendeeServiceException attendeeServiceException)
            {
                return InternalServerError(attendeeServiceException);
            }
        }

        [HttpPut]
        public async ValueTask<ActionResult<Attendee>> PutAttendeeAsync(Attendee attendee)
        {
            try
            {
                Attendee modifiedAttendee =
                    await this.attendeeService.ModifyAttendeeAsync(attendee);

                return Ok(modifiedAttendee);
            }
            catch (AttendeeValidationException attendeeValidationException)
                when (attendeeValidationException.InnerException is NotFoundAttendeeException)
            {
                return NotFound(attendeeValidationException.InnerException);
            }
            catch (AttendeeValidationException attendeeValidationException)
            {
                return BadRequest(attendeeValidationException.InnerException);
            }
            catch (AttendeeDependencyValidationException attendeeValidationException)
                when (attendeeValidationException.InnerException is InvalidAttendeeReferenceException)
            {
                return FailedDependency(attendeeValidationException.InnerException);
            }
            catch (AttendeeDependencyValidationException attendeeDependencyValidationException)
               when (attendeeDependencyValidationException.InnerException is AlreadyExistsAttendeeException)
            {
                return Conflict(attendeeDependencyValidationException.InnerException);
            }
            catch (AttendeeDependencyException attendeeDependencyException)
            {
                return InternalServerError(attendeeDependencyException);
            }
            catch (AttendeeServiceException attendeeServiceException)
            {
                return InternalServerError(attendeeServiceException);
            }
        }

        [HttpDelete("{attendeeId}")]
        public async ValueTask<ActionResult<Attendee>> DeleteAttendeeByIdAsync(Guid attendeeId)
        {
            try
            {
                Attendee deletedAttendee =
                    await this.attendeeService.RemoveAttendeeByIdAsync(attendeeId);

                return Ok(deletedAttendee);
            }
            catch (AttendeeValidationException attendeeValidationException)
                when (attendeeValidationException.InnerException is NotFoundAttendeeException)
            {
                return NotFound(attendeeValidationException.InnerException);
            }
            catch (AttendeeValidationException attendeeValidationException)
            {
                return BadRequest(attendeeValidationException.InnerException);
            }
            catch (AttendeeDependencyValidationException attendeeDependencyValidationException)
                when (attendeeDependencyValidationException.InnerException is LockedAttendeeException)
            {
                return Locked(attendeeDependencyValidationException.InnerException);
            }
            catch (AttendeeDependencyValidationException attendeeDependencyValidationException)
            {
                return BadRequest(attendeeDependencyValidationException);
            }
            catch (AttendeeDependencyException attendeeDependencyException)
            {
                return InternalServerError(attendeeDependencyException);
            }
            catch (AttendeeServiceException attendeeServiceException)
            {
                return InternalServerError(attendeeServiceException);
            }
        }
    }
}