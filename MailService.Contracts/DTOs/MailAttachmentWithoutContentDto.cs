﻿using System;

namespace MailService.Contracts.DTOs
{
    public class MailAttachmentWithoutContentDto
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string MediaType { get; private set; }
    }
}