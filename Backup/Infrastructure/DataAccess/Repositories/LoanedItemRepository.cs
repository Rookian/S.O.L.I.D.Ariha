using System.Collections.Generic;
using Core.Domain.Bases.Repositories;
using Core.Domain.Model;
using Core.ReflectorExtension;
using NHibernate;

namespace Infrastructure.DataAccess.Repositories
{
    public class LoanedItemRepository : Repository<LoanedItem>, ILoanedItemRepository
    {
        public IList<LoanedItem> GetAllView()
        {
            return GetSession()
                   .CreateCriteria(typeof(LoanedItem))
                   .SetFetchMode(Reflector.GetPropertyName<LoanedItem>(x => x.LoanedBy), FetchMode.Eager)
                   .List<LoanedItem>();
        }
    }
}
