using Final.Models;

namespace Final.ViewModels
{
    public class WordsViewModel
    {
        public string Letter { get; set; }
        public List<Word> Words { get; set; }
        public Story Story { get; set; }
    }
}
