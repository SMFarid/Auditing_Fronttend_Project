using AutoMapper;
using Frontend_Project.ViewModel;
using Frontend_Project.ViewModel.Assignment;

namespace Frontend_Project.Mapper
{
    public class EducationProfile : Profile
    {
        public EducationProfile()
        {

            CreateMap<SessionDetailDTO, SessionDetailPostDTO>().ReverseMap(); 
            CreateMap<AuditorRoundCodeAssignmentVM, EditAuditorRoundCodeAssignmentVM>().ReverseMap(); 
        }
    }
}
