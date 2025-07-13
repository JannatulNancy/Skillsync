using Microsoft.AspNetCore.Identity;
using SkillSync.Models;
using System;
using System.Collections.Generic;

namespace SkillSync.Models
{
    public class LearnerProfile : IdentityUser
    {
        public string Name { get; set; }
        public string ProfileDetails { get; set; }

        public DateTime RegistrationDate { get; set; }
        public DateTime LastLogin { get; set; }

        public ICollection<LearningList> LearningLists { get; set; }
        public ICollection<Recommendation> Recommendations { get; set; }
        public ICollection<Reminder> Reminders { get; set; }
        public ICollection<Report> Reports { get; set; }
        public ICollection<LearningPath> LearningPaths { get; set; }
    }
}