using AutoMapper;
using Core.Domain.Model.ConsumerProtection;
using Infrastructure.Automapper.ValueResolver;
using UserInterface.Models;

namespace Infrastructure.Automapper.ConfigurationProfiles
{
    public class SalesmanArticleMapperProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap
                <SalesmanArticleGroupedByMonthAndDescription, SalesmanArticleGroupedByMonthAndDescriptionForm>()
                .ForMember(dest => dest.Month, opt => opt.ResolveUsing(new MonthValueResolver()));
        }
    }
}