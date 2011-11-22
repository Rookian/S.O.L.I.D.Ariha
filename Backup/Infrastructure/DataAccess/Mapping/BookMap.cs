using Core.Domain.Bases;
using Core.Domain.Model;
using FluentNHibernate.Mapping;

namespace Infrastructure.DataAccess.Mapping
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