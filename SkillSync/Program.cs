using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SkillSync.Data;
using SkillSync.Models;

namespace SkillSync
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // ✅ Database connection
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            // ✅ Identity + roles
            builder.Services.AddDefaultIdentity<LearnerProfile>(options =>
            {
                options.SignIn.RequireConfirmedAccount = false;
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.AddRazorPages();

            var app = builder.Build();

            // ✅ Migrate and seed
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var dbContext = services.GetRequiredService<ApplicationDbContext>();
                    dbContext.Database.Migrate();

                    Console.WriteLine("🌱 Seeding roles...");
                    await SeedRolesAsync(services);

                    Console.WriteLine("👤 Seeding admin user...");
                    await SeedAdminUserAsync(services);

                    Console.WriteLine("📊 Seeding dashboard demo data...");
                    SeedDashboardDemoData(dbContext);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"❌ Seeding failed: {ex.Message}");
                }
            }

            // ✅ Request pipeline
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapRazorPages();
            app.Run();
        }

        // 🌐 Role seeder
        public static async Task SeedRolesAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            string[] roles = { "Admin", "User" };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                    Console.WriteLine($"🔧 Role '{role}' created.");
                }
                else
                {
                    Console.WriteLine($"ℹ️ Role '{role}' already exists.");
                }
            }
        }

        // 👤 Admin seeder
        public static async Task SeedAdminUserAsync(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<LearnerProfile>>();
            string adminEmail = "admin@skillsync.com";
            string adminPassword = "Admin@123";

            var adminUser = await userManager.FindByEmailAsync(adminEmail);
            if (adminUser == null)
            {
                var newAdmin = new LearnerProfile
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    Name = "SkillSync Admin",
                    ProfileDetails = "System default administrator",
                    RegistrationDate = DateTime.Now,
                    LastLogin = DateTime.Now
                };

                var result = await userManager.CreateAsync(newAdmin, adminPassword);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(newAdmin, "Admin");
                    Console.WriteLine("✅ Admin account created and assigned to 'Admin' role.");
                }
                else
                {
                    Console.WriteLine("❌ Failed to create admin user:");
                    foreach (var error in result.Errors)
                    {
                        Console.WriteLine($"   • {error.Description}");
                    }
                }
            }
            else
            {
                Console.WriteLine("ℹ️ Admin user already exists.");
            }
        }

        // 📊 Dashboard demo data seeder
        public static void SeedDashboardDemoData(ApplicationDbContext context)
        {
            var learner = context.Learners.FirstOrDefault(l => l.Email == "learner@example.com");
            if (learner == null) return;

            if (!context.ProgressRecords.Any())
            {
                context.ProgressRecords.AddRange(new[]
                {
                    new Progress
                    {
                        SkillId = context.Skills.First().SkillId,
                        LearningListId = context.LearningLists.First().LearningListId,
                        Completion = 80,
                        LastUpdate = DateTime.Now.AddDays(-2)
                    },
                    new Progress
                    {
                        SkillId = context.Skills.Skip(1).First().SkillId,
                        LearningListId = context.LearningLists.First().LearningListId,
                        Completion = 100,
                        LastUpdate = DateTime.Now.AddDays(-1)
                    }
                });
            }

            if (!context.Reminders.Any())
            {
                context.Reminders.AddRange(new[]
                {
                    new Reminder
                    {
                        Message = "Review Entity Framework basics",
                        DueDate = DateTime.Now.AddDays(2),
                        Priority = "High",
                        SkillId = context.Skills.First().SkillId,
                        LearnerId = learner.Id
                    },
                    new Reminder
                    {
                        Message = "Complete the progress dashboard UI",
                        DueDate = DateTime.Now.AddDays(3),
                        Priority = "Medium",
                        LearningPathId = context.LearningPaths.First().LearningPathId,
                        LearnerId = learner.Id
                    }
                });
            }

            context.SaveChanges();
        }
    }
}