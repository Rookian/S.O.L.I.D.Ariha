using System.Linq;
using Iesi.Collections.Generic;
using Core.Domain.Bases;

namespace Core.Domain.Model
{
    public class Team : Entity
    {
        public virtual string Name { get; set; }
        private readonly ISet<Employee> _employees = new HashedSet<Employee>();

        public virtual Employee[] GetEmployees()
        {
            return _employees.ToArray();
        }

        public virtual Team RemoveEmployee(Employee employee)
        {
            if (GetEmployees().Contains(employee))
            {
                _employees.Remove(employee);
                employee.RemoveTeam(this);
            }
            
            return this;
        }

        public virtual Team AddEmployee(Employee employee)
        {
            if (GetEmployees().Contains(employee))
            {
                _employees.Add(employee);
                employee.AddTeam(this);
            }

            return this;
        }
    }
}