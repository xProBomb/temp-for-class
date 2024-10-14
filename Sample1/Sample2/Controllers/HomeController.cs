using System.Diagnostics;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Sample2.Models;

namespace Sample2.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult TeamViewer(Teams model)
    {
        if (ModelState.IsValid)
        {

            if (model.TeamSize < 2 || model.TeamSize > 10)
            {
                ModelState.AddModelError("TeamSize", "Team size must be between 2 and 10");
                return View("Index");
            }

            var names = new List<string>();
            if (!string.IsNullOrEmpty(model.Names))
            {
                names = model.Names.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                var nameRegex = new Regex(@"^[a-zA-Z ,\.\-_']+$");
                foreach (var name in names)
                {
                    if (!nameRegex.IsMatch(name.Trim()))
                    {
                        ModelState.AddModelError("Names", "Names can only contain letters, spaces, and the characters ,.-_'");
                        break;
                    }
                }
            }

            var random = new Random();
            names = names.OrderBy(x => random.Next()).ToList();

            var numberOfTeams = (int)Math.Ceiling((double)names.Count / model.TeamSize);
            var teams = new List<Team>();

            for (int i = 0; i < numberOfTeams; i++)
            {
                var teamMembers = names.Skip(i * model.TeamSize).Take(model.TeamSize).ToList();
                teams.Add(new Team
                {
                    TeamName = $"Team {i + 1}",
                    Members = teamMembers
                });
            }

            var teamViewerViewModel = new TeamViewerViewModel
            {
                TeamSize = model.TeamSize,
                NumberOfTeams = numberOfTeams,
                Teams = teams
            };

            return View("TeamViewer", teamViewerViewModel);
        }

        return View("Index");
    }


    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
