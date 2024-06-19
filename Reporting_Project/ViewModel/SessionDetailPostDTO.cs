namespace Frontend_Project.ViewModel
{
    public class SessionDetailPostDTO
    {
        public SessionDetailPostDTO() { 
            StudentsAttendedList = new List<StudentDTO> ();
        }

        public int AuditorId { get; set; }
        public string RoundCode { get; set; }
        public DateTime ReportStart { get; set; }
        public DateTime ReportEnd { get; set; }
        public Boolean Conducted { get; set; }
        public Boolean MaterialDelivered { get; set; }
        public Boolean FilesDelivered { get; set; }
        public List<StudentDTO> StudentsAttendedList { get; set; }
        public int ConnectionQuality { get; set; }
        public int VoiceQuality { get; set; }
        public int VideoQuality { get; set; }
    }
}
