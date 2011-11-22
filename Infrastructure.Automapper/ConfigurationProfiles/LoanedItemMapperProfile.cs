using AutoMapper;
using Core.Domain.Model;
using UserInterface.Models;

namespace Infrastructure.Automapper.ConfigurationProfiles
{
    public class LoanedItemMapperProfile : Profile
    {

        protected override void Configure()
        {
            Mapper.CreateMap<LoanedItem, LoanedItemForm>()
                    .ForMember(x => x.EmployeeName, y => y.MapFrom(x => x.LoanedBy != null ? string.Format("{0} {1}", x.LoanedBy.FirstName, x.LoanedBy.LastName) : "n/a"));
        }

    }
}