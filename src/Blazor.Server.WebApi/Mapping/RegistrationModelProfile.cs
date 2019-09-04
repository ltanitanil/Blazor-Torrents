using System.ComponentModel;
using AutoMapper;
using Blazor.Server.BusinessLayer.Entities;
using Blazor.Server.DataAccessLayer.Entities;
using Blazor.Shared.Models.ViewModels.Account;

namespace Blazor.Server.WebApi.Mapping
{
    public class RegistrationModelProfile : Profile
    {
        public RegistrationModelProfile()
        {
            CreateMap<RegistrationViewModel, RegistrationModel>().ForMember(x => x.Gender,
                        opt => opt.MapFrom((rwm, rm) =>
                        {
                            return rwm.Gender switch
                            {
                                "male" => Gender.Male,
                                "female" => Gender.Female,
                                _ => throw new InvalidEnumArgumentException("\"Gender\" must be \"male\" or \"female\"")
                            };
                        }));
        }
    }
}
