using System.Collections.Generic;
using Core.Domain.Model;

namespace Core.Domain.Bases.Repositories
{
    public interface ILoanedItemRepository : IRepository<LoanedItem>
    {
        IList<LoanedItem> GetAllView();
    }
}
