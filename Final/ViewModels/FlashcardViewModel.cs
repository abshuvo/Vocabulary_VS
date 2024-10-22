namespace Final.ViewModels
{
    public class FlashcardViewModel
    {
        public int Id { get; set; }
        public string Word { get; set; }
        public string Definition { get; set; }
        public string Letter { get; set; }
        public int CurrentIndex { get; set; }
        public int TotalWords { get; set; }
    }
}
