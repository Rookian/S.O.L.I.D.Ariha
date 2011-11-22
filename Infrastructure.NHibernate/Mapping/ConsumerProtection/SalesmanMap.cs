using Core.Domain.Model.ConsumerProtection;
using FluentNHibernate.Mapping;

namespace Infrastructure.NHibernate.Mapping.ConsumerProtection
{
    public class SalesmanMap : ClassMap<Salesman>
    {
        public SalesmanMap()
        {
            Id(x => x.Id)
                .Column("SalesmanId")
                .GeneratedBy.Identity();

            Map(x => x.Name);
            Map(x => x.Place);
        }
    }
}