using AutoMapper;
using Frontend_Project.ViewModel;

namespace Frontend_Project.Mapper
{
    public class EducationProfile : Profile
    {
        public EducationProfile()
        {

            CreateMap<SessionDetailDTO, SessionDetailPostDTO>().ReverseMap();
        }
    }
}
