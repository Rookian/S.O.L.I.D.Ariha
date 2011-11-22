using Core.Domain.Bases.Repositories;
using Infrastructure.NHibernate.Repositories;
using UserInterface.Controllers;


namespace SchemaCreator
{
    class Program
    {
        //private static readonly ITeamRepository _teamRepository = new TeamRepository();
        //private static readonly IEmployeeRepository _employeeRepository = new EmployeeRepository();

        //private static readonly EmployeeController _employeeController = new EmployeeController(_employeeRepository, _teamRepository);
        static void Main()
        {


            Helper.CreateSessionFactory();

            //CreateTestDb();
            //Helper.CreateSessionFactory();



            //EmployeeRepository employeeRepository = new EmployeeRepository();
            //Employee employee = employeeRepository.GetEmployeeByName("aaa");
            //employee.EmployeeLastName = "TEST!!!";
            //Employee employee2 = employeeRepository.GetEmployeeByName("aaa");
        }

        private static void CreateTestDb()
        {
            //var repository = new SalesmanArticleRepository();

            //var salesmanArticle = new SalesmanArticle
            //                          {
            //                              Salesman = new Salesman
            //                                             {
            //                                                 Name = "PC-WARE1", Place = "Leipzig1"
            //                                             },
            //                              Article = new Article 
            //                              { 
            //                                  Description = "PCs1",
            //                                  GoodsGroup = new GoodsGroup
            //                                                   {
            //                                                       Description = "Hardware"
            //                                                   }
            //                              },
            //                              Amount = 10,
            //                              Cost = 100,
            //                              Date = DateTime.Now
            //                          };

            //repository.SaveOrUpdate(salesmanArticle);


            using (var session = Helper.CreateSessionFactory().OpenSession())
            {
            //    using (session.BeginTransaction())
            //    {
            //        //var employee1 = new Employee { EmployeeEMail = "Mail1", EmployeeFirstName = "Firstname1", EmployeeLastName = "Lastname1" };
            //        //var team1 = new Team { Name = "Team1" };
            //        //var team2 = new Team { Name = "Team2" };

            //        //employee1.AddTeam(team1);
            //        //employee1.AddTeam(team2);

            //        //var employee2 = new Employee { EmployeeEMail = "Mail2", EmployeeFirstName = "Firstname2", EmployeeLastName = "Lastname2" };
            //        //var team3 = new Team { Name = "Team3" };

            //        //employee2.AddTeam(team3);
            //        //employee2.AddTeam(team2);

            //        //session.Save(employee1);
            //        //session.Save(employee2);

            //        //  T	E
            //        //  -----
            //        //  1	1
            //        //  2	1
            //        //  2	2
            //        //  3	2
            //    }
            }
        }
    }
}
