using Microsoft.AspNetCore.Authorization;
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
    public class LearnerProgressboardModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public LearnerProgressboardModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Progress> ProgressRecords { get; set; }
        public IList<Reminder> Reminders { get; set; }

        public async Task OnGetAsync()
        {
            var email = User.Identity?.Name;
            if (string.IsNullOrEmpty(email)) return;

            ProgressRecords = await _context.ProgressRecords
                .Include(p => p.Skill)
                .Include(p => p.LearningList)
                    .ThenInclude(ll => ll.Learner)
                .Where(p => p.LearningList.Learner.Email == email)
                .ToListAsync();

            Reminders = await _context.Reminders
                .Include(r => r.Skill)
                .Include(r => r.LearningPath)
                .Include(r => r.Learner)
                .Where(r => r.Learner.Email == email)
                .ToListAsync();
        }
    }
}