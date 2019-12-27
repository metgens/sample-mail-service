using System;
using System.Collections.Generic;
using System.Text;

namespace MailService.Contracts.Queries.Base
{
    public abstract class PagedQuery
    {
        /// <summary>
        /// Current page number - numbered from 1
        /// </summary>
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
    }
}
