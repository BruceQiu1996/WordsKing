namespace WordsKing.ViewModels
{
    public class BookListItemViewModel
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Introduce { get; set; }
        public int WordNum { get; set; }
        public string Cover { get; set; }
        public IEnumerable<Tag> Tags { get; set; }
    }

    public class Tag 
    {
        public Tag(string tag)
        {
            TagName = tag;
        }
        public string TagName { get; set; }
    }
}
