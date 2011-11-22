using System.Linq;
using System.Web.Mvc;
using Core.Domain.Bases.Repositories;
using Core.Domain.Factories;
using Core.Domain.Model;

namespace UserInterface.Controllers
{
    public class TeamController : Controller
    {
        private readonly ITeamRepository _teamRepository;

        public TeamController() : this(TeamRepositoryFactory.GetDefault())
        {
            
        }

        public TeamController(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        //
        // GET: /Team/

        public ActionResult Index()
        {
            return View(_teamRepository.GetAll().ToList());
        }

        //
        // GET: /Team/Create
        [OutputCache(VaryByParam = "*", Duration = 0)]
        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Team/Create

        [HttpPost]
        public ActionResult Create(Team team)
        {
            try
            {
                _teamRepository.SaveOrUpdate(team);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Team/Delete/5

        public ActionResult Delete(int id)
        {
            try
            {
                _teamRepository.Delete(_teamRepository.GetById(id));
 
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Team/Edit/5
        [OutputCache(VaryByParam = "*", Duration = 0)]
        public ActionResult Edit(int id)
        {
            return View(_teamRepository.GetById(id)?? new Team());
        }

        //
        // POST: /Team/Edit/5

        [HttpPost]
        public ActionResult Edit(Team team)
        {
            try
            {
                _teamRepository.SaveOrUpdate(team);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
