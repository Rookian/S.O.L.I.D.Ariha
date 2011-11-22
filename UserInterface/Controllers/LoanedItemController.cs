using System.Web.Mvc;
using Core.Domain.Bases.Repositories;
using Core.Domain.Model;
using UserInterface.Controllers.Base;

namespace UserInterface.Controllers
{
    public class LoanedItemController : ConventionController
    {
        private readonly ILoanedItemRepository _loanedItemRepository;

        public LoanedItemController(ILoanedItemRepository loanedItemRepository)
        {
            _loanedItemRepository = loanedItemRepository;
        }
        //
        // GET: /LoanedItem/

        public ActionResult Index()
        {
            return null; //AutoMappedView<LoanedItemForm[]>(_loanedItemRepository.GetAllView().ToArray());
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
                //loanedItem.LoanedBy = _employeeRepository.GetById(employeeId);
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
            return View("Index");
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
