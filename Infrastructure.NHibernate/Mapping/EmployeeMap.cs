using Core.Domain.Model;
using FluentNHibernate.Mapping;

namespace Infrastructure.NHibernate.Mapping
{
    public sealed class EmployeeMap : ClassMap<Employee>
    {
        public EmployeeMap()
        {
            Table("tblEmployee");
            Id(p => p.Id)
                .Column("EmployeeId")
                .GeneratedBy.Identity();

            Map(p => p.EMail);
            Map(p => p.LastName);
            Map(p => p.FirstName);
            Map(p => p.Infos).Not.Nullable();
            Map(p => p.Count);
            Map(p => p.IsCool).Not.Nullable();
            //HasManyToMany(p => p.GetTeams())
            //    .Access.CamelCaseField(Prefix.Underscore)
            //    .Table("TeamEmployee")
            //    .ParentKeyColumn("EmployeeId")
            //    .ChildKeyColumn("TeamId")
            //    .LazyLoad()
            //    .AsSet()
            //    .Cascade.SaveUpdate();
            
            HasMany(p => p.GetLoanedItems())
                .Access.CamelCaseField(Prefix.Underscore)
                .Cascade.SaveUpdate()
                .KeyColumn("EmployeeId");
        }
    }
}