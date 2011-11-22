using System.Linq;
using System.Web.Mvc;
using Core.Domain.Bases.Repositories.ConsumerProtection;
using Core.Domain.Model.ConsumerProtection;
using UserInterface.Constants;
using UserInterface.Controllers.Base;
using UserInterface.Extensions;

namespace UserInterface.Controllers
{
    public class ArticleController : ConventionController
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IGoodsGroupRepository _goodsGroupRepository;

        public ArticleController(IArticleRepository articleRepository, IGoodsGroupRepository goodsGroupRepository)
        {
            _articleRepository = articleRepository;
            _goodsGroupRepository = goodsGroupRepository;
        }

        //
        // GET: /Article/

        public ActionResult Index()
        {

            var article3 = _articleRepository.GetById(1);
            var article4 = _articleRepository.GetById(1);
            return View(_articleRepository.GetAll());
        }

        //
        // GET: /Article/Create

        public ActionResult Create()
        {
            //ViewData[Keys.GoodsGroupDropDownList] =
            //    MvcExtensions.CreateSelectList(_goodsGroupRepository.GetAll().ToList(), x => x.Id, x => x.Description);

            return View();
        }

        //
        // POST: /Article/Create

        [HttpPost]
        public ActionResult Create(Article article, int goodsGroupId)
        {

            article.GoodsGroup = _goodsGroupRepository.GetById(goodsGroupId);
            _articleRepository.SaveOrUpdate(article);

            return RedirectToAction("Index");
        }

        //
        // GET: /Article/Delete/5

        public ActionResult Delete(int id)
        {
            _articleRepository.Delete(_articleRepository.GetById(id));
            return RedirectToAction("Index");
        }

        //
        // GET: /Article/Edit/5

        public ActionResult Edit(int id)
        {
            return View(_articleRepository.GetById(id));
        }

        //
        // POST: /Article/Edit/5

        [HttpPost]
        public ActionResult Edit(Article article, int goodsGroupId)
        {
            article.GoodsGroup = _goodsGroupRepository.GetById(goodsGroupId);
            _articleRepository.SaveOrUpdate(article);

            return RedirectToAction("Index");
        }
    }
}
