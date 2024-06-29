namespace Frontend_Project.ViewModel
{
    public class SessionDetailPostDTO
    {
        public int auditing_Session_ID { get; set; }
        public int study_Group_ID { get; set; }
        public int AuditorId { get; set; }
        public int Instructor_ID { get; set; }
        public string OtherInstructorName { get; set; }
        public string RoundCode { get; set; }
        public DateTime ReportStart { get; set; }
        public DateTime ReportEnd { get; set; }
        public Boolean Conducted { get; set; }
        public Boolean lab_Flag { get; set; }
        public Boolean test_Flag { get; set; }
        public string Current_Chapter { get; set; }
        public Boolean materialDelivered { get; set; }
        public Boolean depi_Logo_Flag { get; set; } = true;
        
        // public Boolean FilesDelivered { get; set; }
        public List<StudentDTO> StudentsAttendedList { get; set; }
        public string ConnectionQuality { get; set; }
        public string VoiceQuality { get; set; }
        public string VideoQuality { get; set; }
    }
}
