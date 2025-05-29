namespace Domain.Entities
{
    public class OrderLogEntity
    {
        public int Id { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;

        public string ResponseData { get; set; } = string.Empty;
        public DateTime LogDate { get; set; } = DateTime.UtcNow;
    }
}
