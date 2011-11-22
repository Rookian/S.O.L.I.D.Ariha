using FluentNHibernate.Mapping;
using Core.Domain.Model;

namespace Infrastructure.DataAccess.Mapping
{
    public sealed class EmployeeMap : ClassMap<Employee>
    {
        public EmployeeMap()
        {
            // identifier mapping
            Id(p => p.Id)
                .Column("EmployeeId")
                .GeneratedBy.Identity();

            // column mapping
            Map(p => p.EMail);
            Map(p => p.LastName);
            Map(p => p.FirstName);

            // Employee is responsible for the relationship
            HasManyToMany(p => p.GetTeams())
                .Access.CamelCaseField(Prefix.Underscore)
                .Table("TeamEmployee")
                .ParentKeyColumn("EmployeeId")
                .ChildKeyColumn("TeamId")
                .LazyLoad()
                .AsSet()
                .Cascade.SaveUpdate();

            // No inverse attribute => Employee is acting as an aggregate root (parent) 
            // and LoanedItems is the child, see http://blog.lowendahl.net/?p=88
            HasMany(p => p.GetLoanedItems())
                .Access.CamelCaseField(Prefix.Underscore)
                .Cascade.SaveUpdate()
                .KeyColumn("EmployeeId");
        }
    }
}