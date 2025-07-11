using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SkillSync.Models;

namespace SkillSync.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly UserManager<LearnerProfile> userManager;
        private readonly SignInManager<LearnerProfile> signInManager;
        public RegisterModel(UserManager<LearnerProfile> userManager, SignInManager<LearnerProfile> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }
        [BindProperty]
        public InputModel Input { get; set; } = new InputModel();
        public string ReturnUrl { get; set; } = string.Empty;


        public class InputModel
        {
            [Required]
            [Display(Name = "Full Name")]
            public string Name { get; set; }

            [Display(Name = "Profile Info")]
            public string ProfileDetails { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm Password")]
            [Compare("Password", ErrorMessage = "Passwords do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl ?? Url.Content("~/");
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ReturnUrl = returnUrl;
            if (ModelState.IsValid)
            {
                var user = new LearnerProfile
                {
                    UserName = Input.Email,
                    Email = Input.Email,
                    Name = Input.Name,
                    ProfileDetails = Input.ProfileDetails,
                    RegistrationDate = DateTime.Now,
                    LastLogin = DateTime.Now
                };

                var result = await userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: false);
                    TempData["SuccessMessage"] = "Welcome to SkillSync!";
                    return LocalRedirect(returnUrl);
                }

                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
            }

            return Page();
        }

        public void OnGet()
        {

        }
    }
}
