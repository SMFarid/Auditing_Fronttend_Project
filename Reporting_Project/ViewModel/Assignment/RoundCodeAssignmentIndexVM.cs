using Frontend_Project.Common;
using Frontend_Project.Common.Enums;

namespace Frontend_Project.ViewModel.Assignment
{
    public class RoundCodeAssignmentIndexVM
    {
        public RoundCodeAssignmentIndexVM()
        {
            roundCodeAssignmentsVM = new List<AuditorRoundCodeAssignmentVM>();
            EditRoundCodeAssignmentVM = new EditAuditorRoundCodeAssignmentVM();
            statuses = (from RoundCodeAssignmentStates s in Enum.GetValues(typeof(RoundCodeAssignmentStates))
                       select new RoundCodeStatus { code = (int)s, desc = s.ToString()}).ToList();
            // { code = s, Name = s.ToString() };
        }
        public List<AuditorRoundCodeAssignmentVM> roundCodeAssignmentsVM { get; set; }
        public EditAuditorRoundCodeAssignmentVM EditRoundCodeAssignmentVM { get; set; }

        public List<RoundCodeStatus> statuses { get; set; } 
    }
}
