using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SkillSync.Models;
using Microsoft.Extensions.Logging;

namespace SkillSync.Areas.Identity.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly UserManager<LearnerProfile> userManager;
        private readonly SignInManager<LearnerProfile> signInManager;
        private readonly ILogger<RegisterModel> logger;

        public RegisterModel(UserManager<LearnerProfile> userManager, SignInManager<LearnerProfile> signInManager, ILogger<RegisterModel> logger)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.logger = logger;
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
                    await userManager.AddToRoleAsync(user, "User");
                    await signInManager.SignInAsync(user, isPersistent: false);
                    TempData["SuccessMessage"] = "Welcome to SkillSync!";

                    // Debug Logging
                    Console.WriteLine("✅ New user registered: " + Input.Email);
                    logger.LogInformation("User {Email} registered successfully.", Input.Email);

                    return LocalRedirect(returnUrl);
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                    logger.LogWarning("Registration error: {Error}", error.Description);
                }
            }

            return Page();
        }
    }
}