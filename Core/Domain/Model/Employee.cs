using System.Collections.Generic;
using System.Linq;
using Core.Domain.Bases;


namespace Core.Domain.Model
{
    public class Employee : Entity
    {
        public virtual string LastName { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string EMail { get; set; }
        public virtual string Infos { get; set; }
        public virtual int Count { get; set; }
        public virtual bool IsCool { get; set; }

        //private readonly Iesi.Collections.Generic.ISet<Team> _teams = new HashedSet<Team>();
        private readonly IList<LoanedItem> _loanedItems = new List<LoanedItem>();

        //public virtual Team[] GetTeams()
        //{
        //    return _teams.ToArray();
        //}

        public virtual LoanedItem[] GetLoanedItems()
        {
            return _loanedItems.ToArray();
        }

        //public virtual Employee AddTeam(Team team)
        //{
        //    if (!GetTeams().Contains(team))
        //    {
        //        _teams.Add(team);
        //        team.AddEmployee(this);
        //    }

        //    return this;
        //}

        //public virtual Employee RemoveTeam(Team team)
        //{
        //    if (GetTeams().Contains(team))
        //    {
        //        _teams.Remove(team);
        //        team.RemoveEmployee(this);
        //    }

        //    return this;
        //}

        public virtual Employee AddLoanedItem(LoanedItem item)
        {
            _loanedItems.Add(item);
            return this;
        }

        public virtual Employee RemoveLoanedItem(LoanedItem item)
        {
            _loanedItems.Remove(item);
            return this;
        }
    }
}