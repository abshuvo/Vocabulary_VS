
using Final.Models;
using Final.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class DictionaryController : Controller
{
    private readonly ApplicationDbContext _context;

    public DictionaryController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var letters = await _context.Words
            .Select(w => w.Letter)
            .Distinct()
            .OrderBy(l => l)
            .ToListAsync();

        return View(letters);
    }

    public async Task<IActionResult> Words(string letter)
    {
        var words = await _context.Words
            .Where(w => w.Letter == letter)
            .OrderBy(w => w.Text)
            .ToListAsync();

        var story = await _context.Stories
            .FirstOrDefaultAsync(s => s.Letter == letter);

        var model = new WordsViewModel
        {
            Letter = letter,
            Words = words,
            Story = story
        };

        return View(model);
    }

    public async Task<IActionResult> WordDetails(int id)
    {
        var word = await _context.Words.FindAsync(id);
        if (word == null)
            return NotFound();

        return View(word);
    }

    public IActionResult Flashcard(string letter, int? index)
    {
        var words = _context.Words
            .Where(w => w.Text.StartsWith(letter))
            .OrderBy(w => w.Text)
            .ToList();

        if (!words.Any())
        {
            return RedirectToAction("Index");
        }

        int currentIndex = index ?? 0;
        if (currentIndex >= words.Count) currentIndex = 0;
        if (currentIndex < 0) currentIndex = words.Count - 1;

        var currentWord = words[currentIndex];

        var viewModel = new FlashcardViewModel
        {
            Id = currentWord.Id,
            Word = currentWord.Text,
            Definition = currentWord.Definition,
            Letter = letter,
            CurrentIndex = currentIndex,
            TotalWords = words.Count
        };

        return View(viewModel);
    }

    [HttpGet]
    public IActionResult GetNextCard(string letter, int currentIndex)
    {
        var words = _context.Words
            .Where(w => w.Text.StartsWith(letter))
            .OrderBy(w => w.Text)
            .ToList();

        int nextIndex = (currentIndex + 1) % words.Count;
        var nextWord = words[nextIndex];

        return Json(new
        {
            id = nextWord.Id,
            word = nextWord.Text,
            definition = nextWord.Definition,
            currentIndex = nextIndex,
            totalWords = words.Count
        });
    }

    [HttpGet]
    public async Task<IActionResult> Exam(string letter)
    {
        var words = await _context.Words
            .Where(w => w.Letter == letter)
            .OrderBy(w => w.Text)
            .ToListAsync();

        return View(words);
    }

    public IActionResult GetWordDefinition(int id)
    {
        var word = _context.Words.FirstOrDefault(w => w.Id == id);

        if (word == null)
        {
            return Content("Definition not found.");
        }

        return Content(word.Definition); // Assuming the 'Definition' column exists in your Words table
    }

    [HttpGet]
    public async Task<IActionResult> MatchingExam(string letter)
    {
        var words = await _context.Words
            .Where(w => w.Letter == letter)
            .OrderBy(w => w.Text)
            .Take(4)
            .ToListAsync();

        return View(words);
    }
}