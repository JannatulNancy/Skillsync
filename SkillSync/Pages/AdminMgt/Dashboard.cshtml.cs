using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SkillSync.Pages.AdminMgt
{
    [Authorize(Roles = "Admin")]
    public class DashboardModel : PageModel
    {
        public List<string> Notifications { get; set; } = new();

        public void OnGet()
        {
            // Simulated alerts — later you can fetch real data
            Notifications.Add("⚠️ 3 new resources have been flagged for review.");
            Notifications.Add("👥 2 new learners joined today.");
            Notifications.Add("📈 Skill 'Database Optimization' is trending — used in 5 new learning paths.");
        }
    }
}