using System;

namespace MailService.Domain.Base
{
    public interface IEntityWithGuidId
    {
        Guid Id { get; }
    }
}