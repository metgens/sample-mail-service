namespace MailService.Common.Pagination
{
    public abstract class PagedQuery
    {
        /// <summary>
        /// Current page number - numbered from 1
        /// </summary>
        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 25;
    }
}
