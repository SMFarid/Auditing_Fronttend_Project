using Frontend_Project.ViewModel.Auditing;

namespace Frontend_Project.ViewModel
{
    public class AuditingSessionCriteraDTO
    {
        public AuditingSessionCriteraDTO()
        {
            students = new List<StudentDTO>();
            Instructors = new List<InstructorDTO>();
        }
        public int Auditing_Session_ID { get; set; }
        public int Study_Group_ID { get; set; }
        public string TrainingCenterName { get; set; }
        public string TrainingProvider { get; set; }
        public string CourseName { get; set; }
        public List<StudentDTO> students { get; set; }

        public List<InstructorDTO> Instructors { get; set; }
        public string OtherInstructorName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string instructor { get; set; }
        public string SessionType { get; set; }
        public int NumberRegistered { get; set; }
    }
}
