namespace SkillSync.Models
{
    public class Reminder
    {
        public int ReminderId { get; set; }
        public string LearnerId { get; set; }
        public string Message { get; set; }
        public DateTime DateSent { get; set; }

        public LearnerProfile Learner { get; set; }
    }
}
