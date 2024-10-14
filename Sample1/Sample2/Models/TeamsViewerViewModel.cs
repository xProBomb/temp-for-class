using System.ComponentModel.DataAnnotations;

namespace Sample2.Models
{
    public class TeamViewerViewModel
    {
        public int TeamSize { get; set; }
        public int NumberOfTeams { get; set; }

        public List<Team>? Teams { get; set; }
    }

    public class Team
    {
        public string? TeamName { get; set; }

        public List<string>? Members { get; set; }
    }

}