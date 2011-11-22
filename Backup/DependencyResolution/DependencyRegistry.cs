using Core.Domain.Bases;
using Core.Domain.Bases.Repositories;
using Core.Domain.Bases.Repositories.ConsumerProtection;
using Infrastructure.DataAccess.Repositories;
using Infrastructure.DataAccess.Repositories.ConsumerProtection;
using Infrastructure.DataAccess.Session;
using Infrastructure.DataAccess.SessionFactory;
using Infrastructure.DataAccess.UnitOfWork;
using StructureMap.Configuration.DSL;

namespace DependencyResolution
{
    public class DependencyRegistry : Registry
    {
        public DependencyRegistry()
        {
            // Repositories
            For(typeof (ILoanedItemRepository)).Use(typeof (LoanedItemRepository));
            For(typeof (ITeamRepository)).Use(typeof (TeamRepository));
            For(typeof (IEmployeeRepository)).Use(typeof (EmployeeRepository));

            For(typeof (IArticleRepository)).Use(typeof (ArticleRepository));
            For(typeof(ISalesmanRepository)).Use(typeof(SalesmanRepository));
            For(typeof(ISalesmanArticleRepository)).Use(typeof(SalesmanArticleRepository));
            For(typeof(IGoodsGroupRepository)).Use(typeof(GoodsGroupRepository));

            // UnitOfWork
            For(typeof (IUnitOfWork)).Use(typeof (UnitOfWork));

            // SessionBuilder
            For(typeof (ISessionBuilder)).Use(typeof (SessionBuilder));

            // SessionFactoryBuilder
            For(typeof (ISessionFactoryBuilder)).Use(typeof (SessionFactoryBuilder));
        }
    }
}
