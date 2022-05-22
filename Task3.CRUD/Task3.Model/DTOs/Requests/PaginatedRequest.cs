namespace Task3.Model.DTOs.Requests
{
    public class PaginatedRequest
    {
        public int? PageIndex { get; set; }
        public int? PageSize { get; set; }

        public bool IsValid()
        {
            return PageIndex is not null
                && PageSize is not null
                && PageIndex > 0
                && PageSize > 0;
        }
    }
}
