namespace SkillSync.Models
{
    public class Report
    {
        public int ReportId { get; set; }
        public string LearnerId { get; set; }
        public string ReportData { get; set; }
        public string Details { get; set; }
        public bool GeneratedBySystem { get; set; }

        public LearnerProfile Learner { get; set; }
    }
}
