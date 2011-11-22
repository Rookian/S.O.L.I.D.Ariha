using System.Web.Mvc;
using Core.Domain.Bases.Repositories.ConsumerProtection;
using Core.Domain.Factories.ConsumerProtection;
using Core.Domain.Model.ConsumerProtection;

namespace UserInterface.Controllers
{
    public class SalesmanController : Controller
    {
        private readonly ISalesmanRepository _salesmanRepository;
        public SalesmanController()
            : this(SalesmanRepositoryFactory.GetDefault())
        {

        }

        public SalesmanController(ISalesmanRepository salesmanRepository)
        {
            _salesmanRepository = salesmanRepository;
        }

        //
        // GET: /Salesman/

        public ActionResult Index()
        {
            return View(_salesmanRepository.GetAll());
        }


        //
        // GET: /Salesman/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Salesman/Create

        [HttpPost]
        public ActionResult Create(Salesman salesman)
        {
            try
            {
                _salesmanRepository.SaveOrUpdate(salesman);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Salesman/Delete/5

        public ActionResult Delete(int id)
        {
            _salesmanRepository.Delete(_salesmanRepository.GetById(id));
            return RedirectToAction("Index");
        }

        //
        // GET: /Salesman/Edit/5

        public ActionResult Edit(int id)
        {
            return View(_salesmanRepository.GetById(id));
        }

        //
        // POST: /Salesman/Edit/5

        [HttpPost]
        public ActionResult Edit(Salesman salesman)
        {
            try
            {
                _salesmanRepository.SaveOrUpdate(salesman);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
