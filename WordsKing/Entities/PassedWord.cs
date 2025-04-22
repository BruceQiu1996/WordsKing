namespace WordsKing.Entities
{
    public class PassedWord
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string UserId { get; set; }
        public string BookId { get; set; }
        public string WordID { get; set; }
        public DateTime Time { get; set; }
    }
}
