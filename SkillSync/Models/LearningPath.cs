namespace SkillSync.Models
{
    public class LearningPath
    {
        public int LearningPathId { get; set; }
        public string Title { get; set; }
        public string CreatedBy { get; set; }
        public string Description { get; set; }
        public string? LearnerId { get; set; }
        public LearnerProfile Learner { get; set; }

        public int? AdminId { get; set; }
        public Admin Admin { get; set; }
    }

}
