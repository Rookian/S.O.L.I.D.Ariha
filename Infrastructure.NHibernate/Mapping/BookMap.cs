using Core.Domain.Bases.Enumerations;
using Core.Domain.Model;
using FluentNHibernate.Mapping;

namespace Infrastructure.NHibernate.Mapping
{
    public sealed class BookMap : SubclassMap<Book>
    {
        public BookMap()
        {
            // identity mapping
            DiscriminatorValue(DiscriminatorValueLoanedItemEnum.Book);

            // column mapping
            Map(p => p.Author);
            Map(p => p.Isbn);
        }
    }
}