using AutoMapper;
using MailService.Common.Pagination;
using MailService.Contracts.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace MailService.Domain.MappingProfiles
{
    public class MailMappingProfile : Profile
    {
        public MailMappingProfile()
        {
            CreateMap<PagedResult<Mail>, PagedResult<MailDto>>();
            CreateMap<Mail, MailDto>();
            CreateMap<MailAttachment, MailAttachmentWithoutContentDto>();
        }
    }
}
