using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SkillSync.Data;
using SkillSync.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SkillSync.Pages.LearnerProgress
{
    [Authorize]
    public class ReminderModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ReminderModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Reminder> Reminders { get; set; }

        public async Task<IActionResult> OnPostDismissAsync(int id)
        {
            var reminder = await _context.Reminders.FindAsync(id);

            if (reminder != null)
            {
                _context.Reminders.Remove(reminder);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage(); // Refresh the page
        }
        public async Task OnGetAsync()
        {
            var learnerEmail = User.Identity.Name;

            Reminders = await _context.Reminders
                .Include(r => r.Learner)
                .Where(r => r.Learner.Email == learnerEmail)
                .OrderBy(r => r.DueDate)
                .ToListAsync();


        }
    }
}