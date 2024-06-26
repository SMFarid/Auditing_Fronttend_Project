using Frontend_Project.ViewModel.Auditing;

namespace Frontend_Project.ViewModel.Assignment
{
    public class EditAuditorRoundCodeAssignmentVM
    {
        public EditAuditorRoundCodeAssignmentVM()
        {
            auditors = new List<AuditorListVM>();
        }
        public string RoundCode { get; set; }
        public int? AssignmentID { get; set; }
        public int? AuditorID { get; set; }
        public DateTime? AssignmentDate { get; set; }
        public int? Conducted { get; set; }
        public List<AuditorListVM> auditors { get; set; }
    }
}
