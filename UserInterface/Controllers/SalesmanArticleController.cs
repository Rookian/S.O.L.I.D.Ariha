using System.Linq;
using System.Web.Mvc;
using Core.Domain.Bases.Repositories.ConsumerProtection;
using Core.Domain.Model.ConsumerProtection;
using UserInterface.Constants;
using UserInterface.Extensions;

namespace UserInterface.Controllers
{
    public class SalesmanArticleController : Controller
    {
        private readonly ISalesmanArticleRepository _salesmanArticleRepository;
        private readonly ISalesmanRepository _salesmanRepository;
        private readonly IArticleRepository _articleRepository;

        public SalesmanArticleController(ISalesmanArticleRepository salesmanArticleRepository, ISalesmanRepository salesmanRepository, IArticleRepository articleRepository)
        {
            _salesmanArticleRepository = salesmanArticleRepository;
            _salesmanRepository = salesmanRepository;
            _articleRepository = articleRepository;
        }

        //
        // GET: /SalesmanArticle/

        public ContentResult Data()
        {
            var result = new ContentResult {Content = "<h1><b>Hallo</b></h1>"};
            return result;
        }

        public ActionResult Index()
        {
            var result = _salesmanArticleRepository.GetAll().OrderBy(x=>x.Date);
            
            ViewData[Keys.SalesmanArticleGroupedByMonthAndDescription] = _salesmanArticleRepository.GetSalesmanArticleGroupedByMonthAndDescription();

            return View(result);
        }

        //
        // GET: /SalesmanArticle/Create

        public ActionResult Create()
        {
            //ViewData[Keys.SalesmanDropDownList] = MvcExtensions.CreateSelectList(_salesmanRepository.GetAll().ToList(),
            //                                                                     x => x.Id, x => x.Name);

            //ViewData[Keys.ArticleDropDownList] = MvcExtensions.CreateSelectList(_articleRepository.GetAll().ToList(),
            //                                                                    x => x.Id, x => x.Description);
            return View();
        }

        //
        // POST: /SalesmanArticle/Create

        [HttpPost]
        public ActionResult Create(SalesmanArticle salesmanArticle, int salesmanId, int articleId)
        {
            try
            {
                salesmanArticle.Salesman = _salesmanRepository.GetById(salesmanId);
                salesmanArticle.Article = _articleRepository.GetById(articleId);

                _salesmanArticleRepository.SaveOrUpdate(salesmanArticle);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /SalesmanArticle/Delete/5

        public ActionResult Delete(int id)
        {
            _salesmanArticleRepository.Delete(_salesmanArticleRepository.GetById(id));
            return RedirectToAction("Index");
        }

        //
        // GET: /SalesmanArticle/Edit/5

        public ActionResult Edit(int id)
        {
            SalesmanArticle salesmanArticle = _salesmanArticleRepository.GetById(id);

            int salesmanId = 0;
            int articleId = 0;

            if (salesmanArticle.Salesman != null)
            {
                salesmanId = salesmanArticle.Salesman.Id;
            }

            if (salesmanArticle.Article != null)
            {
                articleId = salesmanArticle.Article.Id;
            }

            //ViewData[Keys.SalesmanDropDownList] = MvcExtensions.CreateSelectList(_salesmanRepository.GetAll().ToList(),
            //                                                                    x => x.Id, x => x.Name, salesmanId);

            //ViewData[Keys.ArticleDropDownList] = MvcExtensions.CreateSelectList(_articleRepository.GetAll().ToList(),
            //                                                                    x => x.Id, x => x.Description, articleId);
            return View(salesmanArticle);
        }

        //
        // POST: /SalesmanArticle/Edit/5

        [HttpPost]
        public ActionResult Edit(SalesmanArticle salesmanArticle, int salesmanId, int articleId)
        {
            try
            {
                salesmanArticle.Salesman = _salesmanRepository.GetById(salesmanId);
                salesmanArticle.Article = _articleRepository.GetById(articleId);

                _salesmanArticleRepository.SaveOrUpdate(salesmanArticle);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
