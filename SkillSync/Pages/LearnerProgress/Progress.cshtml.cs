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
    public class ProgressModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ProgressModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Progress> ProgressRecords { get; set; }

        public async Task OnGetAsync()
        {
            var userEmail = User.Identity.Name;

            ProgressRecords = await _context.ProgressRecords
                .Include(p => p.Skill)
                .Include(p => p.LearningList)
                .ThenInclude(ll => ll.Learner)
                .Where(p => p.LearningList.Learner.Email == userEmail)
                .ToListAsync();
        }
    }
}