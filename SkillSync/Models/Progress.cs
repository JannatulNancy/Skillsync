namespace SkillSync.Models
{
    public class Progress
    {
        public int ProgressId { get; set; }
        public int LearningListId { get; set; }
        public int SkillId { get; set; }
        public int Completion { get; set; }
        public DateTime LastUpdate { get; set; }

        public LearningList LearningList { get; set; }
        public Skill Skill { get; set; }
    }
}
