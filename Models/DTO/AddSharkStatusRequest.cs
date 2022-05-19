namespace SharkSpotterAPI.Models.DTO
{
    public class AddSharkStatusRequest
    {
        public Guid FlagId { get; set; }
        public Guid BeachId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
