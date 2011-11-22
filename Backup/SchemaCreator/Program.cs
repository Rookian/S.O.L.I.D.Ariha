using System;
using Core.Domain.Model.ConsumerProtection;
using Infrastructure.DataAccess.Repositories.ConsumerProtection;


namespace SchemaCreator
{
    class Program
    {
        static void Main()
        {
            CreateTestDb();
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
            //        //var employee1 = new Employee { EMail = "Mail1", FirstName = "Firstname1", LastName = "Lastname1" };
            //        //var team1 = new Team { Name = "Team1" };
            //        //var team2 = new Team { Name = "Team2" };

            //        //employee1.AddTeam(team1);
            //        //employee1.AddTeam(team2);

            //        //var employee2 = new Employee { EMail = "Mail2", FirstName = "Firstname2", LastName = "Lastname2" };
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
