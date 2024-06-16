namespace Frontend_Project.ViewModel
{
    public class AuditingSessionCriteraDTO
    {
        public string TrainingCenterName { get; set; }
        public string TrainingProvider { get; set; }
        public string CourseName { get; set; }
        public List<StudentDTO> students { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string instructor { get; set; }
        public string SessionType { get; set; }
        public int NumberRegistered { get; set; }
    }
}
