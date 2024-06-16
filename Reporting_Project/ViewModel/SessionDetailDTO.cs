namespace Frontend_Project.ViewModel
{
    public class SessionDetailDTO
    {
        public int AuditorId { get; set; }
        public List<AuditorGroupsDTO> sessions { get; set; } = new List<AuditorGroupsDTO>();
        public List<StudentDTO> StudentsAttendedList { get; set; } = new List<StudentDTO>();
   
        public string RoundCode { get; set; }
        public string sessionName { get; set; }
        public DateTime reportStart { get; set; }
        public Boolean Conducted { get; set; }
        public Boolean MaterialDelivered { get; set; }
        public Boolean FilesDelivered { get; set; }
        public int ConnectionQuality { get; set; }
        public int VoiceQuality { get; set; }
        public int VideoQuality { get; set; }
        public Boolean Lab {  get; set; }
        public Boolean Test {  get; set; }
        public string Current_Chapter { get; set; }


    }
}
