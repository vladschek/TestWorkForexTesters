namespace Common.DTOs
{
    public class ReadSubscriptionDTO
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
