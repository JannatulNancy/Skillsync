using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using SkillSync.Data;
using SkillSync.Models;

namespace SkillSync.Pages.Skills
{
    public class CreateModel : PageModel
    {
        private readonly SkillSync.Data.ApplicationDbContext _context;

        public CreateModel(SkillSync.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["AdminId"] = new SelectList(_context.Admins, "AdminId", "AdminId");
            return Page();
        }

        [BindProperty]
        public Skill Skill { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Skills.Add(Skill);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
