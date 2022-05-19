namespace SharkSpotterAPI.Models.DTO
{
    public class SharkStatusDTO
    {
        public Guid Id { get; set; }
        public Guid FlagId { get; set; }
        public Guid BeachId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public BeachDTO Beach { get; set; }
        public FlagDTO Flag { get; set; }
    }
}
