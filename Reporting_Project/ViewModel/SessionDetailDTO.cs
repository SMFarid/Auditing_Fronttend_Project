using Frontend_Project.ViewModel.Auditing;

namespace Frontend_Project.ViewModel
{
    public class SessionDetailDTO
    {
        public List<StudentDTO> StudentsAttendedList { get; set; } = new List<StudentDTO>(); 
        public List<InstructorDTO> InstructorList { get; set; } = new List<InstructorDTO>(); 
        public List<AuditorGroupsDTO> sessions { get; set; } = new List<AuditorGroupsDTO>();
        // محتاجه اعرف هستقبله من الباك وبعدين ابعته ولا هيتبعت من الفونت عل طول 
        public int auditing_Session_ID { get; set; }
        public int study_Group_ID { get; set; }
        public string TrainingProvider { get; set; }
        public int AuditorId { get; set; }
        public int Instructor_ID { get; set; }
        public string RoundCode { get; set; }
        public string sessionName { get; set; }
        public DateTime reportStart { get; set; }
        public Boolean Conducted { get; set; }
        public Boolean lab_Flag { get; set; }
        public Boolean test_Flag { get; set; }
        public string current_Chapter { get; set; }
        public Boolean MaterialDelivered { get; set; }
        public Boolean depi_Logo_Flag { get; set; }
       // public Boolean FilesDelivered { get; set; }
        public string ConnectionQuality { get; set; }
        public string VoiceQuality { get; set; }
        public string VideoQuality { get; set; }
        public int NumberRegistered { get; set; }
        public string TrainingCenterName { get; set; }


    }
}
