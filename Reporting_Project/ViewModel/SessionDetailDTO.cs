using Frontend_Project.ViewModel.Auditing;

namespace Frontend_Project.ViewModel
{
    public class SessionDetailDTO
    {
        public List<StudentDTO> StudentsAttendedList { get; set; } = new List<StudentDTO>(); 
        public List<InstructorDTO> InstructorList { get; set; } = new List<InstructorDTO>(); 
        public string OtherInstructorName { get; set; }
        public List<AuditorGroupsDTO> sessions { get; set; } = new List<AuditorGroupsDTO>();
        // محتاجه اعرف هستقبله من الباك وبعدين ابعته ولا هيتبعت من الفونت عل طول 
        public int? auditing_Session_ID { get; set; }
        public int? study_Group_ID { get; set; }
        public string? TrainingProvider { get; set; }
        public string ?Track {  get; set; }
        public int? AuditorId { get; set; }
        public int? Instructor_ID { get; set; }
        public string? RoundCode { get; set; }
        public string? sessionName { get; set; }
        public DateTime? reportStart { get; set; }
        public Boolean Conducted { get; set; } = false;
        public Boolean lab_Flag { get; set; } = false;
        public Boolean test_Flag { get; set; } = false;
        public string current_Chapter { get; set; }
        public Boolean MaterialDelivered { get; set; } = false;
        public Boolean depi_Logo_Flag { get; set; } = false;
        // public Boolean FilesDelivered { get; set; }
        public string ConnectionQuality { get; set; }
        public string VoiceQuality { get; set; }
        public string VideoQuality { get; set; }

        //new
        public string HardwareProficiency { get; set; }
        public bool UnderstoodExamples { get; set; }
        public bool UnderstoonExplaination { get; set; }
        public bool TimeForQuestions { get; set; }
        public bool InstructorEncouragement { get; set; }
        public bool MaterialIsClear { get; set; }
        public string ACCondition { get; set; }
        public bool CenterEnvironment { get; set; }
        public bool InitiativeClear { get; set; }
        public bool PrevLinks { get; set; }

        public int NumberRegistered { get; set; }
        public string TrainingCenterName { get; set; }
        public string MeetingLink { get; set; }

    }
}
