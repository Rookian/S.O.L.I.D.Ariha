using Core.Domain.Model;
using FluentNHibernate.Mapping;

namespace Infrastructure.DataAccess.Mapping
{
    public sealed class TeamMap : ClassMap<Team>
    {
        public TeamMap()
        {
            // identity mapping
            Id(p => p.Id)
                .Column("TeamId")
                .GeneratedBy.Identity();

            // column mapping
            Map(p => p.Name);

            // Employee is ressponsible for the relationship
            HasManyToMany(p => p.GetEmployees())
                .Access.CamelCaseField(Prefix.Underscore)
                .Table("TeamEmployee")
                .ParentKeyColumn("TeamId")
                .ChildKeyColumn("EmployeeId")
                .LazyLoad()
                .AsSet()
                .Inverse()
                .Cascade.SaveUpdate();
        }
    }
}