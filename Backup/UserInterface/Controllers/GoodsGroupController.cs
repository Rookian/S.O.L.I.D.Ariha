using System.Web.Mvc;
using Core.Domain.Bases.Repositories.ConsumerProtection;
using Core.Domain.Factories.ConsumerProtection;
using Core.Domain.Model.ConsumerProtection;

namespace UserInterface.Controllers
{
    public class GoodsGroupController : Controller
    {
        private readonly IGoodsGroupRepository _goodsGroupRepository;
        public GoodsGroupController()
            : this(GoodsGroupRepositoryFactory.GetDefault())
        {

        }

        public GoodsGroupController(IGoodsGroupRepository goodsGroupRepository)
        {
            _goodsGroupRepository = goodsGroupRepository;
        }

        //
        // GET: /GoodsGroup/

        public ActionResult Index()
        {
            return View(_goodsGroupRepository.GetAll());
        }

        //
        // GET: /GoodsGroup/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /GoodsGroup/Create

        [HttpPost]
        public ActionResult Create(GoodsGroup goodsGroup)
        {
            try
            {
                _goodsGroupRepository.SaveOrUpdate(goodsGroup);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /GoodsGroup/Delete/5

        public ActionResult Delete(int id)
        {
            _goodsGroupRepository.Delete(_goodsGroupRepository.GetById(id));
            return RedirectToAction("Index");
        }

    
        //
        // GET: /GoodsGroup/Edit/5

        public ActionResult Edit(int id)
        {
            return View(_goodsGroupRepository.GetById(id));
        }

        //
        // POST: /GoodsGroup/Edit/5

        [HttpPost]
        public ActionResult Edit(GoodsGroup goodsGroup)
        {
            try
            {
                _goodsGroupRepository.SaveOrUpdate(goodsGroup);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
