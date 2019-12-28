using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        [HttpPost]
        public async Task<IActionResult> CreateMail(CreateMailCmd command)
        {
            await _commandBus.Send(command);
            return Created("", null);
        }

        [HttpPut("{mailId}")]
        public async Task<IActionResult> EditMail([FromRoute]Guid mailId, [FromBody]EditMailCmd command)
        {
            await _commandBus.Send(command);
            return Ok();
        }

        [HttpPost("{mailId}/attachments")]
        public async Task<IActionResult> AddMailAttachment(AddMailAttachmentCmd command)
        {
            await _commandBus.Send(command);
            return Ok();
        }

        [HttpDelete("{mailId}/attachments/{attachmentGuid}")]
        public async Task<IActionResult> RemoveMailAttachment(RemoveMailAttachmentCmd command)
        {
            await _commandBus.Send(command);
            return Ok();
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendAllPendingMails()
        {
            await _commandBus.Send(new SendAllPendingMailsCmd());
            return Accepted();
        }

        [HttpPost("{mailId}/send")]
        public async Task<IActionResult> SendMail([FromRoute]Guid mailId)
        {
            await _commandBus.Send(new SendMailCmd(mailId));
            return Accepted();
        }

        [HttpGet()]
        public async Task<IActionResult> GetAllMails([FromQuery] GetAllMailsQuery query)
        {
            var results = await _queryBus.Send<GetAllMailsQuery, PagedResult<MailDto>>(query);

            return Ok(results);
        }

        [HttpGet("{mailId}")]
        public async Task<IActionResult> GetMail([FromRoute]Guid mailId)
        {
            throw new NotImplementedException();
        }

        [HttpGet("{mailId}/status")]
        public async Task<IActionResult> GetMailStatus([FromRoute]Guid mailId)
        {
            throw new NotImplementedException();
        }

    }
}
