using System.Linq;
using System.Web.Mvc;
using Core.Domain.Bases.Repositories;
using Core.Domain.Factories;
using Core.Domain.Model;
using AutoMapper;
using UserInterface.Models;

namespace UserInterface.Controllers
{
    public class LoanedItemController : Controller
    {
        private readonly ILoanedItemRepository _loanedItemRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public LoanedItemController() : this(LoanedItemRepositoryFactory.GetDefault(), EmployeeRepositoryFactory.GetDefault())
        {

        }

        public LoanedItemController(ILoanedItemRepository loanedItemRepository, IEmployeeRepository employeeRepository)
        {
            _loanedItemRepository = loanedItemRepository;
            _employeeRepository = employeeRepository;
        }
        //
        // GET: /LoanedItem/

        public ActionResult Index()
        {
            var result = Mapper.Map<LoanedItem[], LoanedItemForm[]>(_loanedItemRepository.GetAllView().ToArray());
            return View(result);
        }

        //
        // GET: /LoanedItem/Create
        [OutputCache(VaryByParam = "*", Duration = 0)]
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /LoanedItem/Create

        [HttpPost]
        public ActionResult Create(LoanedItem loanedItem, int employeeId)
        {
            try
            {   
                loanedItem.LoanedBy = _employeeRepository.GetById(employeeId);
                _loanedItemRepository.SaveOrUpdate(loanedItem);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /LoanedItem/Delete/5

        public ActionResult Delete(int id)
        {
            _loanedItemRepository.Delete(_loanedItemRepository.GetById(id));
            return View();
        }

        //
        // GET: /LoanedItem/Edit/5
        [OutputCache(VaryByParam = "*", Duration = 0)]
        public ActionResult Edit(int id)
        {
            return View(_loanedItemRepository.GetById(id));
        }

        //
        // POST: /LoanedItem/Edit/5

        [HttpPost]
        public ActionResult Edit(LoanedItem loanedItem)
        {
            try
            {
                _loanedItemRepository.Merge(loanedItem);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
