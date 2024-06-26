namespace Frontend_Project.ViewModel.Assignment
{
    public class RoundCodeAssignmentIndexVM
    {
        public RoundCodeAssignmentIndexVM()
        {
            roundCodeAssignmentsVM = new List<AuditorRoundCodeAssignmentVM>();
            EditRoundCodeAssignmentVM = new EditAuditorRoundCodeAssignmentVM();
        }
        public List<AuditorRoundCodeAssignmentVM> roundCodeAssignmentsVM { get; set; }
        public EditAuditorRoundCodeAssignmentVM EditRoundCodeAssignmentVM { get; set; }
    }
}
