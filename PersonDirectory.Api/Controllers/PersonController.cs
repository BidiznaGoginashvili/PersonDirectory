using MediatR;
using Microsoft.AspNetCore.Mvc;
using PersonDirectory.Api.Infrastructure;
using PersonDirectory.Application.Queries.GetPerson;
using PersonDirectory.Application.Queries.GetReport;
using PersonDirectory.Application.Queries.GetPersons;
using PersonDirectory.Application.Commands.UploadPhoto;
using PersonDirectory.Application.Commands.ChangePerson;
using PersonDirectory.Application.Commands.CreatePerson;
using PersonDirectory.Application.Commands.DeletePerson;
using PersonDirectory.Application.Commands.CreateRelatedPerson;
using PersonDirectory.Application.Commands.DeleteRelatedPerson;

namespace PersonDirectory.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : BaseApiController
    {
        private readonly IMediator _mediator;
        public PersonController(IMediator mediator) : base(mediator) { }

        /// <summary>
        /// Retrieves detailed information about a persons by filtering.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns detailed information about persons if found.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPersonsAsync([FromQuery] GetPersonsQuery query, CancellationToken cancellationToken) =>
             await RunQueryAsync(query, cancellationToken);

        /// <summary>
        /// Retrieves detailed information about a person by their ID.
        /// </summary>
        /// <param name="id">The ID of the person to retrieve information for.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns detailed information about the specified person if found, or a 404 NotFound response if the person is not found.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPersonAsync(int id, CancellationToken cancellationToken)
        {
            var query = new GetPersonQuery();
            query.Id = id;

            return await RunQueryAsync(query, cancellationToken);
        }

        /// <summary>
        /// Deletes a person by their ID.
        /// </summary>
        /// <param name="id">The ID of the person to delete.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns a 200 OK response if the person is successfully deleted, a 404 NotFound response if the person is not found, or a 204 NoContent response if no content is returned.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeletePersonAsync(int id, CancellationToken cancellationToken)
        {
            var command = new DeletePersonCommand() { Id = id };

            return await RunCommandAsync(command, cancellationToken);
        }

        /// <summary>
        /// Creates a new person.
        /// </summary>
        /// <param name="command">The command containing information to create the person.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns a 201 Created response if the person is successfully created, or a 400 BadRequest response if the request is invalid.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreatePersonAsync([FromBody] CreatePersonCommand command, CancellationToken cancellationToken)
        {
            return await RunCommandAsync(command, cancellationToken);
        }

        /// <summary>
        /// Updates an existing person.
        /// </summary>
        /// <param name="id">The ID of the person to update.</param>
        /// <param name="command">The command containing information to update the person.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns a 200 OK response if the person is successfully updated, a 404 NotFound response if the person is not found, or a 204 NoContent response if no content is returned.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> ChangePersonAsync(int id, [FromBody] ChangePersonCommand command, CancellationToken cancellationToken)
        {
            command.Id = id; ;

            return Ok(await RunCommandAsync(command, cancellationToken));
        }

        /// <summary>
        /// Retrieves a report containing related persons' count for a specific person.
        /// </summary>
        /// <param name="id">The ID of the person to retrieve the report for.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns a report containing related persons' count for the specified person if found, or a 404 NotFound response if the person is not found.</returns>
        [HttpGet("{id}/report")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetReportAsync(int id, CancellationToken cancellationToken)
        {
            var query = new GetReportQuery();
            query.PersonId = id;

            return await RunQueryAsync(query, cancellationToken);
        }

        /// <summary>
        /// Uploads a photo for a specific person.
        /// </summary>
        /// <param name="id">The ID of the person to upload the photo for.</param>
        /// <param name="command">The command containing the photo to upload.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns a 200 OK response if the photo is successfully uploaded, a 204 NoContent response if no content is returned, or a 400 BadRequest response if the request is invalid.</returns>
        [HttpPatch("{id}/photo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UploadPhotoAsync(int id, [FromForm] UploadPhotoCommand command, CancellationToken cancellationToken)
        {
            command.Id = id;

            return await RunCommandAsync(command, cancellationToken);
        }

        /// <summary>
        /// Creates a relationship between two persons.
        /// </summary>
        /// <param name="id">The ID of the person to create the relationship for.</param>
        /// <param name="command">The command containing information to create the relationship.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns a 201 Created response if the relationship is successfully created, or a 400 BadRequest response if the request is invalid.</returns>
        [HttpPost("{id}/relationship")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateRelationshipAsync(int id, [FromBody] CreateRelatedPersonCommand command, CancellationToken cancellationToken)
        {
            command.PersonId = id;

            return await RunCommandAsync(command, cancellationToken);
        }

        /// <summary>
        /// Deletes a relationship between two persons.
        /// </summary>
        /// <param name="id">The ID of the person to delete the relationship for.</param>
        /// <param name="command">The command containing information to delete the relationship.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns a 200 OK response if the relationship is successfully deleted, a 404 NotFound response if the person or relationship is not found, or a 204 NoContent response if no content is returned.</returns>
        [HttpDelete("{id}/relationship")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteRelationshipAsync(int id, [FromBody] DeleteRelatedPersonCommand command, CancellationToken cancellationToken)
        {
            command.PersonId = id;

            return await RunCommandAsync(command, cancellationToken);
        }
    }
}
