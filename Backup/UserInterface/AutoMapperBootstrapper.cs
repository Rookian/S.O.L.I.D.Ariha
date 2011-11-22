using System.Linq;
using AutoMapper;
using Core.Domain.Model;
using UserInterface.Models;

namespace UserInterface
{
    public class AutoMapperBootsTrapper
    {
        public static void Boot()
        {
            // ReSharper disable PossibleNullReferenceException

            Mapper.CreateMap<Employee, EmployeeForm>()
                   .ForMember(dest => dest.TeamName, opt => opt.MapFrom(x => x.GetTeams().FirstOrDefault() != null ? x.GetTeams().FirstOrDefault().Name : "n/a"));

            Mapper.CreateMap<Employee, EmployeeDropdownList>()
                .ForMember(dest => dest.Name, opt =>
                                                  {
                                                      opt.NullSubstitute("n/a");
                                                      opt.MapFrom(x => string.Format("{0}, {1}", x.FirstName, x.LastName));
                                                  });

            Mapper.CreateMap<Employee, LoanedItemDropdownList>()
                .ForMember(x => x.Id, y => y.MapFrom(x => x.GetLoanedItems().FirstOrDefault() != null ? x.GetLoanedItems().FirstOrDefault().Id : 0))
                .ForMember(x => x.Title, y => y.MapFrom(x => x.GetLoanedItems().FirstOrDefault() != null ? x.GetLoanedItems().FirstOrDefault().Name : null));

            Mapper.CreateMap<LoanedItem, LoanedItemForm>()
                .ForMember(x => x.EmployeeName, y => y.MapFrom(x => x.LoanedBy != null ? string.Format("{0} {1}", x.LoanedBy.FirstName, x.LoanedBy.LastName) : "n/a"))
                ;
                
            // ReSharper restore PossibleNullReferenceException
        }
    }
}