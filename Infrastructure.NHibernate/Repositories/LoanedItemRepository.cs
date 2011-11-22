using System.Collections.Generic;
using Core.Domain.Bases.Repositories;
using Core.Domain.Model;

namespace Infrastructure.NHibernate.Repositories
{
    public class LoanedItemRepository : Repository<LoanedItem>, ILoanedItemRepository
    {
        public IList<LoanedItem> GetAllView()
        {
            return GetSession()
                .QueryOver<LoanedItem>()
                .Fetch(x => x.LoanedBy).Eager
                .List<LoanedItem>();
        }
    }
}