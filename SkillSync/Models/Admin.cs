namespace SkillSync.Models
{
    public class Admin
    {
        public int AdminId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

        public ICollection<LearningPath> LearningPaths { get; set; }
        public ICollection<Skill> Skills { get; set; }

    }

}
