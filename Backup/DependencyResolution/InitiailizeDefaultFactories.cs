using Core.Domain.Bases;
using Core.Domain.Bases.Repositories;
using Core.Domain.Bases.Repositories.ConsumerProtection;
using Core.Domain.Factories;
using Core.Domain.Factories.ConsumerProtection;
using StructureMap;

namespace DependencyResolution
{
    public class InitiailizeDefaultFactories
    {
        public void Configure()
        {
            // UnitOfWorkFactory
            UnitOfWorkFactory.GetDefault = () => ObjectFactory.GetInstance<IUnitOfWork>();

            // Repository Factories
            EmployeeRepositoryFactory.GetDefault = () => ObjectFactory.GetInstance<IEmployeeRepository>();
            TeamRepositoryFactory.GetDefault = () => ObjectFactory.GetInstance<ITeamRepository>();
            LoanedItemRepositoryFactory.GetDefault = () => ObjectFactory.GetInstance<ILoanedItemRepository>();

            ArticleRepositoryFactory.GetDefault = () => ObjectFactory.GetInstance<IArticleRepository>();
            SalesmanArticleRepositoryFactory.GetDefault = () => ObjectFactory.GetInstance<ISalesmanArticleRepository>();
            SalesmanRepositoryFactory.GetDefault = () => ObjectFactory.GetInstance<ISalesmanRepository>();
            GoodsGroupRepositoryFactory.GetDefault = () => ObjectFactory.GetInstance<IGoodsGroupRepository>();
        }
    }
}