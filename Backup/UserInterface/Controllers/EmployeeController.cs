using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Core.Domain.Bases.Repositories;
using Core.Domain.Factories;
using Core.Domain.Model;
using UserInterface.Constants;
using UserInterface.Extensions;
using UserInterface.Models;

namespace UserInterface.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly ILoanedItemRepository _loanedItemRepository;

        public EmployeeController() : this(EmployeeRepositoryFactory.GetDefault(), TeamRepositoryFactory.GetDefault(), LoanedItemRepositoryFactory.GetDefault())
        {

        }

        public EmployeeController(IEmployeeRepository employeeRepository, ITeamRepository teamRepository, ILoanedItemRepository loanedItemRepository)
        {
            _employeeRepository = employeeRepository;
            _teamRepository = teamRepository;
            _loanedItemRepository = loanedItemRepository;
        }

        public ActionResult Index()
        {
            var result = _employeeRepository.GetAllView();
            var mappedresult = Mapper.Map<List<Employee>, List<EmployeeForm>>(result);

            return View(mappedresult);
        }

        //
        // GET: /Employee/Create

        [OutputCache(VaryByParam = "*", Duration = 0)]
        public ActionResult Create()
        {
            ViewData[Keys.Teams] = MvcExtensions.CreateSelectList(_teamRepository.GetAll().ToList(), teamVal => teamVal.Id, teamText => teamText.Name);
            return View();
        }

        //
        // POST: /Employee/Create

        [HttpPost]
        public ActionResult Create(Employee employee, int teamId)
        {
            try
            {
                employee.AddTeam(_teamRepository.GetById(teamId));
                _employeeRepository.SaveOrUpdate(employee);
                return RedirectToAction("Index");
            }
            catch
            {
                ViewData[Keys.Teams] = MvcExtensions.CreateSelectList(_teamRepository.GetAll().ToList(), teamVal => teamVal.Id, teamText => teamText.Name);
                return View();
            }
        }

        //
        // POST: /Employee/Delete/5

        public ActionResult Delete(Employee employee)
        {
            try
            {
                _employeeRepository.Delete(employee);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Employee/Edit/5
        [OutputCache(VaryByParam = "*", Duration = 0)]
        public ActionResult Edit(int id)
        {
            ViewData[Keys.Teams] = MvcExtensions.CreateSelectList(_teamRepository.GetAll().ToList(), teamVal => teamVal.Id, teamText => teamText.Name);
            return View(_employeeRepository.GetById(id) ?? new Employee());
        }

        //
        // POST: /Employee/Edit/5

        [HttpPost]
        public ActionResult Edit(Employee employee, int teamId)
        {
            try
            {
                var employeeTmp = _employeeRepository.GetById(employee.Id);

                employeeTmp.AddTeam(_teamRepository.GetById(teamId));
                _employeeRepository.SaveOrUpdate(employeeTmp);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        [OutputCache(VaryByParam = "*", Duration = 0)]
        public ActionResult Details(int id)
        {
            var employee = _employeeRepository.GetById(id);

            ViewData[Keys.EmployeeName] = string.Format("{0} {1}", employee.FirstName, employee.LastName);
            return View(employee.GetLoanedItems());
        }
    }
}