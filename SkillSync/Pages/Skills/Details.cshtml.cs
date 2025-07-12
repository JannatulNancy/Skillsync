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
    public class DetailsModel : PageModel
    {
        private readonly SkillSync.Data.ApplicationDbContext _context;

        public DetailsModel(SkillSync.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Skill Skill { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var skill = await _context.Skills.FirstOrDefaultAsync(m => m.SkillId == id);

            if (skill is not null)
            {
                Skill = skill;

                return Page();
            }

            return NotFound();
        }
    }
}
