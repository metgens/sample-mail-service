using MailService.Common.Pagination;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MailService.Data.Repositories.Base
{
    public static class PagedRepositoryExtensions
    {
        public static async Task<PagedResult<T>> GetPagedAsync<T>(this IQueryable<T> query,
                                            PagedQuery pagedQuery) where T : class
        {
            var result = new PagedResult<T>();
            result.CurrentPage = pagedQuery.CurrentPage;
            result.PageSize = pagedQuery.PageSize;
            result.RowCount = await query.CountAsync();

            var pageCount = (double)result.RowCount / pagedQuery.PageSize;
            result.PageCount = (int)Math.Ceiling(pageCount);

            var skip = (pagedQuery.CurrentPage - 1) * pagedQuery.PageSize;
            result.Results = await query.Skip(skip)
                                  .Take(pagedQuery.PageSize)
                                  .ToListAsync();

            return result;
        }
    }
}
