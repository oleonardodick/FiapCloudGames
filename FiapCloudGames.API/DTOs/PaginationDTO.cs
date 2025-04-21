namespace FiapCloudGames.API.DTOs
{
    public class PaginationDTO
    {
        public int PageNumber { get; set; }
        public int TotalPages { get; set; }
        public int TotalItems { get; set; }
        public int PerPage { get; set; }
    }
}
