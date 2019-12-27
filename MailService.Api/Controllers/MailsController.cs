using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailService.Contracts.Commands;
using MailService.Contracts.Queries.Base;
using Microsoft.AspNetCore.Mvc;

namespace MailService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MailsController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> CreateMail(CreateMailCmd command)
        {
            throw new NotImplementedException();
        }

        [HttpPut("{mailId}")]
        public async Task<IActionResult> EditMail([FromRoute]Guid mailId, [FromBody]EditMailCmd editMailCmd)
        {
            throw new NotImplementedException();
        }

        [HttpPost("{mailId}/attachments")]
        public async Task<IActionResult> AddMailAttachment(AddMailAttachmentCmd command)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{mailId}/attachments/{attachmentGuid}")]
        public async Task<IActionResult> RemoveMailAttachment(RemoveMailAttachmentCmd command)
        {
            throw new NotImplementedException();
        }

        [HttpPost("/send")]
        public async Task<IActionResult> SendAllPendingMails()
        {
            throw new NotImplementedException();
        }

        [HttpPost("{mailId}/send")]
        public async Task<IActionResult> SendMail([FromRoute]Guid mailId)
        {
            throw new NotImplementedException();
        }

        [HttpGet()]
        public async Task<IActionResult> GetAllMails([FromQuery] PagedQuery query)
        {
            throw new NotImplementedException();
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
