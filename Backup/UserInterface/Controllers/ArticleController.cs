using System.Linq;
using System.Web.Mvc;
using Core.Domain.Bases.Repositories.ConsumerProtection;
using Core.Domain.Factories.ConsumerProtection;
using Core.Domain.Model.ConsumerProtection;
using UserInterface.Constants;
using UserInterface.Extensions;

namespace UserInterface.Controllers
{
    public class ArticleController : Controller
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IGoodsGroupRepository _goodsGroupRepository;
        public ArticleController()
            : this(ArticleRepositoryFactory.GetDefault(), GoodsGroupRepositoryFactory.GetDefault())
        {

        }

        public ArticleController(IArticleRepository articleRepository, IGoodsGroupRepository goodsGroupRepository)
        {
            _articleRepository = articleRepository;
            _goodsGroupRepository = goodsGroupRepository;
        }

        //
        // GET: /Article/

        public ActionResult Index()
        {
            return View(_articleRepository.GetAll());
        }

        //
        // GET: /Article/Create

        public ActionResult Create()
        {
            ViewData[Keys.GoodsGroupDropDownList] =
                MvcExtensions.CreateSelectList(_goodsGroupRepository.GetAll().ToList(), x => x.Id, x => x.Description);

            return View();
        }

        //
        // POST: /Article/Create

        [HttpPost]
        public ActionResult Create(Article article, int goodsGroupId)
        {
            try
            {
                article.GoodsGroup = _goodsGroupRepository.GetById(goodsGroupId);
                _articleRepository.SaveOrUpdate(article);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
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
            Article article = _articleRepository.GetById(id);
            int goodsGroupId = 0;

            if (article.GoodsGroup != null)
            {
                goodsGroupId = article.GoodsGroup.Id;
            }

            ViewData[Keys.GoodsGroupDropDownList] =
               MvcExtensions.CreateSelectList(_goodsGroupRepository.GetAll().ToList(), x => x.Id, x => x.Description, goodsGroupId);
            
            return View(article);
        }

        //
        // POST: /Article/Edit/5

        [HttpPost]
        public ActionResult Edit(Article article, int goodsGroupId)
        {
            try
            {
                article.GoodsGroup = _goodsGroupRepository.GetById(goodsGroupId);
                _articleRepository.SaveOrUpdate(article);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
