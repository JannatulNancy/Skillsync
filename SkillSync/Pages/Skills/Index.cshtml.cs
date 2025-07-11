using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SkillSync.Data;
using SkillSync.Models;

namespace SkillSync.Pages.Skills
{
    public class IndexModel : PageModel
    {
        private readonly SkillSync.Data.ApplicationDbContext _context;

        public IndexModel(SkillSync.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Skill> Skill { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Skill = await _context.Skills
                .Include(s => s.Admin).ToListAsync();
        }
    }
}
