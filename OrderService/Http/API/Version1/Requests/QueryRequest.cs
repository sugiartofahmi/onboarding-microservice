using Microsoft.AspNetCore.Mvc;

namespace DotNetService.Http.API.Version1
{
    public enum SortOrderEnum
    {
        Asc,
        Desc
    }

    public class Query
    {
        public Query()
        {
            Order = SortOrderEnum.Desc;
            Page = 1;
            PerPage = 10;
        }

        [FromQuery(Name = "search")]
        public string Search { get; set; }

        [FromQuery(Name = "pagination")]
        public bool Pagination { get; set; }

        [FromQuery(Name = "per_page")]
        public int PerPage { get; set; }

        [FromQuery(Name = "page")]
        public int Page { get; set; }

        [FromQuery(Name = "sort_by")]
        public string SortBy { get; set; }

        [FromQuery(Name = "order")]
        public SortOrderEnum Order { get; set; }
    }
}
