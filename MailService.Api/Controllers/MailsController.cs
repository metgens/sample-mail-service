using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailService.Api.Exceptions;
using MailService.Common.Bus.Command;
using MailService.Common.Bus.Query;
using MailService.Common.Pagination;
using MailService.Contracts.Commands;
using MailService.Contracts.DTOs;
using MailService.Contracts.Queries;
using Microsoft.AspNetCore.Mvc;

namespace MailService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailsController : ControllerBase
    {
        private readonly ICommandBus _commandBus;
        private readonly IQueryBus _queryBus;

        public MailsController(ICommandBus commandBus, IQueryBus queryBus)
        {
            _commandBus = commandBus;
            _queryBus = queryBus;
        }
        
        /// <summary>
        /// Create a new mail, without sending it.
        /// </summary>
        /// <response code="400">Bad request</response>     
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public async Task<IActionResult> CreateMail(CreateMailCmd command)
        {
            await _commandBus.Send(command);
            return Created("", null);
        }

        /// <summary>
        /// Edit exisiting, not sent mail.
        /// </summary>
        /// <response code="400">Bad request</response>     
        /// <response code="404">Mail with provided Id does not exist</response>     
        /// <response code="409">Sent mail edition not allowed</response>     
        [HttpPut("{mailId}")]
        [ProducesResponseType(201)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [ProducesResponseType(typeof(ErrorResponse), 404)]
        [ProducesResponseType(typeof(ErrorResponse), 409)]
        public async Task<IActionResult> EditMail([FromRoute]Guid mailId, [FromBody]EditMailCmd command)
        {
            command.SetMailId(mailId);
            await _commandBus.Send(command);
            return Ok();
        }

        /// <summary>
        /// Add single attachment to exisiting, not sent mail.
        /// </summary>
        /// <response code="400">Bad request</response>     
        /// <response code="404">Mail with provided Id does not exist.</response>     
        /// <response code="409">Sent mail edition not allowed</response>  
        [HttpPost("{mailId}/attachments")]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [ProducesResponseType(typeof(ErrorResponse), 404)]
        [ProducesResponseType(typeof(ErrorResponse), 409)]
        public async Task<IActionResult> AddMailAttachment([FromRoute]Guid mailId, AddMailAttachmentCmd command)
        {
            command.SetMailId(mailId);
            await _commandBus.Send(command);
            return Ok();
        }

        /// <summary>
        /// Remove single attachment from exisiting, not sent mail.
        /// </summary>
        /// <response code="400">Bad request</response>     
        /// <response code="404">Mail with provided Id does not exist.</response>     
        /// <response code="409">Sent mail edition not allowed</response>  
        [HttpDelete("{mailId}/attachments/{attachmentId}")]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [ProducesResponseType(typeof(ErrorResponse), 404)]
        [ProducesResponseType(typeof(ErrorResponse), 409)]
        public async Task<IActionResult> RemoveMailAttachment([FromRoute]Guid mailId, [FromRoute]Guid attachmentId)
        {
            await _commandBus.Send(new RemoveMailAttachmentCmd(mailId, attachmentId));
            return Ok();
        }

        /// <summary>
        /// Send pending mails. Maximum number of mails to send is provided in request parameter.
        /// </summary>
        /// <response code="500">Mail sender error</response> 
        [HttpPost("send")]
        [ProducesResponseType(202)]
        [ProducesResponseType(typeof(ErrorResponse), 404)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> SendPendingMails([FromQuery]int maxNumberToSend = 500)
        {
            await _commandBus.Send(new SendPendingMailsCmd(maxNumberToSend));
            return Accepted();
        }

        /// <summary>
        /// Send the specified not sent mail or resend sent one.
        /// </summary>
        /// <response code="500">Mail sender error</response>  
        /// <response code="404">Mail with provided Id does not exist.</response>   
        [HttpPost("{mailId}/send")]
        [ProducesResponseType(202)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> SendMail([FromRoute]Guid mailId)
        {
            await _commandBus.Send(new SendMailCmd(mailId));
            return Accepted();
        }

        /// <summary>
        /// Get all mails paginated
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet()]
        [ProducesResponseType(typeof(List<MailDto>), 200)]
        public async Task<IActionResult> GetAllMails([FromQuery] GetAllMailsQuery query)
        {
            var results = await _queryBus.Send<GetAllMailsQuery, PagedResult<MailDto>>(query);

            return Ok(results);
        }

        /// <summary>
        /// Get the specified mail
        /// </summary>
        /// <param name="mailId"></param>
        /// <returns></returns>
        [HttpGet("{mailId}")]
        [ProducesResponseType(typeof(MailDto), 200)]
        public async Task<IActionResult> GetMail([FromRoute]Guid mailId)
        {
            var results = await _queryBus.Send<GetMailQuery, MailDto>(new GetMailQuery(mailId));
            return Ok(results);
        }

        /// <summary>
        /// Get the specified mail status (pending, sent, draft)
        /// </summary>
        /// <returns></returns>
        [HttpGet("{mailId}/status")]
        public async Task<IActionResult> GetMailStatus([FromRoute]Guid mailId)
        {
            var results = await _queryBus.Send<GetMailStatusQuery, string>(new GetMailStatusQuery(mailId));
            return Ok(results);
        }

    }
}
