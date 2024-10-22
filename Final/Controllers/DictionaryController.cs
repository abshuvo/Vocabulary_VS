
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

    [HttpGet]
    public async Task<IActionResult> Flashcard(string letter)
    {
        var words = await _context.Words
            .Where(w => w.Letter == letter)
            .OrderBy(w => w.Text)
            .ToListAsync();

        return View(words);
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